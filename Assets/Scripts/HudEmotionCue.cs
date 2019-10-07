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

/// Represents a HUD element that is born when the player is near an Emotion Interactable.
[RequireComponent(typeof(CanvasGroup))]
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
        State = HudEmotionCueState.Idle;
    }

    /* Active */

    public void SetActive() {
        State = HudEmotionCueState.Active;
        // Tweens
        float animTime = 0.16f;

        // Fade-out element
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, animTime)
            .setEaseOutQuad()
            .setOnComplete(() => { State = HudEmotionCueState.Unmounted; });

        // Fade-out element's sprites
        gameObject
            .GetComponentsInChildren<SpriteRenderer>()
            .ToList()
            .ForEach(sprite => {
                sprite.color = new Color(1f, 1f, 1f, 1f);
                LeanTween.alpha(sprite.gameObject, 0, animTime)
                    .setEaseOutQuad()
                    .setOnComplete(() => { State = HudEmotionCueState.Unmounted; });
            });

        // Scale-up
        float targetScale = 1.66f;
        LeanTween.scale(gameObject, new Vector3(targetScale, targetScale, targetScale), animTime)
            .setEaseOutExpo();
    }

    void Update() {
        switch (State) {
            case HudEmotionCueState.Mounting:
                UpdateMounting();
                break;
            case HudEmotionCueState.Idle:
            case HudEmotionCueState.Active:
                break;
            case HudEmotionCueState.Unmounted:
                Object.Destroy(gameObject);
                break;
        }
        
    }
}