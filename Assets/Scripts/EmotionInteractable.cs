using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractable: Interactable {
    public EmotionType MyEmotionType;

    void Start() {
        InteractableManager.Interactables.Add(this);
    }

    void OnDestroy() {
        InteractableManager.Interactables.Remove(this);
    }

    new public void ShowAsInteractable() {
        transform.localScale = new Vector3(2, 2, 2);
        Canvas CharacterCanvas = Manager.Instance.Player.character.CharacterCanvas;
    }

    new public void ShowAsNormal() {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void Update() {
    }
}