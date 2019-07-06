using Fluent;

public abstract class MyFluentDialogue : FluentScript
{
    public override void OnFinish() { Player.Instance.CanMove = true; }
    public override void OnStart() { Player.Instance.CanMove = false; }
}
