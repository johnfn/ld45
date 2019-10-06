using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractable: Interactable {
    public EmotionType myEmotionType;

    void Start() {
        InteractableManager.Interactables.Add(this);
    }

    void OnDestroy() {
        InteractableManager.Interactables.Remove(this);
    }

    override public void ShowAsInteractable() {
        transform.localScale = new Vector3(2, 2, 2);
        Manager.Instance.Player.ShowEmotionCue(myEmotionType);
    }

    override public void ShowAsNormal() {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void Update() {
    }
}