using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitFlags {
  public bool Top = false;
  public bool Left = false;
  public bool Right = false; 
  public bool Bottom = false;

  override public string ToString() {
    if (HitAnything()) {
      return $"Hit: {(Top ? "Top" : "")} {(Left ? "Left" : "")} {(Right ? "Right" : "")} {(Bottom ? "Bottom" : "")}";
    } else {
      return "";
    }
  }

  public bool HitAnything() {
    return (Top || Left || Right || Bottom);
  }
}

[RequireComponent(typeof(BoxCollider2D))]
public class Player: MonoBehaviour {
  public float movementSpeed = 5;

  public float Width { get { return boxCollider.bounds.size.x; } }

  public float Height { get { return boxCollider.bounds.size.y; } }

  public Vector3 TopLeft { get { return transform.position + new Vector3(-Width / 2, Height / 2); } }

  public Vector3 TopRight { get { return transform.position + new Vector3(Width / 2, Height / 2); } }

  public Vector3 BottomLeft { get { return transform.position + new Vector3(-Width / 2, -Height / 2); } }

  public Vector3 BottomRight { get { return transform.position + new Vector3(Width / 2, -Height / 2); } }

  private BoxCollider2D boxCollider;

  private float velocityX = 0f;
  private float velocityY = 0f;

  void Start() {
    boxCollider = GetComponent<BoxCollider2D>();
  }

  bool containsWall(RaycastHit2D[] raycastResult) {
    foreach (var x in raycastResult) {
      if (x.collider.GetComponent<Player>() != null) {
        continue;
      }

      return true;
    }

    return false;
  }

  HitFlags Move(Vector3 desiredMovement) {
    var hitFlagsResult = new HitFlags();

    var xComponent = new Vector3(desiredMovement.x, 0                , 0);
    var yComponent = new Vector3(0                , desiredMovement.y, 0);
    var x = desiredMovement.x;
    var y = desiredMovement.y;

    var actualMovementDelta = desiredMovement;

    if (x != 0) {
      var start  = transform.position + new Vector3(Mathf.Sign(x) * Width / 2,  Height / 2, 0);
      var end    = transform.position + new Vector3(Mathf.Sign(x) * Width / 2, -Height / 2, 0);
      var middle = (start + end) / 2f;
      var hit    = Physics2D.RaycastAll(start, xComponent);

      var pointsToCheck = new List<Vector3> { start, middle, end };

      foreach (var point in pointsToCheck) {
        if (containsWall(Physics2D.RaycastAll(point, xComponent, xComponent.magnitude))) {
          // we have a hit

          actualMovementDelta.x = 0;

          if (x > 0) {
            hitFlagsResult.Right = true;
          }

          if (x < 0) {
            hitFlagsResult.Left = true;
          }
        }
      }
    }

    if (y != 0) {
      var start  = transform.position + new Vector3(Width / 2, Mathf.Sign(y) *  Height / 2, 0);
      var end    = transform.position + new Vector3(Width / 2, Mathf.Sign(y) * -Height / 2, 0);
      var middle = (start + end) / 2f;
      var hit    = Physics2D.RaycastAll(start, yComponent);

      var pointsToCheck = new List<Vector3> { start, middle, end };

      foreach (var point in pointsToCheck) {
        if (containsWall(Physics2D.RaycastAll(point, yComponent, yComponent.magnitude))) {
          // we have a hit

          actualMovementDelta.y = 0;

          if (y > 0) {
            hitFlagsResult.Top = true;
          }
          if (y < 0) {
            hitFlagsResult.Bottom = true;
          }
        }
      }
    }

    transform.position += actualMovementDelta;

    return hitFlagsResult;
  }

  Vector3 calculateDesiredMovement() {
    var dx = 0;
    var dy = velocityY;

    if (Input.GetKey("w")) { dy += 1; }
    if (Input.GetKey("s")) { dy -= 1; }

    if (Input.GetKey("a")) { dx -= 1; }
    if (Input.GetKey("d")) { dx += 1; }

    return new Vector3(dx, dy, 0) * movementSpeed;
  }

  void Update() {
    velocityY -= 0.3f;

    var desiredMovement = calculateDesiredMovement();
    var hit = Move(desiredMovement);

    if (hit.HitAnything()) {
      velocityY = 0;
    }
  }
}
