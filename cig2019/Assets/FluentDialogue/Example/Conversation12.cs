using Fluent;

/// <summary>
/// This example shows how we can execute code under certain conditions
/// </summary>
public class Conversation12 : MyFluentDialogue
{
    int timesVisited = 0;

    public override FluentNode Create()
    {
        return
            Do(() => timesVisited++) *

            If(() => timesVisited == 1,
                Yell("I havent seen you before") *
                Yell("Lets be friends!") *
                Yell("Talk to me again sometime")
            ) *

            If(() => timesVisited == 2,
                Yell("I'm going to count your visits!")
            ) *

            If(() => timesVisited >= 2,
                Yell("Visit number " + timesVisited));

    }
}
