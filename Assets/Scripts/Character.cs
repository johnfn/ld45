using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character: MonoBehaviour {

  public CharacterName CharacterName;
  public static List<Character> Speakers = new List<Character>();

  private Canvas _CharacterCanvas;
  public Canvas CharacterCanvas { get { return _CharacterCanvas; } }

  void Awake() {
    Speakers.Add(this);
  }

  void Start() {
    this._CharacterCanvas = GetComponentInChildren<Canvas>();
  }
}
