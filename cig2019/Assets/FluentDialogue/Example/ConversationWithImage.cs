using UnityEngine;
using UnityEngine.UI;
using Fluent;

public abstract class ConversationWithImage : MyFluentDialogue
{
    public Sprite CharacterHeadSprite;

    public override void OnStart()
    {
        SetNPCHead();
        base.OnStart();
    }

    private void SetNPCHead()
    {
        OptionsPresenter optionsPresenter = GetComponent<OptionsPresenter>();
        Image image = optionsPresenter.DialogUI.transform.Find("NPCHeadImage").GetComponent<Image>();
        image.sprite = CharacterHeadSprite;
    }
}
