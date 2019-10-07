using System.Collections.Generic;
using UnityEngine;

public class EmotionManager: MonoBehaviour {

  public GameObject AffectionCuePrefab;
  public GameObject BetrayalCuePrefab;
  public GameObject CompassionCuePrefab;
  public GameObject CuriosityCuePrefab;
  public GameObject ForgivenessCuePrefab;
  public GameObject RemorseCuePrefab;

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

}