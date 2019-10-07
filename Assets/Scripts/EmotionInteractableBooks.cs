using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractableBooks: EmotionInteractable {
    override public void Interact() {
        base.Interact();

        DialogManager.Instance.StartDialogSequence(DialogText.BookDialog);
    }
}