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

  public GameObject AffectionCuePrefab;
  public GameObject BetrayalCuePrefab;
  public GameObject CompassionCuePrefab;
  public GameObject CuriosityCuePrefab;
  public GameObject ForgivenessCuePrefab;
  public GameObject RemorseCuePrefab;

  private GameObject GetEmotionPrefab(EmotionType emotionType) {
    switch (emotionType) {
      case EmotionType.Affection:
        return Instance.AffectionCuePrefab;
      case EmotionType.Betrayal:
        return Instance.BetrayalCuePrefab;
      case EmotionType.Compassion:
        return Instance.CompassionCuePrefab;
      case EmotionType.Curiosity:
        return Instance.CuriosityCuePrefab;
      case EmotionType.Forgiveness:
        return Instance.ForgivenessCuePrefab;
      case EmotionType.Remorse:
        return Instance.RemorseCuePrefab;
      default:
        return null;
    }
  }

  // Objects

  public Player Player;
    public Transform interactIndicatorParent;
    public GameObject bar1, bar2;

  public GameObject OtherGuy;
  public TextMeshProUGUI InstructObj;
  public Transform tutTriggersParent;

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
    Player.Emotions = new EmotionState {
      Curiosity = true,
      Compassion = false,
      Affection = false,
      Remorse = false,
      Betrayal = false,
    };

    Player.transform.position = FirstGameplayPlayerPosition.transform.position;
  }

  void StartFirstGameplayWithCompassion() {
    Player.Emotions = new EmotionState {
      Curiosity = true,
      Compassion = true,
      Affection = false,
      Remorse = false,
      Betrayal = false,
    };

    Player.transform.position = WithCompassionPosition.transform.position;
  }

  void StartInTown() {
    Player.Emotions = new EmotionState {
      Curiosity = true,
      Compassion = true,
      Affection = false,
      Remorse = false,
      Betrayal = false,
    };

    Player.transform.position = InTownPosition.transform.position;
  }

  void StartInTownWithAffection() {
    Player.Emotions = new EmotionState {
      Curiosity = true,
      Compassion = true,
      Affection = true,
      Remorse = false,
      Betrayal = false,
    };

    Player.transform.position = InTownPosition.transform.position;
  }

  void StartInTownWithAll() {
    Player.Emotions = new EmotionState {
      Curiosity = true,
      Compassion = true,
      Affection = true,
      Remorse = true,
      Betrayal = false,
    };

    Player.transform.position = InTownPosition.transform.position;
  }

  void Update() {

  }

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

  public static GameObject CreateNewEmotionCue(EmotionType emotionType, GameObject Target) {
    GameObject OriginalPrefab = null;
    GameObject EmotionCueObject = null;
    Vector3 offset = new Vector3(-0.5f, -1.5f, 0);

    // Decide which emotion type to use
    OriginalPrefab = Instance.GetEmotionPrefab(emotionType);
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