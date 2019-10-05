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

    public static void CreateNewDialog(string text, GameObject target) {
        GameObject.Instantiate(
            Instance.DialogPrefab,
            target.transform.position,
            Quaternion.identity
        );
    }
}
