using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
  Introduction,
  FirstGameplay
}

public class Manager: MonoBehaviour {
  public GameObject DialogPrefab;

  public GameObject Player;

  public GameObject OtherGuy;

  public Camera Camera;

  public GameState CurrentGameState;

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

  public static void CreateNewDialog(string text, GameObject target) {
    GameObject.Instantiate(
        Instance.DialogPrefab,
        target.transform.position,
        Quaternion.identity
    );
  }
}
