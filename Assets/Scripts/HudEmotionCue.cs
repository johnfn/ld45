using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HudEmotionCue: MonoBehaviour {

    private readonly float velocityX = 0f;

    // Velocity will never go negative
    private float velocityY = 0.3f;

    private float accelY = -0.1f;

    /* Main */

    /** 
     * Change position by velocity.
     * It's a UI element, so it doesn't collide with anything.
     */
    void Move(Vector3 deltaPositon) {
        transform.position += deltaPositon;
        Util.Log(velocityX, velocityY);
    }

    /** Change velocity by acceleration. */
    void Accelerate() {
        velocityY = Util.NonNegative(velocityY + accelY);
    }

    /** Change acceleration by jerk. */
    void Jerk() {
        accelY = Util.NonPositive(accelY + 0.02f);
    }

    void Update() {
        Move(new Vector3(velocityX, velocityY, 0));
        Accelerate();
        Jerk();
    }
}