using System.Collections.Generic;
using UnityEngine;

public class AshFourInteractable: FpcInteractable {
  override public void Interact() {
    DialogManager.Instance.StartDialogSequence(DialogText.AshFour);
  }
}