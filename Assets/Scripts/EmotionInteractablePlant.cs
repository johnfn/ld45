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
        } else if (Lens.Instance.ActiveEmotion == EmotionType.Compassion) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "You rotate the pot so the sunlight is more equally distributed across the plant’s leaves.", },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "The plant itself doesn’t react, but you suspect that it’s pleased.", },
                }
            );
        } else if (Lens.Instance.ActiveEmotion == EmotionType.Affection) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "You tell the plant it has cute leaves." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "Maybe it’s just your imagination, but the plant seems to turn slightly redder." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "Is it blushing?" },
                }
            );
        } else if (Lens.Instance.ActiveEmotion == EmotionType.Remorse) {
            DialogManager.Instance.StartDialogSequence(
                new List<DialogEvent> {
                    new DialogEvent { Name = CharacterName.Blank, Contents = "You take a step back and observe the plant from a distance." },
                    new DialogEvent { Name = CharacterName.Blank, Contents = "It seems to appreciate your respect for its personal space." },
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