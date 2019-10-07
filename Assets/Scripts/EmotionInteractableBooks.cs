using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractableBooks: EmotionInteractable {
    override public void Interact() {
        base.Interact();

        if (Lens.Instance.ActiveEmotion == EmotionType.Curiosity) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "WHOA! These books are AWESOME!" }
                }
            );
        } else {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "It's some old books." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "Nothing too interesting." }
                }
            );
        }
    }
}