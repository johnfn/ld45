using System.Collections.Generic;
using UnityEngine;

public class TrudyInteractable : Interactable {
  override public void ShowAsInteractable() {
    transform.localScale = new Vector3(0.3f, 0.3f, 1);
  }

  override public void ShowAsNormal() {
    transform.localScale = new Vector3(0.25f, 0.25f, 1);
  }

  override public void Interact() {
    DialogManager.Instance.StartDialogSequence(DialogText.Trudialog);
  }

  void Update() {

  }
}