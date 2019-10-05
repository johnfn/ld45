using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum TextState {
  WritingText,
  WaitingForInput
}

public class Dialog: MonoBehaviour {
  public Canvas canvas;
  public Text text;
  public SpriteRenderer sprite;

  public float textScaleFactor = 100;

  void Start() {
    ShowDialog("This is some sample text that is too long to fit in a single line probably...");
  }

  void ShowDialog(string dialog) {
    // Figure out how tall and wide this dialog should be

    var textGen = new TextGenerator();
    var generationSettings = text.GetGenerationSettings(text.rectTransform.rect.size); 

    float width = textGen.GetPreferredWidth(dialog, generationSettings);

    if (width > 400) {

    }

    float height = textGen.GetPreferredHeight(dialog, generationSettings);

    canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(
      width / textScaleFactor,
      height / textScaleFactor
    );

    sprite.size = new Vector2(
      width / textScaleFactor,
      height / textScaleFactor
    );

    text.text = dialog;
  }
}
