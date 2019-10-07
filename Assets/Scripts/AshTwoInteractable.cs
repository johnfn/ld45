using System.Collections.Generic;
using UnityEngine;

public class AshTwoInteractable: FpcInteractable {
  override public void Interact() {
    DialogManager.Instance.StartDialogSequence(DialogText.AshTwo);
  }
}