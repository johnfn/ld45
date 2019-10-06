using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FadeState {
  public float FadeCurrent;

  public float FadeGoal;
}

public enum DialogManagerState {
  Done,
  Fading,
  ShouldShowNextDialog,
  ShowingDialog
}

public class DialogManager: MonoBehaviour {
  public static DialogManager Instance;

  private List<DialogItem> currentSequence;

  private FadeState fadeState;

  private Dialog currentObject;

  private DialogManagerState state;

  void Awake() {
    Instance = this;
  }

  void Fade() {
    if (Mathf.Abs(fadeState.FadeCurrent - fadeState.FadeGoal) > 0.01f) {
      fadeState.FadeCurrent = Mathf.Lerp(fadeState.FadeCurrent, fadeState.FadeGoal, 0.1f);

      Debug.Log(fadeState.FadeCurrent);
    } else {
      state = DialogManagerState.ShouldShowNextDialog;
    }
  }

  void Update() {
    switch (state) {
      case DialogManagerState.Done:
        break;
      case DialogManagerState.Fading:
        Fade();

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

    ModeManager.SetGameMode(GameMode.Playing);
  }

  void ShowNextDialog() {
    if (currentObject != null) {
      GameObject.Destroy(currentObject.gameObject);
    }

    var currentDialogItem = currentSequence.First();

    if (currentDialogItem.SpecialEvent == SpecialDialogEvent.FadeToBlack) {
      fadeState = new FadeState {
        FadeCurrent = 0f,
        FadeGoal = 1f
      };

      state = DialogManagerState.Fading;

      return;
    }

    var characterName = currentDialogItem.Name;

    currentSequence = currentSequence.Skip(1).ToList();

    var speaker = Character.Speakers.First(guy => guy.CharacterName == characterName);

    currentObject = Manager.CreateNewDialog(currentDialogItem.Contents, speaker.gameObject);

    state = DialogManagerState.ShowingDialog;
  }

  public void StartDialogSequence(List<DialogItem> items) {
    ModeManager.SetGameMode(GameMode.Dialog);

    currentSequence = items;
    state = DialogManagerState.ShouldShowNextDialog;
  }
}