using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
  Introduction,
  FirstGameplay
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

  }

  void Update() {

  }

  public static Dialog CreateNewDialog(string text, GameObject Target) {
    var NewDialog = GameObject.Instantiate(
        Instance.DialogPrefab,
        Target.transform.position,
        Quaternion.identity
    );
    return NewDialog;
  }
}
