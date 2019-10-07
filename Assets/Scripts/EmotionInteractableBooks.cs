using System.Collections.Generic;
using UnityEngine;

public class EmotionInteractableBooks: EmotionInteractable {
    private static string Ash(string s) {
        return $"<color=#999999>{ s }</color>";
    }

    override public void Interact() {
        base.Interact();

        if (Lens.Instance.ActiveEmotion == EmotionType.Curiosity) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "In this stack of books, you find a torn piece of paper with a poem on it." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = Ash("“Ashes to ash, dust to dust,"), },
                    new DialogEvent { Name = CharacterName.Blank, Contents = Ash("Eventually we all end up blind."), },
                    new DialogEvent { Name = CharacterName.Blank, Contents = Ash("But when that time comes, no need to be afraid"), },
                    new DialogEvent { Name = CharacterName.Blank, Contents = Ash("‘Cause I swear I won’t leave you behind.”"), },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "The signature at the bottom has been ripped off." },
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