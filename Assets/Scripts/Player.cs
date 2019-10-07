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

  public bool TouchingWater = false;

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

[System.Serializable]
public class EmotionState {
  public bool Curiosity = false;

  public bool Compassion = false;

  public bool Affection = false;

  public bool Remorse = false;

  public bool Betrayal = false;

  public bool Forgiveness = false;
}

[RequireComponent(typeof(BoxCollider2D))]
// extends MonoBehaviour = is component
public class Player: MonoBehaviour {

  /// All emotions the player has unlocked.
  public EmotionState Emotions;

  /// The emotion lens through which the player is currently viewing the world.
  public EmotionType EmotionLens;

  [Header("Controls")]

  public float MovementSpeed;

  public float FallingSpeed;

  public float LadderSpeed = 1f;

  public float MaxFallSpeed;

  public float JumpStrength;

  [Header("1 = infinite floaty. 1.5 = normal floaty. 2 = kind of floaty")]
  public float gravityScaleFactor = 1.3f;

  public ParticleSystem dustPuffs;
  public ParticleSystem leaves;


  public GameObject shadow;

  [HideInInspector]
  public Character character;

  [HideInInspector]
  private Vector3 LookingDirection = Vector3.zero;

  private Vector3 lastSafeSpot;

  public float Width { get { return boxCollider.bounds.size.x; } }

  public float Height { get { return boxCollider.bounds.size.y; } }

  public Vector3 TopLeft { get { return transform.position + new Vector3(-Width / 2, Height / 2); } }

  public Vector3 TopRight { get { return transform.position + new Vector3(Width / 2, Height / 2); } }

  public Vector3 BottomLeft { get { return transform.position + new Vector3(-Width / 2, -Height / 2); } }

  public Vector3 BottomRight { get { return transform.position + new Vector3(Width / 2, -Height / 2); } }

  private BoxCollider2D boxCollider;
  private Animator anim;
  private SpriteRenderer spriteRenderer;

  private bool isActivelyJumping = false;
  private bool didCancelJump = false;

  private bool isTouchingLadder = false;
  private bool isFacingRight = true;

  private float velocityY = 0f;

  private float accelerationY = 0f;

  private HitFlags hitFlags;

  void Start() {
    boxCollider = GetComponent<BoxCollider2D>();
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    character = GetComponent<Character>();
    dustPuffs.Stop();
    leaves.Stop();

    hitFlags = new HitFlags();
  }

  public Vector3 GetLookingDirection() {
    return LookingDirection;
  }

  public void SetLookingDirection(Vector3 direction) {
    LookingDirection = direction.normalized;
  }

  private bool IsColliderAVine(Collider2D obj) {
    var parent = obj.gameObject.transform.parent;

    if (parent == null) {
      return false;
    }

    var grandParent = parent.transform.parent;

    if (grandParent == null) {
      return false;
    }

    return grandParent.gameObject.tag == "Vines";
  }

  private bool IsColliderWater(Collider2D obj) {
    var parent = obj.gameObject.transform.parent;

    if (parent == null) {
      return false;
    }

    var grandParent = parent.transform.parent;

    if (grandParent == null) {
      return false;
    }

    return grandParent.gameObject.tag == "Water";
  }

  List<RaycastHit2D> GetSolidColliders(RaycastHit2D[] raycastResult) {
    var result = new List<RaycastHit2D>();

    foreach (var x in raycastResult) {
      if (x.collider.GetComponent<Player>() != null) {
        continue;
      }

      if (IsColliderAVine(x.collider)) {
        continue;
      }

      if(x.collider.GetComponent<TutTrigger>() != null || x.collider.GetComponent<Character>() != null) {
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
          hitFlagsResult.TouchingWater = hitFlagsResult.TouchingWater || result.Any(hit => IsColliderWater(hit.collider));

          break;
        }
      }
    }

    if (y != 0) {
      var start  = transform.position + new Vector3( Width / 2f, Mathf.Sign(y) * Height / 2, 0);
      var end    = transform.position + new Vector3(-Width / 2f, Mathf.Sign(y) * Height / 2, 0);
      var middle = (start + end) / 2f;

      var pointsToCheck = new List<Vector3> { start, middle, end };

      foreach (var point in pointsToCheck) {
        var result = Physics2D.RaycastAll(point, yComponent, yComponent.magnitude);
        var solidColliders = GetSolidColliders(result);

        if (solidColliders.Count > 0) {
          // we have a hit

          hitFlagsResult.YHit = Mathf.Sign(y);
          hitFlagsResult.YHitObjects = solidColliders.Select(collider => collider.collider.gameObject).ToList();
          hitFlagsResult.TouchingWater = hitFlagsResult.TouchingWater || result.Any(hit => IsColliderWater(hit.collider));

          break;
        }
      }
    }

    return hitFlagsResult;
  }

  /// Call to trigger jump.
  void startJump() {
    isActivelyJumping = true;
    accelerationY = JumpStrength / 2;
    velocityY = JumpStrength * 5;
  }

  void cancelJump() {
    isActivelyJumping = false;
  }

  // Grounded when not in mid-jump.
  bool checkIsMidjump() {
    return (isActivelyJumping || Mathf.Abs(accelerationY) >= 0.01f);
  }

  bool checkIsMidair() {
    return !this.hitFlags.HitBottom();
  }

  

  Vector3 calculateVelocity() {
    var dx = 0f;
    var dy = 0f;

    if (Input.GetKey("a")) {
      dx -= 1;
      SetLookingDirection(new Vector3(-1f, 0, 0));
    }

    if (Input.GetKey("d")) {
      dx += 1; 
      SetLookingDirection(new Vector3(1f, 0, 0));
    }

    if (isTouchingLadder) {
      if (Input.GetKey("w")) { 
        dy += LadderSpeed; 
        SetLookingDirection(new Vector3(0f, 1f, 0f));
      }

      if (Input.GetKey("s")) { 
        dy -= LadderSpeed; 
        SetLookingDirection(new Vector3(0f, -1f, 0f));
      }

      accelerationY = 0f;
      velocityY     = 0f;

      if (Input.GetKeyDown("space") && !checkIsMidjump()) {
        startJump();
      } else if (Input.GetKeyUp("space") && checkIsMidjump()) {
        cancelJump();
      }
    } else {
      velocityY += accelerationY;
      accelerationY /= gravityScaleFactor;

      // cap velocity at gravity so it doesn't become arbitrarily huge when you
      // stand on a platform
      if (hitFlags.HitBottom() && velocityY < FallingSpeed) {
        velocityY = FallingSpeed;
      }

      if (velocityY < MaxFallSpeed) {
        velocityY = MaxFallSpeed;
      }

      dy = velocityY;

      if (Input.GetKey("space") && !checkIsMidair()) {
        startJump();
      } else if (Input.GetKeyUp("space") && checkIsMidjump()) {
        cancelJump();
      }
    }

    var result = new Vector3(dx, dy, 0) * MovementSpeed;

    return result;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (IsColliderAVine(other)) {
      isTouchingLadder = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (IsColliderAVine(other)) {
      isTouchingLadder = false;
    }
  }

  HitFlags Move(Vector3 desiredMovement) {
    var hit = CheckForHit(desiredMovement);

    if (!isTouchingLadder && hit.HitAnything()) {
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

  void RecoverFromDeath() {
    transform.position = lastSafeSpot;
  }

  void FixedUpdate() {
    velocityY -= 0.3f;

    var desiredMovement = calculateVelocity();

    // Set Animator parameters based on new and previous states

    bool prevWalk = anim.GetBool("walking"), 
         prevJump = anim.GetBool("jumping"), 
         prevClimb = anim.GetBool("climbing");
    bool nextWalk = Mathf.Abs(desiredMovement.x) > 0, 
         nextJump = !isTouchingLadder && checkIsMidair(),
         nextClimb = isTouchingLadder && (
           prevClimb || (desiredMovement.y == LadderSpeed * MovementSpeed || desiredMovement.y == -LadderSpeed * MovementSpeed) // hack
         );
    

    if (!prevClimb && nextClimb) leaves.Play();
    if (prevClimb && !nextClimb) leaves.Stop();
    if (!prevWalk && nextWalk) dustPuffs.Play();
    if (prevWalk && !nextWalk) dustPuffs.Stop();
    if (!prevClimb && nextClimb) anim.Play("Climb");
    if (prevClimb || nextClimb) {
      anim.speed = Mathf.Abs(desiredMovement.y) > 0 ? 1 : 0;
      if (Mathf.Abs(desiredMovement.y) > 0)
      {
        anim.speed = 1;
        //leaves.Play();

      }
      else {
        anim.speed = 0;
        //leaves.Stop();

      }
    } else { anim.speed = 1; }

    shadow.SetActive(!nextJump && !nextClimb);

    anim.SetBool("walking", nextWalk);
    anim.SetBool("climbing", nextClimb);
    anim.SetBool("jumping", nextJump);

    //Make the sprite and particle system face the right way
    bool prevDir = isFacingRight;
    if (desiredMovement.x > 0) {
      isFacingRight = true;
    } else if (desiredMovement.x < 0) {
      isFacingRight = false;
    }
    spriteRenderer.flipX = !isFacingRight;
    if (prevDir != isFacingRight) dustPuffs.transform.Rotate(Vector2.up, Mathf.Deg2Rad * 180);

    // This is pretty important. Hit() does not properly update your currently
    // touching objects if you try to raycast with a zero length vector, so
    // you'll loose that information if you don't guard for that case here.

    if (desiredMovement != Vector3.zero) {
      var oldHitFlags = hitFlags;

      var newHitFlags = Move(desiredMovement);
      hitFlags = newHitFlags;

      if (!hitFlags.TouchingWater && hitFlags.HitBottom() && !oldHitFlags.HitBottom()) {
        lastSafeSpot = transform.position;
      }
    }

    if (hitFlags.TouchingWater) {
            LeanTween.moveLocalY(Manager.Instance.bar2, -10f, 0.3f).setEaseInOutQuad();
            LeanTween.moveLocalY(Manager.Instance.bar1, 10f, 0.4f).setEaseInOutQuad().setOnComplete(() => {
                RecoverFromDeath();
                LeanTween.moveLocalY(Manager.Instance.bar1, 622f, 0.8f).setEaseInOutQuad();
                LeanTween.moveLocalY(Manager.Instance.bar2, -622f, 0.8f).setEaseInOutQuad();
            });

        }

    if (Input.GetKeyDown("x")) {
      var interactable = InteractableManager.Instance.GetInteractableTarget();

      if (interactable) {
        interactable.Interact();
      }
    }

    if (Input.GetKeyDown("1")) {
      // Show emotion
    }

    // Logs
    // Log velocity
  }

  /* Outward-facing functions */

  // Creates emotion cue on player canvas.
  public GameObject ShowEmotionCue(EmotionType emotionType) {
    GameObject MyCanvas = this.character.CharacterCanvas.gameObject;
    GameObject EmotionCue = Manager.CreateNewEmotionCue(emotionType, MyCanvas);

    return EmotionCue;
  }
}
