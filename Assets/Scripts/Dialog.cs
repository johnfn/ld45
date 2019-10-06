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

  [Header("Smaller is faster")]
  public int textSpeed;

  private DialogState state;
  private string entireDialog;
  private int tick = 0;
  private Dictionary<EmotionType, List<DialogEvent>> emotionReactions;

  private string currentTagName = "";

  private string visibleDialog = "";

  private int index = 0;

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

  void Start() {
    ReactionText.gameObject.SetActive(false);
    ReactionIcon.gameObject.SetActive(false);
  }

  void CalculateDialogSize(string dialog) {
    // Figure out how tall and wide this dialog should be

    var textGen = new TextGenerator();
    var generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size); 

    generationSettings.generationExtents = new Vector2(maxDialogWidth, 10000);

    float width = textGen.GetPreferredWidth(dialog, generationSettings);
    float height = textGen.GetPreferredHeight(dialog, generationSettings);

    canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(
      width / textScaleFactor,
      height / textScaleFactor
    );

    sprite.size = new Vector2(
      width / textScaleFactor,
      height / textScaleFactor
    );
  }

  public DialogState GetDialogState() {
    return state;
  }

  public void StartDialog(string dialog, Dictionary<EmotionType, List<DialogEvent>> emotionReactions = null) {
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

    if (this.emotionReactions != null) {
      foreach (var reaction in emotionReactions) {
        ReactionText.gameObject.SetActive(true);
        ReactionIcon.gameObject.SetActive(true);

        ReactionText.text = $"z: { reaction.Key.ToString() }";
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
        if (Input.GetKeyDown("x")) {
          state = DialogState.Done;
        }

        break;
    }
  }
}
