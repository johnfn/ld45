using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable: MonoBehaviour {
  void Start() {
    InteractableManager.Interactables.Add(this);
  }

  void OnDestroy() {
    InteractableManager.Interactables.Remove(this);
  }

  void Update() {

  }
}
