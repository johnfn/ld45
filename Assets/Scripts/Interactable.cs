using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactable: MonoBehaviour {

   public GameObject interactIndicator;
   public float xPos;

    void Start() {
        xPos = transform.position.x;
        InteractableManager.Interactables.Add(this);
    }

  void OnDestroy() {
    InteractableManager.Interactables.Remove(this);
  }

  virtual public void ShowAsInteractable() {
        interactIndicator = Instantiate(Manager.Instance.interactIndicatorPrefab, new Vector3(xPos, transform.position.y+3, 0), Quaternion.identity, Manager.Instance.interactIndicatorParent);
        interactIndicator.SetActive(true);
  }

  virtual public void ShowAsNormal() {
        Destroy(interactIndicator);
        interactIndicator.SetActive(false);
    }

  virtual public void Interact() {

  }

  void Update() {

  }
}
