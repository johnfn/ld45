using System.Collections.Generic;
using UnityEngine;

public enum MusicSegment {
  LakeRasa0,
  LakeRasa1,
  LakeRasa2,
  LakeRasa3,

  NewHylidea
}

public enum Stem {
  None,

  Ash,
  Affection,
  Compassion,
  Remorse
}

public class MusicManager: MonoBehaviour {
  public GameObject LR0Trigger;
  public GameObject LR1Trigger;
  public GameObject LR2Trigger;
  public GameObject LR3Trigger;

  public GameObject NHTrigger;

  public AudioSource LR0;
  public AudioSource LR1;
  public AudioSource LR2;
  public AudioSource LR3;

  public AudioSource NHWelcomeLol;
  public AudioSource NHBase;
  public AudioSource NHAsh;
  public AudioSource NHAffection;
  public AudioSource NHCompassion;
  public AudioSource NHRemorse;

  public AudioSource Betrayal;

  public static MusicManager Instance;

  void Awake() {
    Instance = this;
  }

  void Start() {
    LR0.loop = true;
    LR1.loop = true;
    LR2.loop = true;
    LR3.loop = true;

    NHWelcomeLol.loop = true;
    NHBase.loop = true;
    NHAsh.loop = true;
    NHAffection.loop = true;
    NHCompassion.loop = true;
    NHRemorse.loop = true;
    Betrayal.loop = true;

    NHWelcomeLol.Play();
    NHBase.Play();
    NHAsh.Play();
    NHAffection.Play();
    NHCompassion.Play();
    NHRemorse.Play();

    NHWelcomeLol.volume = 0f;
    NHBase.volume = 0f;
    NHAsh.volume = 0f;
    NHAffection.volume = 0f;
    NHCompassion.volume = 0f;
    NHRemorse.volume = 0f;
    Betrayal.volume = 0f;

    LR0.volume = 0f;
    LR1.volume = 0f;
    LR2.volume = 0f;
    LR3.volume = 0f;

    LR0.Play();
    LR1.Play();
    LR2.Play();
    LR3.Play();

    TriggerMusicSegment(MusicSegment.LakeRasa0);
  }

  public void Betray() {
    NHWelcomeLol.volume = 0f;
    NHBase.volume = 0f;
    NHAsh.volume = 0f;
    NHAffection.volume = 0f;
    NHCompassion.volume = 0f;
    NHRemorse.volume = 0f;

    LR0.volume = 0f;
    LR1.volume = 0f;
    LR2.volume = 0f;
    LR3.volume = 0f;

    Betrayal.volume = 1f;
    Betrayal.Play();
  }

  public void SetStem(Stem stem, bool on) {
    switch (stem) {
      case Stem.Affection:
        LeanTween.value(NHAffection.gameObject, v => NHAffection.volume = v, NHAffection.volume, on ? 1f : 0f, 1f);
        break;
      case Stem.Ash:
        LeanTween.value(NHAsh.gameObject, v => NHAsh.volume = v, NHAsh.volume, on ? 1f : 0f, 1f);
        break;
      case Stem.Compassion:
        LeanTween.value(NHCompassion.gameObject, v => NHCompassion.volume = v, NHCompassion.volume, on ? 1f : 0f, 1f);
        break;
      case Stem.Remorse:
        LeanTween.value(NHRemorse.gameObject, v => NHRemorse.volume = v, NHRemorse.volume, on ? 1f : 0f, 1f);
        break;
    }
  }

  public void TriggerMusicSegment(MusicSegment segment) {
    if (
      segment == MusicSegment.LakeRasa0 ||
      segment == MusicSegment.LakeRasa1 ||
      segment == MusicSegment.LakeRasa2 ||
      segment == MusicSegment.LakeRasa3
    ) {
      // i no how to code go away
      LeanTween.value(NHBase.gameObject, v => NHBase.volume = v, NHBase.volume, 0f, 1f);

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

    if (segment == MusicSegment.NewHylidea) {
      LeanTween.value(LR0.gameObject, v => LR0.volume = v, LR0.volume, 0f, 1f);
      LeanTween.value(LR1.gameObject, v => LR1.volume = v, LR1.volume, 0f, 1f);
      LeanTween.value(LR2.gameObject, v => LR2.volume = v, LR2.volume, 0f, 1f);
      LeanTween.value(LR3.gameObject, v => LR3.volume = v, LR3.volume, 0f, 1f);

      LeanTween.value(NHBase.gameObject, v => NHBase.volume = v, NHBase.volume, 1f, 1f);
    }
  }

  void Update() {
    var player = Manager.Instance.Player;
  }
}