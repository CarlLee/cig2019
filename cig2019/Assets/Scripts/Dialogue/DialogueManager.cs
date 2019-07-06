using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Data;
using OfficeOpenXml;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public List<List<string>> Data;
    public TextChanger Patient;
    public TextChanger Select1;
    public TextChanger Select2;
    //public GameObject Gun;
    public int Now = 0;

    private void Awake()
    {
        instance = this;
    }

    public void StartDialogue(string fileName)
    {
        Now = 0;
        ReadData(fileName);
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
        Patient.gameObject.SetActive(true);
        if (Data[Now][1] != "")
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
    }
    // 当点中1选项
    public void OnSelect1Clicked()
    {
        if (Data[Now][3] != "")
        {
            Patient.SetText(Data[Now][3]);
        }
        else
        {
            Patient.gameObject.SetActive(false);
        }
        Reaction(Data[Now][4]);
        Select1.gameObject.SetActive(false);
        Select2.gameObject.SetActive(false);

    }
    // 当点中2选项
    public void OnSelect2Clicked()
    {
        if (Data[Now][6] != "")
        {
            Patient.SetText(Data[Now][6]);
        }
        else
        {
            Patient.gameObject.SetActive(false);
        }
        Reaction(Data[Now][7]);
        Select1.gameObject.SetActive(false);
        Select2.gameObject.SetActive(false);
    }

    // 发生行为
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
            case "结束":
                EndADay();
                return;
            default:
                Debug.Log(act+"   在这里调用变形方法");
                break;
        }
        Now += 1;
        // 说下一句话
        Invoke("DialogueIn", 0);
    }

    public void EndADay()
    {
        Debug.Log("忙碌的一天结束了   在这里调用每日结算");
        ClinicManager.instance.DayEnd();
    }
}
