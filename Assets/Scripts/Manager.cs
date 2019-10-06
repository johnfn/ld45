using System.Collections.Generic;
using UnityEngine;

public enum GameState {
  Introduction,
  FirstGameplay
}

public class Manager: MonoBehaviour {
  // Prefabs 
  public GameObject DialogPrefab;

  public GameObject CuriosityCuePrefab;

  // Objects

  public Player Player;

  public GameObject OtherGuy;

  public Camera Camera;

  public GameState CurrentGameState;

  [Header("Objects used to fade game in and out")]

  public GameObject FullFade;

  public GameObject CircleFade;

  /** Singleton instance of the Manager. */
  public static Manager Instance;

  void Awake() {
    Instance = this;
  }

  void Start() {
    FullFade.gameObject.GetComponent<SpriteRenderer>().color   = new Color(1f, 1f, 1f, 0f);
    CircleFade.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

    // Stuff that happens at the very beginning of the game.
    StartNewScene();
  }

  void StartNewScene() {
    switch (CurrentGameState) {
      case GameState.Introduction:
        StartIntroduction();
        break;
      case GameState.FirstGameplay:

        break;
    }
  }

  void StartIntroduction() {
    DialogManager.Instance.StartDialogSequence(DialogText.FirstDialog);
  }

  void Update() {

  }

  public static Dialog CreateNewDialog(string text, GameObject target) {
    var dialogGO = GameObject.Instantiate(
      Instance.DialogPrefab,
      target.transform.position,
      Quaternion.identity
    );

    Util.Log(target.name);

    var dialog = dialogGO.GetComponent<Dialog>();

    dialog.StartDialog(text);

    return dialog;
  }

  public static GameObject CreateNewEmotionCue(EmotionType emotionType, GameObject Target) {
    GameObject OriginalPrefab = null;
    GameObject EmotionCue = null;

    // Decide which emotion type to use
    switch (emotionType) {
      case EmotionType.Curiosity:
        OriginalPrefab = Instance.CuriosityCuePrefab;
        break;
    }
    if (OriginalPrefab == null) {
      Util.Log("No emotion cue for emotionType", emotionType, "or prefab is null");
      return EmotionCue;
    }

    // Create & return
    Util.Log(OriginalPrefab);
    Util.Log(Target.transform.position);
    EmotionCue = GameObject.Instantiate(
      OriginalPrefab,
      Target.transform.position + new Vector3(0, 2, 0),
      Quaternion.identity
    );
    // Nest in parent
    EmotionCue.transform.SetParent(Target.transform);
    return EmotionCue;
  }
}

// TODO;
// * read button press
// * when dialog, all other motion is frozen probably