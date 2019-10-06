using UnityEngine;

public enum GameMode {
  Playing,
  Dialog
}

public class HasActiveMode: MonoBehaviour {
  public GameMode ActiveMode;

  void Awake() {
    ModeManager.ThingsWithModes.Add(this);
  }
}