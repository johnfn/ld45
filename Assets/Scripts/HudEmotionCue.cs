using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HudEmotionCueState {
    // Animating in
    Mounting,

    // Idle
    Idle,
    // Upon interaction
    Active,
    Unmounted,
}

public class HudEmotionCue: MonoBehaviour {

    private readonly float velocityX = 0f;

    // Velocity will never go negative
    private float velocityY;

    private float accelY;

    private HudEmotionCueState State;

    void Awake() {
        State = HudEmotionCueState.Mounting;
        // Mounting vars: starts with upwards velocity; negative accel
        velocityY = 0.3f;
        accelY = -0.1f;
    }

    /* Mounting */

    /// Change position by velocity.
    void Move(Vector3 deltaPositon) {
        transform.position += deltaPositon;
        // It's a UI element, so no need to worry about collision
    }

    /// Change velocity by acceleration.
    void Accelerate() {
        velocityY = Util.NonNegative(velocityY + accelY);
    }

    /// Change acceleration by jerk.
    void Jerk() {
        accelY = Util.NonPositive(accelY + 0.02f);
    }

    private void UpdateMounting() {
        Move(new Vector3(velocityX, velocityY, 0));
        Accelerate();
        Jerk();
        if (accelY == 0) {
            SetIdle();
        }
    }

    /* Idle */

    public void SetIdle() {
        Util.Log("Setting idle");
        State = HudEmotionCueState.Idle;
    }

    /* Active */

    public void SetActive() {
        State = HudEmotionCueState.Active;
        Util.Log("hud emotion cue active. insert fadeout here.");
        // Setup

    }

    private void UpdateActive() {
        
        float scale = 1.4f;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update() {
        switch (State) {
            case HudEmotionCueState.Mounting:
                UpdateMounting();
                break;
            case HudEmotionCueState.Idle:
                break;
            case HudEmotionCueState.Active:
                UpdateActive();
                break;
            case HudEmotionCueState.Unmounted:
                Object.Destroy(gameObject);
                break;
        }
        
    }
}