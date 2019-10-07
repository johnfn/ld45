using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public enum CharacterName {
  Blank,
  Ash,
  Trudy,
  Gracie
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

  private static string Compassion(string s) {
    return $"<color=#ff9999>{ s }</color>";
  }

  private static string Affection(string s) {
    return $"<color=#ff0000>{ s }</color>";
  }

  private static string Remorse(string s) {
    return $"<color=#ff88ff>{ s }</color>";
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
    new DialogEvent { Name = CharacterName.Blank, Contents = "Hello..." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..?" },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("Oh, hello there, dearie.") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("How's your day going?") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("My day is going just fine.") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("I got up early and frogclimbed with my friends...") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("...and once we were finished we came back to our frogomes and ate some frogfast.") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("But a cutie like you probably doesn't know anything about having to hit the frogym!") },
    new DialogEvent { Name = CharacterName.Blank, Contents = ".........." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "uh.............................................." },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("I mean, or maybe you do?") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("I don't mean to assume!") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "u h h u h . . . . . ." },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(oh no i hope they like me)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(oh no am i talking too much again)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(go and see a cute cat and ruin it by talking way too much!)") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "You know I can hear you, right?" },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(way to go trudy!)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(it's just like the last time......)") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Trudy is now talking so quietly you can't hear her any more." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "But despite being very confused, you did learn how to feel ever so slight amounts of " + Affection("affection") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Press 3 to see the world in a more affectionate light!" },
  };

  public static List<DialogEvent> Trudialog2 = new List<DialogEvent> {
    new DialogEvent { Name = CharacterName.Blank, Contents = "... ? ? ? ?" },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(they're back again!)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(ah!)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(ah ah ah!)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(ahhhhhhhhhhhhh!)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(i'm so embarrassed...)") },
    new DialogEvent { Name = CharacterName.Trudy, Contents = Affection("(maybe if i look away they wont notice me......)") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Despite Trudy not even talking to you, you can't help but notice that she is a master class in teaching " + Affection("affection") + ".", instruct = "Press 3" },
  };

  public static List<DialogEvent> GracieDialog = new List<DialogEvent> {
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Oh hello there, mister. I'm Gracie." },
    new DialogEvent { Name = CharacterName.Blank, Contents = "What are you doing all the way out here away from town?" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Oh well mister I used to live in New Hylidae but well" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "But well some people didn't want me to live there" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "So I left" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "And now I'm here" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "All by myself" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "What happened?" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Well....." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "It all started with my science project." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "You see I had this science project for class right mister" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "... ok ..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "And on my science project I was doing a study of alllllll the different types of frogs!" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Big frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Small frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Skinny frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Fat frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Spotted frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Striped frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Jumping frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Still frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Normal frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Abnormal frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Frogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "..?" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Dogs" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "???" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "(Just kidding, I didn't study dogs mister.)" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "(I was just making sure you were paying attention.)" },
    new DialogEvent { Name = CharacterName.Blank, Contents = "Uh huh..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "Anyway mister my report was so long and so detailed" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "That all my friends got jealous!" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "..." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "And then they didn't like me any more" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "And they made me live out here" },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "it's ok." },
    new DialogEvent { Name = CharacterName.Gracie, Contents = "It's a little lonely though." },
    new DialogEvent { Name = CharacterName.Blank, Contents = Remorse("Trudy's long, absurd story has arose emotions in you you had previous forgotten.") },
    new DialogEvent { Name = CharacterName.Blank, Contents = Remorse("You feel remorseful for her ridiculous plight.") },
    new DialogEvent { Name = CharacterName.Blank, Contents = "To see things with a remorseful lense, press 4.", instruct = "Press 3" },
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

  public static List<DialogEvent> AshThree = new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("You know...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Long ago, Lake Rasa was a popular place.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Married frogs walking their frog babies up and down the vines was a common sight.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("But now?") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Just look around.") },
      new DialogEvent { Name = CharacterName.Blank, Contents = Curious("What changed?") },
      new DialogEvent { Name = CharacterName.Blank, Contents = Compassion("Empty houses and not a frog in sight.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Hey, was that a little bit of ") + Compassion("compassion?") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Looks like you learned another emotion! You can use this one by pressing 2.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Anyways, we're about halfway there.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Last one up is a rotten egg!"), instruct = "Press 2 to toggle compassion." }
  };

  
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

  public static List<DialogEvent> AshFive = new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("It might not look like it...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("But this right here is a gate.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("Us, we're trapped in here.") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("But if someone were to become master over all four of their emotions...") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("It's said that this gate will open!") },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("And through the gate, you'll pass on from this town and beyond, into the outer world.") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Back to my home." },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("That's what they say.") },
  };

  public static List<DialogEvent> AshSix = new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Your ability to feel emotion!") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("It surpasses even my own!") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("...") },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("This gate may just open for you. Try and see!") },
  };

  public static List<DialogEvent> GateNoGood = new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Blank, Contents = "You brace yourself with anticipation, and walk through the gate..." },
      new DialogEvent { Name = CharacterName.Blank, Contents = "..." },
      new DialogEvent { Name = CharacterName.Blank, Contents = "...only to come out the other side." },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Huh." },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Guess it's not working." },
  };

  public static List<DialogEvent> GateGood = new List<DialogEvent> {
      new DialogEvent { Name = CharacterName.Blank, Contents = "Oh no!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Ash!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Can you write this script?" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "I wanna go to bed!" },
      new DialogEvent { Name = CharacterName.Ash  , Contents = Ash("No.") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "This is definitely the worst thing you'll ever do to me." },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Ash!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "I can't believe it!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "You were the bad guy the whole time!" },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Yep.") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "You made trudy horribly nervous!" },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Uhh... no") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "You stole Gracie's science fair project!" },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("Err?") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "You were the one who made Ludum Dare end 3 hours early! It was ALL YOU!" },
      new DialogEvent { Name = CharacterName.Ash, Contents = Ash("What?") },
      new DialogEvent { Name = CharacterName.Blank, Contents = "Well... be that as it may!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "I won't forget this, Ash!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "I'll be back to defeat you..." },
      new DialogEvent { Name = CharacterName.Blank, Contents = "In LUDUM DARE 46!!!" },
      new DialogEvent { Name = CharacterName.Blank, Contents = "[CREDITS]" }, // special value to show credits scene!
  };
}