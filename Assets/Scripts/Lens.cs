using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lens: MonoBehaviour {
  private EmotionType activeEmotion = EmotionType.None;
  private Player player;
  private SpriteRenderer spriteRenderer;

  void Start() {
    player = Manager.Instance.Player;
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void CheckForNewEmotion() {
    EmotionType? newEmotion = null;

    if (Input.GetKeyDown("1") && player.Emotions.Curiosity) {
      newEmotion = EmotionType.Curiosity;
    }

    if (newEmotion != null) {
      if (activeEmotion == newEmotion) {
        activeEmotion = EmotionType.None;
      } else {
        activeEmotion = (EmotionType) newEmotion;
      }
    }
  }

  void Update() {
    if (ModeManager.Instance.CurrentGameMode != GameMode.Playing) {
      return;
    }

    CheckForNewEmotion();

    switch (activeEmotion) {
      case EmotionType.None:
        spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        break;
      case EmotionType.Curiosity:
        spriteRenderer.color = new Color(.3f, .6f, 1f, 0.3f);
        break;
    }
  }
}