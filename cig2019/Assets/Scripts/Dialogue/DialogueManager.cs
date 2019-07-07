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
    public TextChanger Doctor;
    public TextChanger Select1;
    public TextChanger Select2;
    //public GameObject Gun;
    public int Now = 0;
    public float WaitTime = 0.2f;

    private void Awake()
    {
        instance = this;
    }

    public void StartDialogue(string fileName)
    {
        Now = 0;
        ReadData(fileName);
        DialogueIn();
        Debug.LogWarning("!!");
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
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(DialogueLoop());
    }

    // 病人对话循环
    private IEnumerator DialogueLoop()
    {
        string[] dias = Data[Now][1].Split("_".ToCharArray());
        for (int i = 0 ;i<dias.Length; i++)
        {
            Patient.SetText(dias[i]);
            yield return new WaitForSeconds(WaitTime);
            while (true)
            {               
                if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
                {
                    //Debug.Log("出循环");                   
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        SelectIn();
        yield return 0;
    }

    public void SelectIn()
    {
        if (Data[Now][1] != "")
        {
            if (Data[Now][2] != "")
            {
                Select1.gameObject.SetActive(true);
                Select1.SetText(Data[Now][2]);
            }
            else
            {
                Select1.gameObject.SetActive(false);
            }
            if (Data[Now][6] != "")
            {
                Select2.gameObject.SetActive(true);
                Select2.SetText(Data[Now][6]);
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
        if (Data[Now][3] != "") // 进入对话循环
        {
            Doctor.gameObject.SetActive(true);
            StartCoroutine(SelectLoop(1));
        }
        else
        {
            Reaction(Data[Now][5]);
        }
        // 关闭对话框
        Select1.gameObject.SetActive(false);
        Select2.gameObject.SetActive(false);
    }
    // 当点中2选项
    public void OnSelect2Clicked()
    {
        if (Data[Now][7] != "")
        {
            Doctor.gameObject.SetActive(true);
            StartCoroutine(SelectLoop(2));
        }
        else
        {
            Reaction(Data[Now][9]);
        }

        Select1.gameObject.SetActive(false);
        Select2.gameObject.SetActive(false);
    }
    // 医生反应循环
    private IEnumerator SelectLoop(int selectNumber)
    {
        //yield return new WaitForSeconds(WaitTime);
        Doctor.gameObject.SetActive(true);

        string[] dias = Data[Now][selectNumber * 4 - 1].Split("_".ToCharArray());

        for (int i = 0; i < dias.Length; i++)
        {
            Doctor.SetText(dias[i]);
            yield return new WaitForSeconds(WaitTime);
            while (true)
            {
                if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
                {
                   // Debug.Log("出循环");
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        StartCoroutine(ResponceLoop(selectNumber));
        yield return 0;
    }
    // 病人回复循环
    private IEnumerator ResponceLoop(int selectNumber)
    {
        string[] dias = Data[Now][selectNumber * 4].Split("_".ToCharArray());

        for (int i = 0; i < dias.Length; i++)
        {
            Patient.SetText(dias[i]);
            yield return new WaitForSeconds(WaitTime);
            while (true)
            {
                if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
                {
                    //Debug.Log("出循环");
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        Reaction(Data[Now][selectNumber * 4 + 1]);
        Doctor.gameObject.SetActive(false);

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(WaitTime);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        DialogueIn(); // 在这里达成循环

        yield return 0;
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
