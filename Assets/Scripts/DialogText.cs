using UnityEngine;
using System.Collections.Generic;

public enum CharacterName {
  Blank,
  Ash
}

public struct DialogItem {
  public CharacterName Name;
  public string Contents;
}

public class DialogText {
  public static List<DialogItem> FirstDialog = new List<DialogItem> {
    new DialogItem { Name = CharacterName.Blank, Contents = "Hi there" },
    new DialogItem { Name = CharacterName.Ash, Contents = "What's up?" },
  };
}