using UnityEngine;
using Fluent;

/// <summary>
/// This example shows that the conversations can be paused until something happens
/// </summary>
public class Conversation14 : MyFluentDialogue
{
    public GameObject blueGuy2;
    public GameObject player;
    public bool HasVisitedRed { get; set; }
    bool cameBackForPrize { get; set; }    

    public override void OnStart()
    {
        HasVisitedRed = false;
        cameBackForPrize = false;
    }

    public override FluentNode Create()
    {
        return
            Yell("Stand next to that red guy") *
            ContinueWhen(() => HasVisitedRed) *

            Yell(blueGuy2, "Well done!") *
            Yell(blueGuy2, "Go back for your prize!") *
            ContinueWhen(() => cameBackForPrize) *

            Yell(player, "Prize please!") *
            Yell("It was stolen!") *
            Yell(player, ";-(");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (HasVisitedRed)
            cameBackForPrize = true;
    }

}
