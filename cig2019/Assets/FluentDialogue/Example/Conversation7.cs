using UnityEngine;
using Fluent;

/// <summary>
/// This example shows you how to execute code when an option is selected
/// </summary>
public class Conversation7 : MyFluentDialogue
{
    Color pink = new Color(1.0f, 0.412f, 0.706f);
    Color blue = Color.blue;

    private void PlayerColor(Color color)
    {
        Player.Instance.gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0, "Time to realise your choices affect the world!") *

            Options
            (
                Option("Make me pink!") *
                    Write("Abara Kadabara!") *
                    Do(() => PlayerColor(pink)) *

                Option("Make me blue!") *
                    Hide() *
                    Yell("Abara Kadabara!") *
                    Do(() => PlayerColor(blue)) *
                    Pause(1) *
                    Show() *

                Option("Bye !") *
                    Hide() *
                    Yell("Bye bye!") *
                    End()
             );
    }
}
    


