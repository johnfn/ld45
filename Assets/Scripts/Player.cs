using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitFlags {
  // Hit information (for solid things that stop you)
  public float XHit = 0f;
  public float YHit = 0f;

  public List<GameObject> XHitObjects;
  public List<GameObject> YHitObjects;

  public bool HitTop() { return YHit > 0; } 
  public bool HitBottom() { return YHit < 0; } 
  public bool HitLeft() { return XHit < 0; } 
  public bool HitRight() { return XHit > 0; } 

  // Touch information (for things that you can walk through, but impact you somehow)
  public float XTouch = 0f;
  public float YTouch = 0f;

  public List<GameObject> XTouchedObjects;
  public List<GameObject> YTouchedObjects;

  public bool TouchTop() { return YTouch > 0; } 
  public bool TouchBottom() { return YTouch < 0; } 
  public bool TouchLeft() { return XTouch < 0; } 
  public bool TouchRight() { return XTouch > 0; } 


  override public string ToString() {
    if (HitAnything()) {
      return $"Hit: {(HitTop() ? "Top" : "")} {(HitLeft() ? "Left" : "")} {(HitRight() ? "Right" : "")} {(HitBottom() ? "Bottom" : "")}";
    } else {
      return "";
    }
  }

  public bool HitAnything() {
    return XHit != 0f || YHit != 0f;
  }

  public bool TouchAnything() {
    return XTouch != 0f || YTouch != 0f;
  }
}

[RequireComponent(typeof(BoxCollider2D))]
// extends MonoBehaviour = is component
public class Player: MonoBehaviour {
  public float movementSpeed = 0.2f;

  public float fallingSpeed = -0.3f;

  [Header("1 = infinite floaty. 1.5 = normal floaty. 2 = kind of floaty")]
  public float gravityScaleFactor = 1.3f;

  [HideInInspector]
  public Character character;

  public float Width { get { return boxCollider.bounds.size.x; } }

  public float Height { get { return boxCollider.bounds.size.y; } }

  public Vector3 TopLeft { get { return transform.position + new Vector3(-Width / 2, Height / 2); } }

  public Vector3 TopRight { get { return transform.position + new Vector3(Width / 2, Height / 2); } }

  public Vector3 BottomLeft { get { return transform.position + new Vector3(-Width / 2, -Height / 2); } }

  public Vector3 BottomRight { get { return transform.position + new Vector3(Width / 2, -Height / 2); } }

  private BoxCollider2D boxCollider;
  private Animator anim;
  private SpriteRenderer spriteRenderer;

  private float velocityX = 0f;
  private float velocityY = 0f;

  private float accelerationY = 0f;

  private HitFlags lastHitFlags;

  void Start() {
    boxCollider = GetComponent<BoxCollider2D>();
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    character = GetComponent<Character>();

    lastHitFlags = new HitFlags();
  }

  List<RaycastHit2D> GetSolidColliders(RaycastHit2D[] raycastResult) {
    var result = new List<RaycastHit2D>();

    foreach (var x in raycastResult) {
      if (x.collider.GetComponent<Player>() != null) {
        continue;
      }

      if (x.collider.tag == "Vines") {
        continue;
      }

      result.Add(x);
    }

    return result;
  }

  List<RaycastHit2D> GetPassableColliders(RaycastHit2D[] raycastResult) {
    var result = new List<RaycastHit2D>();

    foreach (var x in raycastResult) {
      if (x.collider.GetComponent<Player>() != null) {
        continue;
      }

      if (x.collider.tag != "Vines") {
        continue;
      }

      result.Add(x);
    }

    return result;
  }

  HitFlags CheckForHit(Vector3 desiredMovement) {
    var hitFlagsResult = new HitFlags();

    var x = desiredMovement.x;
    var y = desiredMovement.y;
    var xComponent = new Vector3(x, 0, 0);
    var yComponent = new Vector3(0, y, 0);

    if (x != 0) {
      var start  = transform.position + new Vector3(Mathf.Sign(x) * Width / 2,  Height / 2, 0);
      var end    = transform.position + new Vector3(Mathf.Sign(x) * Width / 2, -Height / 2, 0);
      var middle = (start + end) / 2f;

      var pointsToCheck = new List<Vector3> { start, middle, end };

      foreach (var point in pointsToCheck) {
        var result = Physics2D.RaycastAll(point, xComponent, xComponent.magnitude);
        var solidColliders = GetSolidColliders(result);

        if (solidColliders.Count > 0) {
          // we have a hit

          hitFlagsResult.XHit = Mathf.Sign(x);
          hitFlagsResult.XHitObjects = solidColliders.Select(collider => collider.collider.gameObject).ToList();

          break;
        }

        var passableColliders = GetPassableColliders(result);

        if (passableColliders.Count > 0) {
          hitFlagsResult.XTouchedObjects = passableColliders.Select(collider => collider.collider.gameObject).ToList();
        }
      }
    }

    if (y != 0) {
      var start  = transform.position + new Vector3(Width / 2, Mathf.Sign(y) *  Height / 2, 0);
      var end    = transform.position + new Vector3(Width / 2, Mathf.Sign(y) * -Height / 2, 0);
      var middle = (start + end) / 2f;

      var pointsToCheck = new List<Vector3> { start, middle, end };

      foreach (var point in pointsToCheck) {
        var result = Physics2D.RaycastAll(point, yComponent, yComponent.magnitude);
        var solidColliders = GetSolidColliders(result);

        if (solidColliders.Count > 0) {
          // we have a hit

          hitFlagsResult.YHit = Mathf.Sign(y);
          hitFlagsResult.YHitObjects = solidColliders.Select(collider => collider.collider.gameObject).ToList();

          break;
        }

        var passableColliders = GetPassableColliders(result);

        if (passableColliders.Count > 0) {
          hitFlagsResult.YTouchedObjects = passableColliders.Select(collider => collider.collider.gameObject).ToList();
        }
      }
    }

    return hitFlagsResult;
  }

  Vector3 calculateVelocity() {
    velocityY += accelerationY;
    accelerationY /= gravityScaleFactor;

    // cap velocity at gravity so it doesn't become arbitrarily huge when you
    // stand on a platform
    if (lastHitFlags.HitBottom() && velocityY < fallingSpeed) {
      velocityY = fallingSpeed;
    }

    if (lastHitFlags.TouchAnything()) {
      velocityY = 0f;
    }

    var dx = 0;
    var dy = velocityY;

    if (Input.GetKey("a")) { dx -= 1; }
    if (Input.GetKey("d")) { dx += 1; }

    if (Input.GetKey("space") && lastHitFlags.HitBottom()) { accelerationY = 1; }

    return new Vector3(dx, dy, 0) * movementSpeed;
  }

  bool isJumping()
  {
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, spriteRenderer.bounds.extents.y +0.3f);
    if (hit.collider != null)
    {
      return false;
    }
    return true;


  }

  HitFlags Move(Vector3 desiredMovement) {
    var hit = CheckForHit(desiredMovement);

    if (hit.HitAnything()) {
      var stepSize = 0.01f;

      for (var x = 0f; Mathf.Abs(x) < Mathf.Abs(desiredMovement.x); x += stepSize * Mathf.Sign(desiredMovement.x)) {
        var step = new Vector3(Mathf.Sign(desiredMovement.x) * stepSize, 0, 0);
        var xHit = CheckForHit(step);

        if (xHit.HitAnything()) {
          break;
        }

        transform.position += step;
      }

      for (var y = 0f; Mathf.Abs(y) < Mathf.Abs(desiredMovement.y); y += stepSize * Mathf.Sign(desiredMovement.y)) {
        var step = new Vector3(0, Mathf.Sign(desiredMovement.y) * stepSize, 0);
        var yHit = CheckForHit(step);

        if (yHit.HitAnything()) {
          break;
        }

        transform.position += step;
      }

      return hit;
    }

    transform.position += desiredMovement;

    return hit;
  }

  void Update() {
    velocityY -= 0.3f;

    var desiredMovement = calculateVelocity();
    anim.SetBool("walking", Mathf.Abs(desiredMovement.x) > 0);
    anim.SetBool("jumping", isJumping());
    spriteRenderer.flipX = desiredMovement.x < 0;
    var hitFlags = Move(desiredMovement);

    this.lastHitFlags = hitFlags;

    if (Input.GetKeyDown("1")) {
      // Show emotion
    }
  }

  /* Outward-facing functions */

  /**
   * 
   */
  public void ShowEmotionCue(EmotionType emotionType) {
    GameObject MyCanvas = this.character.CharacterCanvas;
    GameObject EmotionCue = Manager.CreateNewEmotionCue(emotionType, MyCanvas);
  }
}
