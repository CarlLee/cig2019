using Fluent;

/// <summary>
/// This example shows how to let certain responses happen only one time for the current conversation.
/// When the conversation restarts those responses will happen again.
/// </summary>
public class Conversation11 : MyFluentDialogue
{
    public override FluentNode Create()
    {
        return
            Show() *
            Write("Only once responses will happen again when the conversation is restarted") *
            Options
            (
                Option("What is the secret message ?") *
                    Once
                    (
                        Write("OOOGABOOGA!") 
                    ) *
                    Write("Restart the conversation to hear the secret again") *

                Option("Bye!") *
                    Hide() *
                    Yell("Bye bye!") *
                    End()
            );
    }
}
