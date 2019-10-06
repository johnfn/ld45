using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable: MonoBehaviour {

    Vector3 origScale;

  void Start() {
        origScale = transform.localScale;
    InteractableManager.Interactables.Add(this);
  }

  void OnDestroy() {
    InteractableManager.Interactables.Remove(this);
  }

  virtual public void ShowAsInteractable() {
        transform.localScale = origScale * 1.2f;
  }

  virtual public void ShowAsNormal() {
        transform.localScale = origScale;
  }

  virtual public void Interact() {

  }

  void Update() {

  }
}
