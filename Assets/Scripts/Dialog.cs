using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum TextState {
  Empty,
  WritingText,
  WaitingForInput
}

public class Dialog: MonoBehaviour {
  public Canvas canvas;
  public Text text;
  public SpriteRenderer sprite;

  public float textScaleFactor;
  public float maxDialogWidth;

  [Header("Smaller is faster")]
  public int textSpeed;

  private TextState state;
  private string entireDialog;
  private int tick = 0;

  public float Width { get { return this.sprite.bounds.size.x; } }

  public float Height { get { return this.sprite.bounds.size.y; } }

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

  public void StartDialog(string dialog) {
    text.text = "";
    entireDialog = dialog;

    CalculateDialogSize(dialog);

    state = TextState.WritingText;
  }

  void WriteLetter() {
    var currentDialog = text.text;

    if (currentDialog == this.entireDialog) {
      this.state = TextState.WaitingForInput;

      return;
    }

    var nextCharacter = entireDialog[currentDialog.Length];
    text.text += nextCharacter;
  }

  void Update() {
    ++tick;

    switch (state) {
      case TextState.Empty:
        break;
      case TextState.WritingText:
        if (tick % textSpeed == 0) {
          WriteLetter();
        }

        break;
      case TextState.WaitingForInput:
        break;
    }
  }
}
