using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

  private List<DialogEvent> currentDialogTree;

  private Fade fadeState;

  private Dialog currentDialogObject;

  private DialogEvent currentDialogItem;

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
          var emotionResponse = currentDialogObject.getEmotionResponse();

          if (emotionResponse == -1) {
            if (currentDialogTree.Count == 0) {
              FinishDialogSequence();
            } else {
              state = DialogManagerState.ShouldShowNextDialog;
            }
          } else {
            var (emotionType, interactionString, nextDialogTree) = currentDialogItem.Responses[emotionResponse];

            currentDialogTree = nextDialogTree;
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

    currentDialogItem = currentDialogTree.First();

    if (currentDialogItem.Contents == "[CREDITS]") {
      SceneManager.LoadScene("Credits"); 

      return;
    }

    currentDialogTree = currentDialogTree.Skip(1).ToList();

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

      // if (emo == EmotionType.Affection) {
      //   Manager.Instance.Player.Emotions.Affection = true;
      // } else if (emo == EmotionType.Betrayal) {
      //   Manager.Instance.Player.Emotions.Betrayal = true;
      // } else if (emo == EmotionType.Curiosity) {
      //   Manager.Instance.Player.Emotions.Curiosity = true;
      // } else if (emo == EmotionType.Forgiveness) {
      //   Manager.Instance.Player.Emotions.Forgiveness = true;
      // } else if (emo == EmotionType.Remorse) {
      //   Manager.Instance.Player.Emotions.Remorse = true;
      // } else {
      //   Debug.LogError("Uhhhhhhhhhhhh");
      // }
      bool didTeachEmotion = TeachEmotion(emo);
      if (didTeachEmotion) {
        Debug.Log("TODO: Some sort of receive animation??!");
      } else {
        Debug.LogError("Uhhhhhhhh did not teach any emotion.");
      }

      state = DialogManagerState.ShouldShowNextDialog;
      return;
    }

    var characterName = currentDialogItem.Name;
    var speaker = Character.Speakers
      .OrderBy(x => Util.Distance(x.gameObject, Manager.Instance.Player.gameObject))
      .First(guy => guy.CharacterName == characterName);

    currentDialogObject = Manager.CreateNewDialog(currentDialogItem.Contents, speaker.gameObject, currentDialogItem.Responses);

    state = DialogManagerState.ShowingDialog;
  }

  /// Teach emotion to player.
  /// Returns boolean: whether or not anything changed.
  bool TeachEmotion(EmotionType emotion) {
    if (emotion == EmotionType.None) {
      return false;
    }
    bool didTeachEmotion = false;
    switch (emotion) {
      case EmotionType.Curiosity:
        Manager.Instance.Player.Emotions.Curiosity = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Compassion:
        Manager.Instance.Player.Emotions.Compassion = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Affection:
        Manager.Instance.Player.Emotions.Affection = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Remorse:
        Manager.Instance.Player.Emotions.Remorse = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Betrayal:
        Manager.Instance.Player.Emotions.Betrayal = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Forgiveness:
        Manager.Instance.Player.Emotions.Forgiveness = true;
        didTeachEmotion = true;
        break;
    }
    return didTeachEmotion;
  }

  public void StartDialogSequence(List<DialogEvent> items) {
    ModeManager.SetGameMode(GameMode.Dialog);

    currentDialogTree = items;
    state = DialogManagerState.ShouldShowNextDialog;
  }
}