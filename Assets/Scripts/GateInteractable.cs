using System.Collections.Generic;
using UnityEngine;

public class GateInteractable: Interactable {
  override public void ShowAsInteractable() {
      interactIndicator = Instantiate(Manager.Instance.interactIndicatorPrefab, new Vector3(xPos, transform.position.y + 3, 0), Quaternion.identity, Manager.Instance.interactIndicatorParent);
      interactIndicator.SetActive(true);

      Manager.Instance.SetInstruction("Press X to interact.");
    }

  override public void Interact() {
    if (!Manager.Instance.Player.Emotions.BasicallyHasThemAll()) {
      DialogManager.Instance.StartDialogSequence(DialogText.GateNoGood);
    } else {
      MusicManager.Instance.Betray();
      DialogManager.Instance.StartDialogSequence(DialogText.GateGood);
    }
  }

  void Update() {

  }
}