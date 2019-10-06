using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InteractableManager: MonoBehaviour {

  public static InteractableManager Instance;

  /** All Interactables currently alive add themselves to this. */
  public static List<Interactable> Interactables = new List<Interactable>();

  public int MaxDistanceToInteractable;

  private GameObject currentTarget;

  void Awake() {
    Instance = this;
  }

  void Update() {
    if (Interactables.Count == 0) {
      return;
    }

    var player = Manager.Instance.Player;
    var sortedByDistance = Interactables.OrderBy(x => Util.Distance(x.gameObject, player.gameObject));
    // Figure out which interactable is currently targetted

    var newTarget = sortedByDistance.First().gameObject;

    // Handle
    if (Util.Distance(player.gameObject, newTarget) > MaxDistanceToInteractable) {
      newTarget = null;
    }

    bool existsSomeTarget = (newTarget != null || currentTarget != null);
    bool hasChangedTarget = newTarget != currentTarget;

    if (existsSomeTarget && hasChangedTarget) {
      if (newTarget) {
        newTarget.GetComponent<Interactable>().ShowAsInteractable();
      }

      if (currentTarget) {
        currentTarget.GetComponent<Interactable>().ShowAsNormal();
      }
    }

    if (newTarget) {
      currentTarget = newTarget;
    } else {
      currentTarget = null;
    }
  }
}