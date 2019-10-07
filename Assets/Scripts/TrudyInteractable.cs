using System.Collections.Generic;
using UnityEngine;

public class TrudyInteractable: Interactable {
  override public void ShowAsInteractable() {
      interactIndicator = Instantiate(Manager.Instance.interactIndicatorPrefab, new Vector3(xPos, transform.position.y + 3, 0), Quaternion.identity, Manager.Instance.interactIndicatorParent);
      interactIndicator.SetActive(true);

      Manager.Instance.SetInstruction("Press X to interact.");
    }

  override public void Interact() {
    if (!Manager.Instance.Player.Emotions.Affection) {
      Manager.Instance.Player.Emotions.Affection = true;
      DialogManager.Instance.StartDialogSequence(DialogText.Trudialog);
    } else {
      DialogManager.Instance.StartDialogSequence(DialogText.Trudialog2);
    }
  }

  void Update() {

  }
}