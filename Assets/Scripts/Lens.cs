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

    if (Input.GetKeyDown("3") && player.Emotions.Affection) {
      newEmotion = EmotionType.Affection;
    }

    if (Input.GetKeyDown("4") && player.Emotions.Remorse) {
      newEmotion = EmotionType.Remorse;
    }

    if (newEmotion != null) {
      Stem stem = Stem.None;

      if (newEmotion == EmotionType.Affection) {
        stem = Stem.Affection;
      } else if (newEmotion == EmotionType.Compassion) {
        stem = Stem.Compassion;
      } else if (newEmotion == EmotionType.Remorse) {
        stem = Stem.Remorse;
      }

      if (ActiveEmotion == newEmotion) {
        ActiveEmotion = EmotionType.None;

        if (stem != Stem.None) {
          MusicManager.Instance.SetStem(stem, false);
        }
      } else {
        if (ActiveEmotion == EmotionType.None) {
          ActiveEmotion = (EmotionType) newEmotion;

          if (stem != Stem.None) {
            MusicManager.Instance.SetStem(stem, true);
          }
        }
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
      case EmotionType.Affection:
        spriteRenderer.color = new Color(1f, .3f, .3f, 0.3f);
        go.text = "Affection Lens";

        break;
      case EmotionType.Remorse:
        spriteRenderer.color = new Color(1f, .5f, 1f, 0.3f);
        go.text = "Remorse Lens";

        break;
    }
  }
}