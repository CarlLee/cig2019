using UnityEngine;
using Fluent;

/// <summary>
/// This example shows that you can specify the object that should do the yelling
/// Note that each object specified (eg. player and npc4) needs a Canvas as a child 
/// </summary>
public class Conversation3 : MyFluentDialogue
{
    public GameObject player;
    public GameObject npc4;

    public override FluentNode Create()
    {
        return
            Yell("Anyone can yell!") *
            Yell(player, "Oh!") *
            Yell(player, "Mine looks different!") *
            Yell(npc4, "Mine is a billboard!").Billboard();
    }
}
