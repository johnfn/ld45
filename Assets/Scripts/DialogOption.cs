using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogOption: MonoBehaviour {
  public Text ReactionText;
  public GameObject CuriosityIcon;

  void Update() {

  }

  public void SetReactionText(string text) {
    ReactionText.text = text;
  }

  public void SetReactionIconType(EmotionType emotionType) {
    CuriosityIcon.SetActive(false);

    if (emotionType == EmotionType.Curiosity) {
      CuriosityIcon.SetActive(true);
    }
  }
}