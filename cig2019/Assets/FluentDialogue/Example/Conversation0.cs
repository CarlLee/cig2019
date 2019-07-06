using Fluent;

/// <summary>
/// This is the bare minimum Conversation
/// For this to work you also need a canvas called 'YellCanvas' as a child GameObject.
/// The first Text component found on the canvas will have it's text updated to "Hello World"
/// </summary>
public class Conversation0 : FluentScript
{
    public override FluentNode Create()
    {
        return Yell("Hello World!");
    }
}
