using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractablePlant: EmotionInteractable {
    private static string Ash(string s) {
        return $"<color=#999999>{ s }</color>";
    }

    override public void Interact() {
        base.Interact();

        if (Lens.Instance.ActiveEmotion == EmotionType.Curiosity) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "An ordinary potted plant." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "You stick your finger into the leaves to see if it will react." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "To your relief (and disappointment), it doesn’t try to bite you." },
                }
            );
        } else {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "This is just a plant in a pot." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "Or is the pot on the plant?" },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "Hm." }
                }
            );
        }
    }
}