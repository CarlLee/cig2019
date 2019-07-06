using Fluent;

/// <summary>
/// There are two things explained in this example.
/// The first is that we now inherit from MyConversation which contains the code to stop the player from moving
/// The second is that concept of chaining responses, they will each be executed from Start() to End()
/// </summary>
public class Conversation2 : MyFluentDialogue
{
    FluentString[] sentences = FluentString.FromStringArray(new string[] { "I love ...", "CAKE!", "And chained responses!" });

    public override FluentNode Create()
    {
        return
            Yell(sentences);
    }
}
