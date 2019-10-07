using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public GameObject title;
    public Sprite colorfulTitle;
    public CanvasGroup errorMsg;
    public CanvasGroup content;

    public TMP_InputField inputField;
    public Button button;
    public CanvasGroup allCanvas;

    void Start()
    {
        if (PlayerPrefs.GetInt("FinishedGame", 0) == 1) title.GetComponent<Image>().sprite = colorfulTitle;
        StartAnim();        
    }

    private void StartAnim()
    {
        LeanTween.scale(title, Vector3.one, 2f).setEaseInOutQuad().setOnComplete(() => {
            LeanTween.moveLocalY(title, 110, 1f).setEaseInOutQuad();
            LeanTween.alphaCanvas(content, 1, 1f).setEaseInOutQuad();
        });
    }

    public void LimitInput()
    {
        inputField.text = "";
        if (errorMsg.alpha == 0)
        {
            LeanTween.alphaCanvas(errorMsg, 1, 0.7f);
            button.interactable = true;
        }
    }

    public void GoTo()
    {
        LeanTween.alphaCanvas(allCanvas, 0, 1f).setEaseInOutQuad().setOnComplete(() => { SceneManager.LoadScene("SampleScene"); });
    }
}
