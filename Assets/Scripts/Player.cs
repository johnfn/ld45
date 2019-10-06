using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitFlags {
  public float XHit = 0f;
  public float YHit = 0f;

  public List<GameObject> XHitObjects;
  public List<GameObject> YHitObjects;

  public bool HitTop() {
    return YHit > 0;
  }

  public bool HitBottom() {
    return YHit < 0;
  }

  public bool HitLeft() {
    return XHit < 0;
  }

  public bool HitRight() {
    return XHit > 0;
  }

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

  List<RaycastHit2D> GetColliders(RaycastHit2D[] raycastResult) {
    var result = new List<RaycastHit2D>();

    foreach (var x in raycastResult) {
      if (x.collider.GetComponent<Player>() != null) {
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
        var colliders = GetColliders(result);

        if (colliders.Count > 0) {
          // we have a hit

          hitFlagsResult.XHit = Mathf.Sign(x);
          hitFlagsResult.XHitObjects = colliders.Select(collider => collider.collider.gameObject).ToList();

          break;
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
        var colliders = GetColliders(result);

        if (colliders.Count > 0) {
          // we have a hit

          hitFlagsResult.YHit = Mathf.Sign(y);
          hitFlagsResult.YHitObjects = colliders.Select(collider => collider.collider.gameObject).ToList();

          break;
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

    var dx = 0;
    var dy = velocityY;

    if (Input.GetKey("a")) { dx -= 1; }
    if (Input.GetKey("d")) { dx += 1; }

    if (Input.GetKey("space") && lastHitFlags.HitBottom()) { accelerationY = 1; }

    return new Vector3(dx, dy, 0) * movementSpeed;
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
    spriteRenderer.flipX = desiredMovement.x < 0;
    var hitFlags = Move(desiredMovement);

    this.lastHitFlags = hitFlags;

    if (Input.GetKeyDown("e")) {
      // Show dialog
      Manager.CreateNewDialog("Hello world!", this.gameObject);
    } else if (Input.GetKeyDown("1")) {
      // Show emotion
    }
  }

  /* Outward-facing functions */

  /**
   * 
   */
  public showEmotionCue(EmotionType emotionType) {
    
  }
}
