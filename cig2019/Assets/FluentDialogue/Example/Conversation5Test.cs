using Fluent;
using UnityEngine.UI;

/// <summary>
/// This example introduces writing text to the dialog.
/// You also need a WriteHandler to specify the Text UI element to write to.
/// Write() will write some text with a pause at the end.
/// </summary>
public class Conversation5Test : MyFluentDialogue
{
    FluentString[] sentences = FluentString.FromStringArray(new string[] { "I love ...", "CAKE!", "And chained responses!" });

    public override FluentNode Create()
    {
        return
            Show() *
            Write("I am writing to a text box wooo") *
            Yell(sentences) *
            Hide();
    }
}
