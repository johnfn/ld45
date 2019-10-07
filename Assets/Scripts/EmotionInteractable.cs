using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractable: Interactable {
    public float ScaleFactor = 1.3f;

    public EmotionType myEmotionType = EmotionType.Curiosity;

    /// Populated with the UI element when 
    private GameObject EmotionCue;

    void Awake() {
    }

    override public void ShowAsInteractable() {
        transform.localScale = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        EmotionCue = Manager.Instance.Player.ShowEmotionCue(myEmotionType);
    }

    override public void ShowAsNormal() {
        transform.localScale = new Vector3(1, 1, 1);
        if (EmotionCue != null) {
            Object.Destroy(EmotionCue);
        }
    }

    override public void Interact() {
        // Call HudEmotionCue.SetActive somehow
        if (EmotionCue != null) {
            HudEmotionCue HudEmotionCue = EmotionCue.GetComponent<HudEmotionCue>();
            HudEmotionCue.SetActive();
        }
    }

}