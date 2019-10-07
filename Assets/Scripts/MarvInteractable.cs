using System.Collections.Generic;
using UnityEngine;

public class MarvInteractable : Interactable
{
    override public void ShowAsInteractable()
    {
        interactIndicator = Instantiate(Manager.Instance.interactIndicatorPrefab, new Vector3(xPos, transform.position.y + 3, 0), Quaternion.identity, Manager.Instance.interactIndicatorParent);
        interactIndicator.SetActive(true);

        Manager.Instance.SetInstruction("Press X to interact.");
    }

    override public void Interact()
    {
        DialogManager.Instance.StartDialogSequence(DialogText.MarvDialog);
    }

    void Update()
    {

    }
}