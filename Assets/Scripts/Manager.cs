using System.Collections.Generic;
using UnityEngine;

public enum GameState {
  Introduction,
  FirstGameplay
}

enum EmotionType {
  Curiosity,
  Affection,
  Remorse,
  Betrayal,
  Forgiveness,
}

public class Manager: MonoBehaviour {
  // Prefabs 
  public GameObject DialogPrefab;

  public GameObject RageEmotionPrefab;

  // Objects

  public GameObject Player;

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

  public static Dialog CreateNewDialog(string text, GameObject Target) {
    var dialogGO = GameObject.Instantiate(
        Instance.DialogPrefab,
        Target.transform.position,
        Quaternion.identity
    );

    var dialog = dialogGO.GetComponent<Dialog>();

    dialog.StartDialog(text);

    return dialog;
  }
}

// TODO;
// * read button press
// * when dialog, all other motion is frozen probably