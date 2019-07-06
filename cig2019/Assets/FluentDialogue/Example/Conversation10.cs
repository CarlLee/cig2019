using Fluent;

/// <summary>
/// This example shows how to do multiple things at the same time.
/// This way we can write text while playing sound for example.
/// </summary>
public class Conversation10 : MyFluentDialogue
{
    FluentNode Speak(string text, string soundResource)
    {
        return Parallel
            (
                Write(text) *
                Sound(soundResource)
            );
    }

    public override FluentNode Create()
    {
        return 
            Show() *            
            Options
            (
                Write(0, "We can run multiple responses in parallel!") *
                Option("Make multiple responses happen at the same time!") *
                    Speak("Here is some text being printed, while a sound is being played!", "Sounds/hello") *

                Option("Can I chain parallel nodes ?") *
                    Write("What follows is both sound and text that has to finish before the next sound and text can play") *
                    Speak("Yes", "Sounds/yes") *
                    Speak("We can chain parallel responses", "Sounds/chainparallel") *

                Option("Bye!") *
                    Hide() *
                    Parallel
                    (
                        Yell("Bye bye!") *
                        Sound("Sounds/byebye")
                    ) *
                    End()
            );
    }
}
