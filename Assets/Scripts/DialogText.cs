using UnityEngine;
using System.Collections.Generic;

public enum CharacterName {
  Blank,
  Ash
}

public enum SpecialDialogEvent {
  None,
  FadeToBlack,
  SnapToBlack,
  FadeToFiftyPercent
}

public class DialogItem {
  public CharacterName Name;
  public string Contents;
  public SpecialDialogEvent SpecialEvent = SpecialDialogEvent.None;
}

public class DialogText {
  public static List<DialogItem> FirstDialog = new List<DialogItem> {
    new DialogItem { SpecialEvent = SpecialDialogEvent.SnapToBlack },

    new DialogItem { Name = CharacterName.Ash  , Contents = "Hey!" },

    new DialogItem { SpecialEvent = SpecialDialogEvent.FadeToFiftyPercent },

    new DialogItem { Name = CharacterName.Blank, Contents = "..." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "What happened? Are you okay?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "..." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "What’s your name?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "..." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "I haven’t seen someone down here in ages…" },
    new DialogItem { Name = CharacterName.Blank, Contents = "..." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "And wow, you can tell. What a stench! Whew!" },
    new DialogItem { Name = CharacterName.Blank, Contents = "..." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Hello? Cat got your tongue? Can you understand me?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "..." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Hey, we can’t figure out what’s going on until you talk to me. Aren't you curious, too?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "...curious?..." },
    new DialogItem { Name = CharacterName.Blank, Contents = "W..." },
    new DialogItem { Name = CharacterName.Blank, Contents = "...where am I?" },
    new DialogItem { Name = CharacterName.Ash  , Contents = "You fell into Lake Rasa." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "There’s not much down here except a lot of water, though. And, uh, you. Apparently." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Do you have a name?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "I’m Blank. What’s your name?" },
    new DialogItem { Name = CharacterName.Ash  , Contents = "…’Blank?’ I’ve never heard that one before. I’m Ash." },
    new DialogItem { Name = CharacterName.Blank, Contents = "Is that short for anything?" },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Okay, tell you what, Blank—I really can’t stand the smell down here. It brings back some bad memories." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Mind if we talk as we head up to the city?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "Where’s the city?" },
    new DialogItem { Name = CharacterName.Ash  , Contents = "It’s up this way. Follow me!" },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Actually, why don’t we make this into a little game?" },
    new DialogItem { Name = CharacterName.Ash  , Contents = "I’ll head up to a resting point, and every time you catch up, I’ll answer one of your questions." },
    new DialogItem { Name = CharacterName.Ash  , Contents = "Sound good?" },
    new DialogItem { Name = CharacterName.Blank, Contents = "Sure!" },
  };
}