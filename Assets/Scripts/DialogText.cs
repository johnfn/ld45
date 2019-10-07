using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public enum CharacterName {
  Blank,
  Ash,
  Trudy
}

public class Fade {
  public float RectangleFadeOpacity = 1.0f;
  public float CircleFadeOpacity = 1.0f;
  public bool Immediate = false;
}

public class DialogEvent {
  public CharacterName Name;
  public string Contents;

  // other events that can happen during dialog

  public List<(EmotionType, string, List<DialogEvent>)> Responses = new List<(EmotionType, string, List<DialogEvent>)>();
  public Fade Fade = null;
  public EmotionType ReceiveEmotion = EmotionType.None; 
  public string instruct = null;
  public bool hideInstruct = false;
}

public class DialogText {
  private static string Curious(string s) {
    return $"<color=#6699ff>{ s }</color>";
  }

  private static string Ash(string s) {
    return $"<color=#999999>{ s }</color>";
  }

  public static List<DialogEvent> FirstDialog = new List<DialogEvent> {
    new DialogEvent { Fade = new Fade { Immediate = true, RectangleFadeOpacity = 1.0f } },

    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Hey!"), instruct = "Press X to continue." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." , hideInstruct = true},

    new DialogEvent { ReceiveEmotion = EmotionType.Curiosity },

    new DialogEvent { Name = CharacterName.Ash  , Contents = $"{ Curious("What happened?") } { Ash("Are you okay?") }" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("What’s your name?") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("I haven’t seen someone down here in ages...") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("And wow, you can tell. What a stench! Whew!") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Hello? Cat got your tongue? Can you understand me?") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Hey, we can’t figure out what’s going on until you talk to me. Aren't you ") + Curious("curious") + Ash(", too?") }, 
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.9f, } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.9f, CircleFadeOpacity = 0.5f } },
    new DialogEvent { Name = CharacterName.Blank, Contents = Curious("...curious?...") },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.7f } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.7f, CircleFadeOpacity = 0.5f } },
    new DialogEvent { Name = CharacterName.Blank, Contents = Ash("W...") },
    new DialogEvent { Name = CharacterName.Blank, Contents = Curious("...where am I?") },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("You fell into Lake Rasa.") },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("There’s not much down here except a lot of water, though. And, uh, you. Apparently.") },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Do you have a name?") },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.4f } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.4f, CircleFadeOpacity = 0.3f } },
    new DialogEvent { Name = CharacterName.Blank, Contents = "I’m Blank. What’s your name?" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = "...'Blank?' I've never heard that one before. I’m Ash." },
    new DialogEvent { Name = CharacterName.Blank, Contents = Curious("Is that short for anything?") },

    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Okay, tell you what, Blank—") },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("I really can’t stand the smell down here. It brings back some bad memories.") },

    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Mind if we talk as we head up to the city?") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Where’s the city?" },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("It’s up this way. Follow me!") },
    new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.0f } }, //new DialogEvent { Fade = new Fade { RectangleFadeOpacity = 0.0f, CircleFadeOpacity = 0.0f } },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Actually, why don’t we make this into a little game?") },
    new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("I’ll head up to a resting point, and every time you catch up, I’ll answer one of your questions.") },
    new DialogEvent {
      Name = CharacterName.Ash, 
      Contents = "Sound good?",
      instruct = "Press a number to choose an option",
      Responses = new List<(EmotionType, string, List<DialogEvent>)> {
        ( 
          EmotionType.None, 
          "Okay",
          new List<DialogEvent> {
            new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Great! Let's get going."), instruct = "Press A/D to move" },
          }
        ),

        ( 
          EmotionType.None, 
          "No. Can't you just answer them now?",
          new List<DialogEvent> {
            new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Glad we're on the same page!") },
            new DialogEvent { Name = CharacterName.Ash, Contents = Ash("I'll see you up there.") },
            new DialogEvent { Name = CharacterName.Ash, Contents = Ash(":-)"), instruct = "Press A/D to move" },
          }
        )
      }
    }
  };

  public static List<DialogEvent> Trudialog = new List<DialogEvent> {
    new DialogEvent { Name = CharacterName.Blank, Contents = "Um..." },
    new DialogEvent {
      Name = CharacterName.Trudy, 
      Contents = "Hewwwwoooo",
      Responses = new List<(EmotionType, string, List<DialogEvent>)> {
        ( 
          EmotionType.Curiosity, 
          Curious("Curiosity"),
          new List<DialogEvent> {
            new DialogEvent { Name = CharacterName.Blank, Contents = "I'm really curious about your lack of non w consonant sounds" },
          }
        ),

        ( 
          EmotionType.None, 
          "No Emotion",
          new List<DialogEvent> {
            new DialogEvent { Name = CharacterName.Blank, Contents = "Bored now, bye" },
          }
        ),
      }
    }
  };

  private static List<DialogEvent> MakeAshQuestionList(
    List<DialogEvent> beforeDialog,
    string questionText, 
    List<DialogEvent> afterDialog
  ) {
    return beforeDialog.Concat(
      new List<DialogEvent> {
        new DialogEvent {
          Name = CharacterName.Ash, 
          Contents = questionText,
          Responses = new List<(EmotionType, string, List<DialogEvent>)> {
            ( 
              EmotionType.Curiosity, 
              Curious("So, is Ash short for anything?"),
              new List<DialogEvent> {
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Yeah it’s short for") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Why do you keep ASHING me that") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("LOL") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("okay sorry i’ll stop") },
              }.Concat(afterDialog).ToList()
            ),

            ( 
              EmotionType.Curiosity, 
              Curious("What's this city we're headed to?"),
              new List<DialogEvent> {
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("It’s called ‘New Hylidae.’ More of a town than a city, really.") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("You’ll like the people there, I think. They’re all super nice!") },
                new DialogEvent { Name = CharacterName.Ash, Contents = "<size=12>(except Marv)</size>" },
              }.Concat(afterDialog).ToList()
            ),

            ( 
              EmotionType.Curiosity, 
              Curious("How did I end up in the lake?"),
              new List<DialogEvent> {
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("I don’t know!") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("I heard a big splash and rushed down to make sure no one was hurt. It’s a pretty long fall.") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Honestly, it’s incredible that you didn’t shatter your spine. Or get flattened on impact with the water and turn into a pancake of blood and viscera.") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash(":D") },
              }.Concat(afterDialog).ToList()
            ),

            ( 
              EmotionType.Curiosity, 
              Curious("Have you been here a long time?"),
              new List<DialogEvent> {
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Yeah. I’ve lived in town for quite a while.") },
                new DialogEvent { Name = CharacterName.Ash, Contents = Ash("It seems small at first, but I think eventually you’ll love it as much as I do!") },
              }.Concat(afterDialog).ToList()
            ),
          }
        }
      }
    ).ToList();
  }

  public static List<DialogEvent> AshTwo = MakeAshQuestionList(
    beforeDialog: new List<DialogEvent>(),
    questionText: "Congrats, you made it! You win a question.",
    afterDialog: new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Well, that's enough of that.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Hey, by the way!") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("I noticed when we talked before you seemed a bit...") + " " + Curious("thoughtful") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Curious("Didn't you wonder why that was?") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Actually, yeah, I did." },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Well, I'd bet you've learned how to be curious!") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Press 1 to look at things with a more curious eye") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("You might notice things you didn't before!"), instruct = "Press 1 to toggle curiosity!" },
    }
  );

  public static List<DialogEvent> AshThree = MakeAshQuestionList(
    beforeDialog: new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("We’re halfway there! Hit me.") },
    },
    questionText: "...with another question. Don't actually hit me.",
    afterDialog: new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Well, I'll see you soon!") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Last one up is a rotten egg!") }
    }
  );

  public static List<DialogEvent> AshFour = MakeAshQuestionList(
    beforeDialog: new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("I have some bad news.") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("You're a rotten egg.") },
    },
    questionText: "But the good news is, you get one more question!",
    afterDialog: new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("And on that note...") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("The city is right up here. Come on!") }
    }
  );
}