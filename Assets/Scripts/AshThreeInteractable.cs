using System.Collections.Generic;
using UnityEngine;

public class AshThreeInteractable: FpcInteractable {

    private bool interacted = false;

  override public void Interact() {
        if (!interacted)
        {
            interacted = true;
            DialogManager.Instance.StartDialogSequence(DialogText.AshThree);

            Manager.Instance.Player.Emotions.Compassion = true;
        }
  }
}