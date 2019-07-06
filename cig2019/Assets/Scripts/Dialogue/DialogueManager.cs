using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Data;
using OfficeOpenXml;

public class DialogueManager : MonoBehaviour
{
    public List<List<string>> Data;
    public DialogueReader Patient;
    public DialogueSelect Select1;
    public DialogueSelect Select2;
    public GameObject Gun;
    public int Now = 0;

    private void Start()
    {
        ReadData("1.xlsx");
        DialogueIn();
    }

    // 传入Xlsx文件夹下的文件名，读取一整场对话
    public void ReadData(string fileName)
    {
        Data = LoadExcelData.instance.LoadData(fileName);
    }

    // 进入Now指示的对话
    public void DialogueIn()
    {
        if(Data[Now][1] != "")
        {
            Patient.SetText(Data[Now][1]);
            if (Data[Now][2] != "")
            {
                Select1.gameObject.SetActive(true);
                Select1.SetText(Data[Now][2]);
            }
            else
            {
                Select1.gameObject.SetActive(false);
            }
            if (Data[Now][5] != "")
            {
                Select2.gameObject.SetActive(true);
                Select2.SetText(Data[Now][5]);
            }
            else
            {
                Select2.gameObject.SetActive(false);
            }
        }
        if(!Select1.gameObject.activeSelf && !Select2.gameObject.activeSelf)
        {
            Gun.SetActive(true);
        }
    }

    // 收到回复的行为
    public void Responce(int number)
    {
        if(number == 1)
        {
            Patient.SetText(Data[Now][3]);
            Reaction(Data[Now][4]);
        }
        else if(number == 2)
        {
            Patient.SetText(Data[Now][6]);
            Reaction(Data[Now][7]);
        }
        else
        {
            Debug.LogWarning("Number设置错误！");
        }
        Now += 1;
        // 说下一句话
        Invoke("DialogueIn", 2);
    }

    // 定义每种行为的方法
    private void Reaction(string act)
    {
        switch (act)
        {
            case "aaa":
                Debug.Log("做了aaa");
                break;
            case "bbb":
                Debug.Log("做了bbb");
                break;
            default:
                Debug.Log(act);
                break;
        }
    }

    public void EndADay()
    {
        Debug.Log("忙碌的一天结束了");
    }
}
