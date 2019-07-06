using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public DialogueManager DialogueManager;

    public void OnClicked()
    {
        DialogueManager.EndADay();
        gameObject.SetActive(false);
    }
}
