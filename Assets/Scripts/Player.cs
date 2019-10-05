using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {
  public float movementSpeed = 5;

  void Start() {

  }

  void Update() {
    var dx = 0;
    var dy = 0;

    if (Input.GetKey("w")) { dy += 1; }
    if (Input.GetKey("s")) { dy -= 1; }

    if (Input.GetKey("a")) { dx -= 1; }
    if (Input.GetKey("d")) { dx += 1; }

    transform.position += new Vector3(dx, dy, 0) * movementSpeed;
  }
}
