using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HudEmotionCueState {
    // Showing from proximity to an EmotionInteractable
    Visible,
    // Upon interaction
    Active,
}

public class HudEmotionCue: MonoBehaviour {

    private readonly float velocityX = 0f;

    // Velocity will never go negative
    private float velocityY;

    private float accelY;

    private HudEmotionCueState State;

    /* Movement */

    void Awake() {
        State = HudEmotionCueState.Visible;
        // Starts with upwards velocity
        velocityY = 0.3f;
        // ...and negative accel
        accelY = -0.1f;
    }

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

    /* Other */

    public void SetActive() {
        State = HudEmotionCueState.Active;
        Util.Log("hud emotion cue active. insert fadeout here.");
    }

    /// Handle Active state.
    private void UpdateActive() {
        float scale = 1.4f;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void UpdateVisible() {
        Move(new Vector3(velocityX, velocityY, 0));
        Accelerate();
        Jerk();
    }

    void Update() {
        switch (State) {
            case HudEmotionCueState.Visible:
                UpdateVisible();
                break;
            case HudEmotionCueState.Active:
                UpdateActive();
                break;
        }
        
    }
}