using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Figure out which interactable is currently targetted

public class InteractableManager: MonoBehaviour {
  public static List<Interactable> Interactables = new List<Interactable>();

  public int MaxDistanceToInteractable;

  private GameObject targettedInteractable;

  void Update() {
    if (Interactables.Count == 0) {
      return;
    }

    var player = Manager.Instance.Player;
    var sortedByDistance = Interactables.OrderBy(x => Util.Distance(x.gameObject, player.gameObject));
    var closest = sortedByDistance.First();

    if (targettedInteractable) {
      targettedInteractable.transform.localScale = new Vector3(1, 1, 1);
    }

    if (Util.Distance(player.gameObject, closest.gameObject) < MaxDistanceToInteractable) {
      targettedInteractable = closest.gameObject;

      targettedInteractable.transform.localScale = new Vector3(2f, 2f, 2f);
    } else {
      targettedInteractable = null;
    }
  }
}