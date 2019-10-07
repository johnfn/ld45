using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogOption: MonoBehaviour {
  public Text ReactionText;
  public GameObject CuriosityIcon;

  void Start() {
      
  }

  void Update() {

  }

  public void SetReactionText(string text) {
    ReactionText.text = text;
  }

  public void SetReactionIconType(EmotionType emotionType) {
    // TODO
  }
}