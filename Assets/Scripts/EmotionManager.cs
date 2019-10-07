using System.Collections.Generic;
using UnityEngine;

public class EmotionManager: MonoBehaviour {

  [Header("UI Elements. Appear above player's head when near interactable")]
  public GameObject AffectionCuePrefab;
  public GameObject BetrayalCuePrefab;
  public GameObject CompassionCuePrefab;
  public GameObject CuriosityCuePrefab;
  public GameObject ForgivenessCuePrefab;
  public GameObject RemorseCuePrefab;

  [Header("Find these in OverlayCanvas > LearnedEmotions")]
  public GameObject LearnedAffectionIcon;
  public GameObject LearnedBetrayalIcon;
  public GameObject LearnedCompassionIcon;
  public GameObject LearnedCuriosityIcon;
  public GameObject LearnedForgivenessIcon;
  public GameObject LearnedRemorseIcon;
  
  private GameObject GetEmotionPrefab(EmotionType emotionType) {
    switch (emotionType) {
      case EmotionType.Affection:
        return Instance.AffectionCuePrefab;
      case EmotionType.Betrayal:
        return Instance.BetrayalCuePrefab;
      case EmotionType.Compassion:
        return Instance.CompassionCuePrefab;
      case EmotionType.Curiosity:
        return Instance.CuriosityCuePrefab;
      case EmotionType.Forgiveness:
        return Instance.ForgivenessCuePrefab;
      case EmotionType.Remorse:
        return Instance.RemorseCuePrefab;
      default:
        return null;
    }
  }

/** Singleton instance of the Manager. */
  public static EmotionManager Instance;

  void Awake() {
    Instance = this;
  }

  public static GameObject CreateNewEmotionCue(EmotionType emotionType, GameObject Target) {
    GameObject OriginalPrefab = null;
    GameObject EmotionCueObject = null;
    Vector3 offset = new Vector3(-0.5f, -1.5f, 0);

    // Decide which emotion type to use
    OriginalPrefab = Instance.GetEmotionPrefab(emotionType);
    if (OriginalPrefab == null) {
      Util.Log("No emotion cue for emotionType", emotionType, "or prefab is null");
      return EmotionCueObject;
    }

    // Instantiate and nest in parent
    EmotionCueObject = GameObject.Instantiate(
      OriginalPrefab,
      Target.transform.position + offset,
      Quaternion.identity
    );
    EmotionCueObject.transform.SetParent(Target.transform);

    // Return 
    return EmotionCueObject;
  }

  public static void TeachEmotions(List<EmotionType> Emotions) {
    Emotions.ForEach((Emotion) => EmotionManager.TeachEmotion(Emotion));
  }

  /// Teach emotion to player.
  /// Returns boolean: whether or not anything changed.
  public static bool TeachEmotion(EmotionType emotion) {
    if (emotion == EmotionType.None) {
      return false;
    }
    bool didTeachEmotion = false;
    switch (emotion) {
      case EmotionType.Curiosity:
        Manager.Instance.Player.Emotions.Curiosity = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Compassion:
        Manager.Instance.Player.Emotions.Compassion = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Affection:
        Manager.Instance.Player.Emotions.Affection = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Remorse:
        Manager.Instance.Player.Emotions.Remorse = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Betrayal:
        Manager.Instance.Player.Emotions.Betrayal = true;
        didTeachEmotion = true;
        break;
      case EmotionType.Forgiveness:
        Manager.Instance.Player.Emotions.Forgiveness = true;
        didTeachEmotion = true;
        break;
    }

    EmotionManager.SetEmotionIconActive(emotion);
    return didTeachEmotion;
  }

  public static void SetEmotionIconActive(EmotionType emotionType) {
    switch (emotionType) {
      case EmotionType.Affection:
        Instance.LearnedAffectionIcon.SetActive(true);
        break;
      case EmotionType.Betrayal:
        Instance.LearnedBetrayalIcon.SetActive(true);
        break;
      case EmotionType.Compassion:
        Instance.LearnedCompassionIcon.SetActive(true);
        break;
      case EmotionType.Curiosity:
        Instance.LearnedCuriosityIcon.SetActive(true);
        break;
      case EmotionType.Forgiveness:
        Instance.LearnedForgivenessIcon.SetActive(true);
        break;
      case EmotionType.Remorse:
        Instance.LearnedRemorseIcon.SetActive(true);
        break;
      default:
        break;
    }
    return;
  }
}