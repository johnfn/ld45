using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogState {
  Done,
  WritingText,
  FinishedAndWaitingForInput
}

public class Dialog: MonoBehaviour {
  public Canvas canvas;
  public Text text;
  public SpriteRenderer sprite;
  public Text ReactionText;
  public GameObject ReactionIcon;

  public float textScaleFactor;
  public float maxDialogWidth;

  private EmotionType selectedEmotionResponse = EmotionType.None;

  [Header("Smaller is faster")]
  public int textSpeed;

  private DialogState state;
  private string entireDialog;
  private int tick = 0;
  private List<(EmotionType, string, List<DialogEvent>)> emotionReactions = null;

  private string currentTagName = "";

  private string visibleDialog = "";

  private int index = 0;

  public EmotionType getEmotionResponse() {
    return selectedEmotionResponse;
  }

  char? getNextChar() {
    if (index >= entireDialog.Length) {
      return null;
    } else {
      var result = entireDialog[index];

      ++index;

      return result;
    }
  }

  public float Width { get { return this.sprite.bounds.size.x; } }

  public float Height { get { return this.sprite.bounds.size.y; } }

  private float TextWidth;
  private float TextHeight;

  void Start() {
    ReactionText.gameObject.SetActive(false);
    ReactionIcon.gameObject.SetActive(false);
  }

  void CalculateDialogSize(string dialog) {
    // Figure out how tall and wide this dialog should be

    var textGen = new TextGenerator();
    var generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size); 

    generationSettings.generationExtents = new Vector2(maxDialogWidth, 10000);

    TextWidth  = textGen.GetPreferredWidth(dialog, generationSettings);
    TextHeight = textGen.GetPreferredHeight(dialog, generationSettings);

    canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(
      TextWidth / textScaleFactor,
      TextHeight / textScaleFactor
    );

    sprite.size = new Vector2(
      TextWidth / textScaleFactor,
      TextHeight / textScaleFactor
    );
  }

  public DialogState GetDialogState() {
    return state;
  }

  public void StartDialog(
    string dialog, 
    List<(EmotionType, string, List<DialogEvent>)> emotionReactions = null
  ) {
    visibleDialog = "";
    entireDialog = dialog;

    CalculateDialogSize(dialog);

    state = DialogState.WritingText;

    text.text = "";
    this.emotionReactions = emotionReactions;
  }

  string GetCloseTagName(string tagName) {
    var result = "";

    foreach (var ch in tagName) {
      if (ch == '=') {
        break;
      }

      result += ch;
    }

    return result;
  }

  void WriteLetter() {
    var nextCharacter = getNextChar();

    if (nextCharacter == null) {
      Finish();

      return;
    }

    // read entire tag at once

    if (nextCharacter == '<') {
      var nextTagName = "";

      nextCharacter = getNextChar();

      while (nextCharacter != '>') {
        nextTagName += nextCharacter;
        nextCharacter = getNextChar();

        if (nextCharacter == null) {
          throw new System.Exception("no close tag found");
        }
      }

      if (nextTagName[0] != '/') {
        // <color=red>

        visibleDialog += $"<{ nextTagName }>"; // e.g. <color=red>
        currentTagName = GetCloseTagName(nextTagName); // e.g. color
      } else {
        // </color>

        visibleDialog += $"</{ currentTagName }>";
        currentTagName = "";
      }
    } else {
      visibleDialog += nextCharacter;
    }
  }

  void Finish() {
    this.state = DialogState.FinishedAndWaitingForInput;

    currentTagName = "";
    visibleDialog = entireDialog;

    if (emotionReactions != null) {
      var count = 0f;

      foreach (var (emotionType, interactionText, dialogTree) in emotionReactions) {
        count++;

        var dialogOptionGO = GameObject.Instantiate(
          Manager.Instance.DialogReactionPrefab,
          text.transform.position + new Vector3(
            0f,
            TextHeight * 0.02f - 1f + count * -1f,
            0f
          ),
          Quaternion.identity,
          canvas.transform
        );

        dialogOptionGO.GetComponent<DialogOption>().SetReactionText($"{ count }: { interactionText }");
        dialogOptionGO.GetComponent<DialogOption>().SetReactionIconType(emotionType);
      }
    }
  }

  void UpdateText() {
    if (!text) {
      return;
    }

    var newText = "";

    if (currentTagName != "") {
      newText = $"{ visibleDialog }</{ currentTagName }>";
    } else {
      newText = visibleDialog;
    }

    text.text = newText;
  }

  void CheckForEmotionReaction() {
    if (emotionReactions == null) {
      return;
    }

    var number = 1;

    foreach (var (emotionType, interactionName, nextDialogTree) in emotionReactions) {
      if (Input.GetKeyDown(number.ToString())) {
        state = DialogState.Done;
        selectedEmotionResponse = emotionType;
        break;
      }

      number++;
    }
  }

  void Update() {
    ++tick;

    UpdateText();

    switch (state) {
      case DialogState.Done:
        break;
      case DialogState.WritingText:
        if (Input.GetKeyDown("x")) {
          Finish();
        } else if (tick % textSpeed == 0) {
          WriteLetter();
        }

        break;
      case DialogState.FinishedAndWaitingForInput:
        if (
          Input.GetKeyDown("x") && 
          (emotionReactions == null || emotionReactions.Count == 0)
        ) {
          state = DialogState.Done;
        }

        CheckForEmotionReaction();

        break;
    }
  }
}
