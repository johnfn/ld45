using System.Collections.Generic;
using UnityEngine;

public class TrudyInteractable : Interactable {

    override public void ShowAsInteractable() {
        interactIndicator = Instantiate(Manager.Instance.interactIndicatorPrefab, new Vector3(xPos, transform.position.y + 3, 0), Quaternion.identity, Manager.Instance.interactIndicatorParent);
        interactIndicator.SetActive(true);
    }

    /*
  override public void ShowAsNormal() {
    transform.localScale = new Vector3(0.25f, 0.25f, 1);
  }*/

  override public void Interact() {
    DialogManager.Instance.StartDialogSequence(DialogText.Trudialog);
  }

  void Update() {

  }
}