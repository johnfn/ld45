using System.Collections.Generic;
using UnityEngine;

public class AshThreeInteractable: FpcInteractable {
  override public void Interact() {
    DialogManager.Instance.StartDialogSequence(DialogText.AshThree);

    Manager.Instance.Player.Emotions.Compassion = true;
  }
}