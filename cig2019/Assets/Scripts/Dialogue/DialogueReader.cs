using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueReader : MonoBehaviour
{
    public Text Text;
    
    public void SetText(string text)
    {
        Text.text = text;
    }
}

