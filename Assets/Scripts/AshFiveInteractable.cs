using System.Collections.Generic;
using UnityEngine;

public class AshFiveInteractable: FpcInteractable {

  override public void Interact() {
    var player = Manager.Instance.Player;

    if (player.Emotions.BasicallyHasThemAll()) {
      DialogManager.Instance.StartDialogSequence(DialogText.AshSix);
    } else {
      DialogManager.Instance.StartDialogSequence(DialogText.AshFive);
    }
  }

  void Update() {
    var player = Manager.Instance.Player;
    var dist = Util.Distance(player.gameObject, gameObject);

    MusicManager.Instance.NHAsh.volume = dist < 2f ? 1f : (2f / dist);
  }
}