using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSelect : MonoBehaviour
{
    public Text Text;
    public string Act;
    public DialogueManager DialogueManager;
    public GameObject OtherSelect;
    [Range(1,2)]
    public int Nomber;

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void OnClicked()
    {
        DialogueManager.Responce(Nomber);
        OtherSelect.SetActive(false);
        gameObject.SetActive(false);
    }
}
