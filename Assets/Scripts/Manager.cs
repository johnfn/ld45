using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState {
  Introduction,
  FirstGameplay,
  WithCompassion,
  InTown,
  InTownWithAffection,
  InTownWithAll
}

public class Manager: MonoBehaviour {
  // Prefabs 
  public GameObject DialogPrefab;
  public GameObject interactIndicatorPrefab;

  public GameObject DialogReactionPrefab;

  // Objects

  public Player Player;
    public Transform interactIndicatorParent;
    public GameObject bar1, bar2;

  public GameObject OtherGuy;
  public TextMeshProUGUI InstructObj;
  public Transform tutTriggersParent;

  public GameObject LearnedEmotions;

  public Camera Camera;

  public GameState CurrentGameState;

  [Header("Where you start at for each state")]
  public GameObject IntroductionPlayerPosition;

  public GameObject FirstGameplayPlayerPosition;

  public GameObject WithCompassionPosition;

  public GameObject InTownPosition;

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
        StartIntroduction();
        break;
      case GameState.FirstGameplay:
        StartFirstGameplay();
        break;
      case GameState.WithCompassion:
        StartFirstGameplayWithCompassion();
        break;
      case GameState.InTown:
        StartInTown();
        break;
      case GameState.InTownWithAffection:
        StartInTownWithAffection();
        break;
      case GameState.InTownWithAll:
        StartInTownWithAll();
        break;
    }
  }

    private void ActivateTutTriggers()
    {
        foreach(Transform child in tutTriggersParent) child.gameObject.SetActive(true);
    }

  /* Instructions */
    public void SetInstruction(string instructText)
    {
        CanvasGroup canvasGroup = InstructObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        InstructObj.gameObject.SetActive(true);
        LeanTween.alphaCanvas(canvasGroup, 0.5f, 0.6f).setEaseInOutQuad().setOnComplete(() => {
            LeanTween.alphaCanvas(canvasGroup, 0.7f, 1.5f).setLoopPingPong();
        });
        InstructObj.text = instructText;
    }

    public void HideInstruction()
    {
        LeanTween.alphaCanvas(InstructObj.GetComponent<CanvasGroup>(), 0f, 0.6f).setEaseInOutQuad().setOnComplete(() =>
        {
            InstructObj.gameObject.SetActive(false);
        });
    }

  /* Game state */

  void StartIntroduction() {
    Player.Emotions = new EmotionState {
      Curiosity = false,
      Compassion = false,
      Affection = false,
      Remorse = false,
      Betrayal = false,
    };

    FullFade.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    ActivateTutTriggers();

    Player.transform.position = IntroductionPlayerPosition.transform.position;
    DialogManager.Instance.StartDialogSequence(DialogText.FirstDialog);
  }

  void StartFirstGameplay() {
    EmotionManager.TeachEmotion(EmotionType.Curiosity);

    Player.transform.position = FirstGameplayPlayerPosition.transform.position;
  }

  void StartFirstGameplayWithCompassion() {
    var StartEmotions = new List<EmotionType>() { 
      EmotionType.Curiosity, 
      EmotionType.Compassion,
    };
    EmotionManager.TeachEmotions(StartEmotions);

    Player.transform.position = WithCompassionPosition.transform.position;
  }

  void StartInTown() {
    var StartEmotions = new List<EmotionType>() { 
      EmotionType.Curiosity, 
      EmotionType.Compassion,
    };
    EmotionManager.TeachEmotions(StartEmotions);

    Player.transform.position = InTownPosition.transform.position;
  }

  void StartInTownWithAffection() {
    var StartEmotions = new List<EmotionType>() { 
      EmotionType.Curiosity, 
      EmotionType.Compassion,
      EmotionType.Affection,
    };
    EmotionManager.TeachEmotions(StartEmotions);

    Player.transform.position = InTownPosition.transform.position;
  }

  void StartInTownWithAll() {
    var StartEmotions = new List<EmotionType>() { 
      EmotionType.Curiosity, 
      EmotionType.Compassion,
      EmotionType.Affection,
      EmotionType.Remorse, 
    };
    EmotionManager.TeachEmotions(StartEmotions);

    Player.transform.position = InTownPosition.transform.position;
  }

  void Update() {

  }

  /* Learned emotions */





  /* Misc */

  public static Dialog CreateNewDialog(
    string text, 
    GameObject target, 
    List<(EmotionType, string, List<DialogEvent>)> emotionReactions = null
  ) {
    var dialogGO = GameObject.Instantiate(
      Instance.DialogPrefab,
      target.transform.position,
      Quaternion.identity
    );

    var dialog = dialogGO.GetComponent<Dialog>();

    dialog.StartDialog(text, emotionReactions);

    return dialog;
  }
}
// TODO;
// * read button press
// * when dialog, all other motion is frozen probably