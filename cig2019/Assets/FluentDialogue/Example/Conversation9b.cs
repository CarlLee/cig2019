using Fluent;
using UnityEngine.UI;

/// <summary>
/// This example shows how to start a conversation with a button
/// </summary>
public class Conversation9b : MyFluentDialogue
{
    public Button StartButton;

    public void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            StartButton.interactable = false;
            Run();
        });
    }

    public override void OnFinish()
    {
        base.OnFinish();
        StartButton.interactable = true;
    }

    public override FluentNode Create()
    {
        return
            Yell("You just initiated a converation with a button!");
    }
}
