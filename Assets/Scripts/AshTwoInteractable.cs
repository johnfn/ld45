using System.Collections.Generic;
using UnityEngine;

public class AshTwoInteractable: FpcInteractable {
    private bool interacted = false;

    override public void Interact() {
        if (!interacted)
        {
            interacted = true;
            DialogManager.Instance.StartDialogSequence(DialogText.AshTwo);
        }
        
    }
}