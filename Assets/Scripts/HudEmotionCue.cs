using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HudEmotionCue: MonoBehaviour {

    private readonly float velocityX = 0f;

    // Velocity will never go negative
    private float velocityY = 0.3f;

    private float accelY = -0.1f;
    
    // It's a UI element, so it doesn't collide with anything.
    void Move(Vector3 deltaPositon) {
        transform.position += deltaPositon;
        Util.Log(velocityX, velocityY);
    }

    void Accelerate(Vector3 deltaVelocity) {
        
    }

    void Update() {
        Util.Log("Update");
        Move(new Vector3(velocityX, velocityY, 0));
        if (velocityY > 0) {
            velocityY -= Mathf.Min(velocityY, 0.1f);
        }
    }
}