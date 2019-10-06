using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractable: Interactable {
    public EmotionType myEmotionType;

    private GameObject EmotionCue;

    /*
    (the base class handles these for you)

    void Start() {
        InteractableManager.Interactables.Add(this);
    }

    void OnDestroy() {
        InteractableManager.Interactables.Remove(this);
    } 
    */

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

    override public void Interact() {
        Util.Log("My name is david and i should implement this");
    }

    void Update() {
    }
}