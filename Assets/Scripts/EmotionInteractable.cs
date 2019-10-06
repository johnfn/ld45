using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractable: Interactable {
    public EmotionType myEmotionType;

    private GameObject EmotionCue;

    void Start() {
        InteractableManager.Interactables.Add(this);
    }

    void OnDestroy() {
        InteractableManager.Interactables.Remove(this);
    }

    override public void ShowAsInteractable() {
        transform.localScale = new Vector3(2, 2, 2);
        EmotionCue = Manager.Instance.Player.ShowEmotionCue(myEmotionType);
    }

    override public void ShowAsNormal() {
        transform.localScale = new Vector3(1, 1, 1);
        if (EmotionCue != null) {
            Object.Destroy(EmotionCue);
        }
    }

    void Update() {
    }
}