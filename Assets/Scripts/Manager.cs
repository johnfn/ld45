using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager: MonoBehaviour {
    public GameObject DialogPrefab;
    public static Manager Instance;

    void Awake() {
        Instance = this;
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public static GameObject CreateNewDialog(string text, GameObject Target) {
        var offsetPosition = Target.transform.position + new Vector3(0, 2, 0);
        var newDialog = GameObject.Instantiate(
            Instance.DialogPrefab,
            offsetPosition,
            Quaternion.identity
        );
        // Attempt at centering by width
        // var Dialog = newDialog.GetComponent<Dialog>();
        // Util.Log("Width", Dialog.Width, "Height", Dialog.Height);
        // newDialog.transform.position = newDialog.transform.position - new Vector3(Dialog.Width / 2, 0, 0);

        return newDialog;
    }
}
