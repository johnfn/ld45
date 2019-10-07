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
        } else if (Lens.Instance.ActiveEmotion == EmotionType.Compassion) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "As you smooth out the paper, you wonder if the poem was written for Ash." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "​​You can feel the author’s overpowering feelings washing over your body.", },
                }
            );
        } else if (Lens.Instance.ActiveEmotion == EmotionType.Affection) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "​​The kindness and tenderness of the poem’s prose jumps out at you." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "​​Who wrote this poem for Ash? Unless…", },
                }
            );
        } else if (Lens.Instance.ActiveEmotion == EmotionType.Remorse) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "​​As you finger over the delicate corners of the pages, your heart feels heavy." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "​​The gravity of the author’s lament hits you at once.", },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "​​​​You suddenly realize you are a voyeur to someone else’s deeply personal work.", },
                }
            );
        } else {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "It's some old books." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "Doesn't look too interesting." }
                }
            );
        }
    }
}