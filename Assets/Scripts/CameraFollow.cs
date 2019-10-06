using UnityEngine;

public class CameraFollow: MonoBehaviour {
  public float smoothing;
  public GameObject player;

  private Camera followCamera;

  void Start() {
    followCamera = GetComponent<Camera>();

    // immediately snap cam to player at beginning of game 

    followCamera.transform.position = new Vector3(
      x: player.transform.position.x,
      y: player.transform.position.y,
      z: followCamera.transform.position.z
    );
  }

  void FixedUpdate() {
    var desiredPosition = new Vector3(
      x: player.transform.position.x,
      y: player.transform.position.y,
      z: followCamera.transform.position.z
    );

    followCamera.transform.position = new Vector3(
      x: Mathf.Lerp(followCamera.transform.position.x, desiredPosition.x, smoothing),
      y: Mathf.Lerp(followCamera.transform.position.y, desiredPosition.y, smoothing),

      z: followCamera.transform.position.z
    );
  }
}