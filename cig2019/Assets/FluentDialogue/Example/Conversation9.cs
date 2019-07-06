using Fluent;

/// <summary>
/// This example demonstrates the concept of Conversation initiators.
/// Up to this point the Conversation3DProximityInitiator was automatically added to our GameObjects.
/// This example has the Conversation3DClickInitiator explicitly added to the game object to allow clicking for initiation.
/// </summary>
public class Conversation9 : MyFluentDialogue
{
    public override FluentNode Create()
    {
        return Yell("You clicked on me!");
    }
}
