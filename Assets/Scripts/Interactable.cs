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

  public void ShowAsInteractable() {
    transform.localScale = new Vector3(2, 2, 2);
  }

  public void ShowAsNormal() {
    transform.localScale = new Vector3(1, 1, 1);
  }

  void Update() {

  }
}
