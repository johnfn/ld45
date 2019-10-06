using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable: MonoBehaviour {
  public static List<Interactable> Interactables = new List<Interactable>();

  void Awake() {
    Interactables.Add(this);
  }

  void OnDestroy() {
    Interactables.Remove(this);
  }


  void Update() {

  }
}
