using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character: MonoBehaviour {

  public Canvas CharacterCanvas;
  
  public CharacterName CharacterName;

  public static List<Character> Speakers = new List<Character>();

  void Awake() {
    Speakers.Add(this);
  }
}
