using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public GameObject creditContent;

    void Start()
    {
        LeanTween.moveLocalY(creditContent, 1640, 20f).setDelay(5f).setOnComplete(() => {
            PlayerPrefs.SetInt("FinishedGame", 1);
            SceneManager.LoadScene("Title");
        });
    }

    
}
