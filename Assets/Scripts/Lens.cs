using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lens: MonoBehaviour {
  [HideInInspector]
  public EmotionType ActiveEmotion = EmotionType.None;

  private Player player;
  private SpriteRenderer spriteRenderer;

  public static Lens Instance;

  public GameObject LensNameObject;

  void Start() {
    Instance = this;

    player = Manager.Instance.Player;
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void CheckForNewEmotion() {
    EmotionType? newEmotion = null;

    if (Input.GetKeyDown("1") && player.Emotions.Curiosity) {
      newEmotion = EmotionType.Curiosity;
    }

    if (Input.GetKeyDown("2") && player.Emotions.Compassion) {
      newEmotion = EmotionType.Compassion;
    }

    if (newEmotion != null) {
      if (ActiveEmotion == newEmotion) {
        ActiveEmotion = EmotionType.None;
      } else {
        ActiveEmotion = (EmotionType) newEmotion;
      }
    }
  }

  void Update() {
    if (ModeManager.Instance.CurrentGameMode != GameMode.Playing) {
      return;
    }

    CheckForNewEmotion();

    var go = LensNameObject.gameObject.GetComponent<TextMeshProUGUI>();

    switch (ActiveEmotion) {
      case EmotionType.None:
        spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        go.text = "";

        break;
      case EmotionType.Curiosity:
        spriteRenderer.color = new Color(.3f, .6f, 1f, 0.3f);
        go.text = "Curiosity Lens";

        break;
      case EmotionType.Compassion:
        spriteRenderer.color = new Color(1f, .7f, .7f, 0.3f);
        go.text = "Compassion Lens";

        break;
    }
  }
}