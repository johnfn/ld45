using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutTrigger : MonoBehaviour
{
    private bool seen = false;
    public string instruction = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !seen)
        {
            seen = true;

            if (instruction.Length > 0) Manager.Instance.SetInstruction(instruction);
            else Manager.Instance.HideInstruction();
        }
    }
}
