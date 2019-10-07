using System.Collections.Generic;
using UnityEngine;

public enum MusicSegment {
  LakeRasa0,
  LakeRasa1,
  LakeRasa2,
  LakeRasa3
}

public class MusicManager: MonoBehaviour {
  public GameObject LR0Trigger;
  public GameObject LR1Trigger;
  public GameObject LR2Trigger;
  public GameObject LR3Trigger;

  public AudioSource LR0;
  public AudioSource LR1;
  public AudioSource LR2;
  public AudioSource LR3;

  public static MusicManager Instance;

  void Awake() {
    Instance = this;
  }

  void Start() {
    LR0.loop = true;
    LR1.loop = true;
    LR2.loop = true;
    LR3.loop = true;

    LR0.Play();
    LR0.volume = 0f;

    LR1.Play();
    LR1.volume = 0f;

    LR2.Play();
    LR2.volume = 0f;

    LR3.Play();
    LR3.volume = 0f;

    TriggerMusicSegment(MusicSegment.LakeRasa0);
  }

  public void TriggerMusicSegment(MusicSegment segment) {
    if (
      segment == MusicSegment.LakeRasa0 ||
      segment == MusicSegment.LakeRasa1 ||
      segment == MusicSegment.LakeRasa2 ||
      segment == MusicSegment.LakeRasa3
    ) {
      // i no how to code go away

      switch (segment) {
        case MusicSegment.LakeRasa0:
          LeanTween.value(LR0.gameObject, v => LR0.volume = v, LR0.volume, 1f, 1f);
          LeanTween.value(LR1.gameObject, v => LR1.volume = v, LR1.volume, 0f, 1f);
          LeanTween.value(LR2.gameObject, v => LR2.volume = v, LR2.volume, 0f, 1f);
          LeanTween.value(LR3.gameObject, v => LR3.volume = v, LR3.volume, 0f, 1f);
        break;
        case MusicSegment.LakeRasa1:
          LeanTween.value(LR0.gameObject, v => LR0.volume = v, LR0.volume, 0f, 1f);
          LeanTween.value(LR1.gameObject, v => LR1.volume = v, LR1.volume, 1f, 1f);
          LeanTween.value(LR2.gameObject, v => LR2.volume = v, LR2.volume, 0f, 1f);
          LeanTween.value(LR3.gameObject, v => LR3.volume = v, LR3.volume, 0f, 1f);
        break;
        case MusicSegment.LakeRasa2:
          LeanTween.value(LR0.gameObject, v => LR0.volume = v, LR0.volume, 0f, 1f);
          LeanTween.value(LR1.gameObject, v => LR1.volume = v, LR1.volume, 0f, 1f);
          LeanTween.value(LR2.gameObject, v => LR2.volume = v, LR2.volume, 1f, 1f);
          LeanTween.value(LR3.gameObject, v => LR3.volume = v, LR3.volume, 0f, 1f);
        break;
        case MusicSegment.LakeRasa3:
          LeanTween.value(LR0.gameObject, v => LR0.volume = v, LR0.volume, 0f, 1f);
          LeanTween.value(LR1.gameObject, v => LR1.volume = v, LR1.volume, 0f, 1f);
          LeanTween.value(LR2.gameObject, v => LR2.volume = v, LR2.volume, 0f, 1f);
          LeanTween.value(LR3.gameObject, v => LR3.volume = v, LR3.volume, 1f, 1f);
        break;
      }
    }
  }

  void Update() {
    var player = Manager.Instance.Player;
  }
}