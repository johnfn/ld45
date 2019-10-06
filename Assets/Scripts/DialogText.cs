using UnityEngine;
using System.Collections.Generic;

public enum CharacterName {
  Blank,
  Ash,

  Trudy
}

public class Fade {
  public float RectangleFadeOpacity = 1.0f;
  public bool Immediate = false;
}

public class DialogEvent {
  public CharacterName Name;
  public string Contents;

  // other events that can happen during dialog

  public Fade Fade = null;  public EmotionType ReceiveEmotion = EmotionType.None;
}


public class DialogText {
  public static List<DialogEvent> FirstDialog = new List<DialogEvent> {
    new DialogEvent { Fade = new Fade { Immediate = true, RectangleFadeOpacity = 1.0f } },

    new DialogEvent { Name = CharacterName.Ash  , Contents = "Hey!" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },

    new DialogEvent { ReceiveEmotion = EmotionType.Curiosity },

    new DialogEvent { Name = CharacterName.Ash  , Contents = "What happened? Are you okay?" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "What’s your name?" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "I haven’t seen someone down here in ages…" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "And wow, you can tell. What a stench! Whew!" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "Hello? Cat got your tongue? Can you understand me?" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "Hey, we can’t figure out what’s going on until you talk to me. Aren't you curious, too?" },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.9f, } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.9f, CircleFadeOpacity = 0.5f } },
    new DialogEvent { Name = CharacterName.Blank, Contents = "...curious?..." },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.7f } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.7f, CircleFadeOpacity = 0.5f } },
    new DialogEvent { Name = CharacterName.Blank, Contents = "W..." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "...where am I?" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "You fell into Lake Rasa." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "There’s not much down here except a lot of water, though. And, uh, you. Apparently." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "Do you have a name?" },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.4f } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.4f, CircleFadeOpacity = 0.3f } },
    new DialogEvent { Name = CharacterName.Blank, Contents = "I’m Blank. What’s your name?" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "…’Blank?’ I’ve never heard that one before. I’m Ash." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Is that short for anything?" },

    new DialogEvent { Name = CharacterName.Ash  , Contents = "Okay, tell you what, Blank—" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "I really can’t stand the smell down here. It brings back some bad memories." },
    //new DialogEvent { Name = CharacterName.Ash  , Contents = "Okay, tell you what, Blank—I really can’t stand the smell down here. It brings back some bad memories." },

    new DialogEvent { Name = CharacterName.Ash  , Contents = "Mind if we talk as we head up to the city?" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Where’s the city?" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "It’s up this way. Follow me!" },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.0f } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.0f, CircleFadeOpacity = 0.0f } },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "Actually, why don’t we make this into a little game?" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "I’ll head up to a resting point, and every time you catch up, I’ll answer one of your questions." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "Sound good?" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Sure!" },
  };

  public static List<DialogEvent> Trudialog = new List<DialogEvent> {
    new DialogEvent { Name = CharacterName.Blank, Contents = "<color=red>Um... hi.</color>" },
    new DialogEvent { Name = CharacterName.Trudy, Contents = "Hewwwwoooo" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "I am quite alarmed right now." },
  };
}