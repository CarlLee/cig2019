using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueReader : MonoBehaviour
{
    public Text Text;

    public List<string> Dialogues;
    public List<int> Relating;
    public int now = 0;

    private void Awake()
    {
        Show(Dialogues[now]);
    }

    public void Next()
    {
        if (Relating[now] >= 0)
        {
            Show(Dialogues[Relating[now]]);
            now = Relating[now];
        }

    }

    public void Show(string str)
    {
        Text.text = str;
    }
}

