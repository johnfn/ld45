using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FadeType {
  RectangleFade,
  Both
}

public enum DialogManagerState {
  Done,
  Fading,
  ShouldShowNextDialog,
  ShowingDialog
}

public class DialogManager: MonoBehaviour {
  public static DialogManager Instance;

  [Header("Smaller is slower, 0 is infinitly long")]
  public float FadeSpeed = 0.01f;

  private List<DialogEvent> currentSequence;

  private Fade fadeState;

  private Dialog currentDialogObject;

  private DialogManagerState state;

  private SpriteRenderer rectangleFade;
  private SpriteRenderer circleFade;

  void Awake() {
    Instance = this;
  }

  void Start() {
    rectangleFade = Manager.Instance.FullFade.GetComponent<SpriteRenderer>();
    circleFade = Manager.Instance.CircleFade.GetComponent<SpriteRenderer>();
  }

  void Fade() {
    if (Mathf.Abs(rectangleFade.color.a - fadeState.RectangleFadeOpacity) > 0.01f) {
      rectangleFade.color = new Color(1f, 1f, 1f, Mathf.Lerp(rectangleFade.color.a, fadeState.RectangleFadeOpacity, FadeSpeed));
      circleFade.color = new Color(1f, 1f, 1f, Mathf.Lerp(circleFade.color.a, fadeState.CircleFadeOpacity, FadeSpeed));
    } else {
      rectangleFade.color = new Color(1f, 1f, 1f, fadeState.RectangleFadeOpacity);
      circleFade.color = new Color(1f, 1f, 1f, fadeState.CircleFadeOpacity);

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

    if (currentDialogItem.Fade != null) {
      fadeState = currentDialogItem.Fade;
      state = DialogManagerState.Fading;

      return;
    }

    if (currentDialogItem.instruct != null) {
      Manager.Instance.SetInstruction(currentDialogItem.instruct);
    }

    if (currentDialogItem.hideInstruct == true) {
      Manager.Instance.HideInstruction();
    }

    if (currentDialogItem.ReceiveEmotion != EmotionType.None) {
      var emo = currentDialogItem.ReceiveEmotion;

      if (emo == EmotionType.AffectionCurrentlyUnused) {
        Manager.Instance.Player.Emotions.Affection = true;
      } else if (emo == EmotionType.BetrayalCurrentlyUnused) {
        Manager.Instance.Player.Emotions.Betrayal = true;
      } else if (emo == EmotionType.Curiosity) {
        Manager.Instance.Player.Emotions.Curiosity = true;
      } else if (emo == EmotionType.ForgivenessCurrentlyUnused) {
        Manager.Instance.Player.Emotions.Forgiveness = true;
      } else if (emo == EmotionType.RemorseCurrentlyUnused) {
        Manager.Instance.Player.Emotions.Remorse = true;
      } else {
        Debug.LogError("Uhhhhhhhhhhhh");
      }

      Debug.Log("TODO: Some sort of receive animation??!");

      state = DialogManagerState.ShouldShowNextDialog;

      return;
    }

    var characterName = currentDialogItem.Name;
    var speaker = Character.Speakers.First(guy => guy.CharacterName == characterName);

    currentDialogObject = Manager.CreateNewDialog(currentDialogItem.Contents, speaker.gameObject, currentDialogItem.EmotionReactions);

    state = DialogManagerState.ShowingDialog;
  }

  public void StartDialogSequence(List<DialogEvent> items) {
    ModeManager.SetGameMode(GameMode.Dialog);

    currentSequence = items;
    state = DialogManagerState.ShouldShowNextDialog;
  }
}