using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState {
  Introduction,
  FirstGameplay
}

public class Manager: MonoBehaviour {
  // Prefabs 
  public GameObject DialogPrefab;

  public GameObject CuriosityCuePrefab;

  // Objects

  public Player Player;

  public GameObject OtherGuy;
  public TextMeshProUGUI InstructObj;

  public Camera Camera;

  public GameState CurrentGameState;

  [Header("Where you start at for each state")]
  public GameObject IntroductionPlayerPosition;

  public GameObject FirstGameplayPlayerPosition;

  [Header("Objects used to fade game in and out")]

  public GameObject FullFade;
  public GameObject CircleFade;

  /** Singleton instance of the Manager. */
  public static Manager Instance;

  void Awake() {
    Instance = this;
  }

  void Start() {
    // Stuff that happens at the very beginning of the game.
    StartNewScene();

    //FullFade.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    CircleFade.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
  }

  void StartNewScene() {
    switch (CurrentGameState) {
      case GameState.Introduction:
        //FullFade.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        StartIntroduction();
        break;
      case GameState.FirstGameplay:
        
        StartFirstGameplay();
        break;
    }
  }

    public void SetInstruction(string instructText)
    {
        CanvasGroup canvasGroup = InstructObj.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        InstructObj.gameObject.SetActive(true);
        LeanTween.alphaCanvas(canvasGroup, 0.5f, 1f).setEaseInOutQuad().setOnComplete(() => {
            LeanTween.alphaCanvas(canvasGroup, 0.7f, 1.5f).setLoopPingPong();
        });
        InstructObj.text = instructText;
    }

    public void HideInstruction()
    {
        LeanTween.alphaCanvas(InstructObj.GetComponent<CanvasGroup>(), 0f, 1f).setEaseInOutQuad().setOnComplete(() =>
        {
            InstructObj.gameObject.SetActive(false);
        });
    }

  void StartIntroduction() {
    Player.Emotions = new EmotionState {
      Curiosity = false,
      Compassion = false,
      Affection = false,
      Remorse = false,
      Betrayal = false,
    };

    Player.transform.position = IntroductionPlayerPosition.transform.position;

    DialogManager.Instance.StartDialogSequence(DialogText.FirstDialog);
  }

  void StartFirstGameplay() {
    Player.Emotions = new EmotionState {
      Curiosity = true,
      Compassion = false,
      Affection = false,
      Remorse = false,
      Betrayal = false,
    };

    Player.transform.position = FirstGameplayPlayerPosition.transform.position;
  }

  void Update() {

  }

  public static Dialog CreateNewDialog(string text, GameObject target, Dictionary<EmotionType, List<DialogEvent>> emotionReactions) {
    var dialogGO = GameObject.Instantiate(
      Instance.DialogPrefab,
      target.transform.position,
      Quaternion.identity
    );

    var dialog = dialogGO.GetComponent<Dialog>();

    dialog.StartDialog(text, emotionReactions);

    return dialog;
  }

  public static GameObject CreateNewEmotionCue(EmotionType emotionType, GameObject Target) {
    GameObject OriginalPrefab = null;
    GameObject EmotionCueObject = null;
    Vector3 offset = new Vector3(-0.5f, -1.5f, 0);

    // Decide which emotion type to use
    switch (emotionType) {
      case EmotionType.Curiosity:
        OriginalPrefab = Instance.CuriosityCuePrefab;
        break;
    }
    if (OriginalPrefab == null) {
      Util.Log("No emotion cue for emotionType", emotionType, "or prefab is null");
      return EmotionCueObject;
    }

    // Instantiate and nest in parent
    EmotionCueObject = GameObject.Instantiate(
      OriginalPrefab,
      Target.transform.position + offset,
      Quaternion.identity
    );
    EmotionCueObject.transform.SetParent(Target.transform);

    // Return 
    return EmotionCueObject;
  }
}

// TODO;
// * read button press
// * when dialog, all other motion is frozen probably