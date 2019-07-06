using Fluent;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This example introduces writing text to the dialog.
/// You also need a WriteHandler to specify the Text UI element to write to.
/// Write() will write some text with a pause at the end.
/// </summary>
public class Conversation5 : MyFluentDialogue
{
    public TextMeshProUGUI OtherTextElement;

    public override void OnFinish()
    {
        OtherTextElement.text = "";
        base.OnFinish();
    }

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0, "<color=#0000ffff>Write</color> writes to a text element") *

            Options
            (
                Option("This dialog looks different!") *
                    Write(0.5f, "Yes!") *
                    Write("Dialogs and options are user definable prefabs!\nYou need to press a button to continue this Write").WaitForButton() *

                Option("Write to an element specified in code") *
                    Write(OtherTextElement, 0, "This text element is specified in code!") *

                Option("Bye") *
                    Hide() *
                    Yell("Bye bye!") *
                    End()
             );
    }
}
