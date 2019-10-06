using UnityEngine;

/** Camera follows the player. */
public class CameraFollow: MonoBehaviour {
  public float smoothing = 0.15f;
  private Player player;

  private Camera followCamera;

  private Vector3 effectiveLookingDirection = Vector3.zero;

  void Start() {
    followCamera = GetComponent<Camera>();
    player = Manager.Instance.Player;

    // Immediately snap cam to player at beginning of game 
    followCamera.transform.position = new Vector3(
      x: player.transform.position.x,
      y: player.transform.position.y,
      z: followCamera.transform.position.z
    );
  }

  void FixedUpdate() {
    effectiveLookingDirection = new Vector3(
      Mathf.Lerp(effectiveLookingDirection.x, player.GetLookingDirection().x, 0.70f),
      Mathf.Lerp(effectiveLookingDirection.y, player.GetLookingDirection().y, 0.70f),
      Mathf.Lerp(effectiveLookingDirection.z, player.GetLookingDirection().z, 0.70f)
    );

    var desiredPosition = new Vector3(
      x: player.transform.position.x,
      y: player.transform.position.y,
      z: followCamera.transform.position.z
    ) + effectiveLookingDirection * 1.45f;

    followCamera.transform.position = new Vector3(
      x: Mathf.Lerp(followCamera.transform.position.x, desiredPosition.x, smoothing),
      y: Mathf.Lerp(followCamera.transform.position.y, desiredPosition.y, smoothing),

      z: followCamera.transform.position.z
    );
  }
}