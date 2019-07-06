using Fluent;

/// <summary>
/// This example shows how to inherit from Conversation and add a npc image to the options presenter
/// </summary>
public class Conversation15 : ConversationWithImage
{
    public override FluentNode Create()
    {
        return
        Show() *
        Write(0,
            "I am Balrog-the-great!\n\n" +
            "This is a custom conversation that lets you set an image in the editor which will be displayed by the options presenter") *
        Options
        (
            Option("Bye Balrog!") *
                Hide() *
                Yell("The GREAT!") *
                End()
        );
    }
}
