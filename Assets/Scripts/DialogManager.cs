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

  private Dialog currentDialogObject;

  private DialogManagerState state;

  void Awake() {
    Instance = this;
  }

  void Fade() {
    if (Mathf.Abs(fadeState.FadeCurrent - fadeState.FadeGoal) > 0.01f) {
      fadeState.FadeCurrent = Mathf.Lerp(fadeState.FadeCurrent, fadeState.FadeGoal, 0.1f);

      Manager.Instance.FullFade.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fadeState.FadeCurrent);
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
        if (currentDialogObject.GetDialogState() == DialogState.Done) {
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
    if (currentDialogObject != null) {
      GameObject.Destroy(currentDialogObject.gameObject);
    }

    state = DialogManagerState.Done;

    ModeManager.SetGameMode(GameMode.Playing);
  }

  void ShowNextDialog() {
    if (currentDialogObject != null) {
      GameObject.Destroy(currentDialogObject.gameObject);
    }

    var currentDialogItem = currentSequence.First();

    currentSequence = currentSequence.Skip(1).ToList();

    if (currentDialogItem.SpecialEvent == SpecialDialogEvent.FadeToBlack) {
      fadeState = new FadeState {
        FadeCurrent = 0f,
        FadeGoal = 1f
      };

      state = DialogManagerState.Fading;

      return;
    } else if (currentDialogItem.SpecialEvent == SpecialDialogEvent.FadeToFiftyPercent) {
      fadeState = new FadeState {
        FadeCurrent = 0f,
        FadeGoal = 0.5f
      };

      state = DialogManagerState.Fading;

      return;
    } else if (currentDialogItem.SpecialEvent == SpecialDialogEvent.SnapToBlack) {
      Manager.Instance.FullFade.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

      return;
    }

    var characterName = currentDialogItem.Name;

    var speaker = Character.Speakers.First(guy => guy.CharacterName == characterName);

    currentDialogObject = Manager.CreateNewDialog(currentDialogItem.Contents, speaker.gameObject);

    state = DialogManagerState.ShowingDialog;
  }

  public void StartDialogSequence(List<DialogItem> items) {
    ModeManager.SetGameMode(GameMode.Dialog);

    currentSequence = items;
    state = DialogManagerState.ShouldShowNextDialog;
  }
}