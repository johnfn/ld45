using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Figure out which interactable is currently targetted

public class InteractableManager: MonoBehaviour {
  public static List<Interactable> Interactables = new List<Interactable>();

  public int MaxDistanceToInteractable;

  private GameObject currentTarget;

  void Update() {
    if (Interactables.Count == 0) {
      return;
    }

    var player = Manager.Instance.Player;
    var sortedByDistance = Interactables.OrderBy(x => Util.Distance(x.gameObject, player.gameObject));
    var newTarget = sortedByDistance.First().gameObject;

    if (Util.Distance(player.gameObject, newTarget) > MaxDistanceToInteractable) {
      newTarget = null;
    }

    if ((newTarget != null || currentTarget != null) && newTarget != currentTarget) {
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