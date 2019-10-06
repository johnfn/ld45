using UnityEngine;
using System.Collections.Generic;

public class ModeManager: MonoBehaviour {
  public static List<HasActiveMode> ThingsWithModes = new List<HasActiveMode>();

  private static Dictionary<Component, bool> oldActiveState =  new Dictionary<Component, bool>();

  public static ModeManager Instance;

  [HideInInspector]
  public GameMode CurrentGameMode;

  public static void SetGameMode(GameMode newMode) {
    Instance.CurrentGameMode = newMode;

    foreach (var thing in ThingsWithModes) {
      var children = thing.GetComponents<MonoBehaviour>();

      foreach (var comp in children) {
        oldActiveState[comp] = comp.enabled = thing.ActiveMode == newMode;
      }
    }
  }

  void Awake() {
    Instance = this;
  }

  void Update() {

  }
}