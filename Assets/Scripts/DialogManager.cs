using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DialogManagerState {
  NoDialog,
  ShouldShowNextDialog,
  ShowingDialog
}

public struct DialogItem {
  public GameObject Speaker;
  public string Contents;
}

public class DialogManager: MonoBehaviour {
  public static DialogManager Instance;

  private List<DialogItem> currentDialogSequence;

  private DialogManagerState state;

  void Awake() {
    Instance = this;
  }

  void Update() {
    switch (state) {
      case DialogManagerState.NoDialog:
        break;
      case DialogManagerState.ShouldShowNextDialog:
        ShowNextDialog();
        break;
      case DialogManagerState.ShowingDialog:
        // TODO: Check to see if we should show the next one
        break;
    }
  }

  void ShowNextDialog() {
    var currentDialogItem = currentDialogSequence[0];
    var speaker = currentDialogItem.Speaker;

    Manager.CreateNewDialog(currentDialogItem.Contents, speaker);

    state = DialogManagerState.ShowingDialog;
  }

  public void StartDialogSequence(List<DialogItem> items) {
    currentDialogSequence = items;
    state = DialogManagerState.ShouldShowNextDialog;
  }
}