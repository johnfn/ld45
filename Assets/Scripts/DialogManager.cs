using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DialogManagerState {
  Done,
  ShouldShowNextDialog,
  ShowingDialog
}

public class DialogManager: MonoBehaviour {
  public static DialogManager Instance;

  private List<DialogItem> currentSequence;

  private Dialog currentObject;

  private DialogManagerState state;

  void Awake() {
    Instance = this;
  }

  void Update() {
    switch (state) {
      case DialogManagerState.Done:
        break;
      case DialogManagerState.ShouldShowNextDialog:
        ShowNextDialog();

        break;
      case DialogManagerState.ShowingDialog:
        if (currentObject.GetDialogState() == DialogState.Done) {
          if (currentSequence.Count == 0) {
            FinishDialogSequence();
          } else {
            state = DialogManagerState.ShouldShowNextDialog;
          }
        }

        break;
    }
  }

  void FinishDialogSequence() {
    if (currentObject != null) {
      GameObject.Destroy(currentObject.gameObject);
    }

    state = DialogManagerState.Done;
  }

  void ShowNextDialog() {
    if (currentObject != null) {
      GameObject.Destroy(currentObject.gameObject);
    }

    var currentDialogItem = currentSequence.First();
    var characterName = currentDialogItem.Name;

    currentSequence = currentSequence.Skip(1).ToList();

    var speaker = Character.Speakers.First(guy => guy.CharacterName == characterName);

    currentObject = Manager.CreateNewDialog(currentDialogItem.Contents, speaker.gameObject);

    state = DialogManagerState.ShowingDialog;
  }

  public void StartDialogSequence(List<DialogItem> items) {
    currentSequence = items;
    state = DialogManagerState.ShouldShowNextDialog;
  }
}