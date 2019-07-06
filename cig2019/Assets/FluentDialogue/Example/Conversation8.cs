using UnityEngine;
using Fluent;

/// <summary>
/// This example shows you that options can be shown or hidden based on code.
/// </summary>
public class Conversation8 : MyFluentDialogue
{
    public GameObject player;
    bool gotHorsey = false;

    public override FluentNode Create()
    {
        return
            Show() *
            Write(0, "Options can be shown or hidden based on code!") *

            Options
            (
                Option("I want a HORSEY!").VisibleIf(() => !gotHorsey) *
                    Write("Here you go!") *
                    Do(() => gotHorsey = true) *

                Option("My horsey died! Give me another one!").VisibleIf(() => gotHorsey) *
                    Write("That was the only one in the world!") *
                    Hide() *
                    Yell(player, ":(") *
                    Show() *

                Option("Bye!") *
                    Hide() *
                    Yell("Bye bye!") *
                    End()
             );
    }
}
