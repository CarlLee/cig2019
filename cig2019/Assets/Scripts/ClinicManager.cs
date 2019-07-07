﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClinicManager : MonoBehaviour
{
    public static ClinicManager instance;

    public int MoneyHave = 0;
    public int Day = -1;
    public int[] Medicines = new int[100];
    public int[] MedicinesCost = new int[100];
    public int Rent;
    public List<List<string>> Data;
    public ClearUP ClearUP;

    public GameObject DialogueSys;
    public GameObject NextDayButtom;
    public GameObject MoneyNRentUI;

    public List<Sprite> Sprites;
    public SpriteRenderer PatientSpr;

    //public Text MoneyNRentText;
    public Text MoneyT;
    public Text RentT;
    public Text DayText;
    private int MaxDay;

    public void Initiate()
    {
        ReadData("GameSetting.xlsx");
        MaxDay = Data.Count;
        Debug.Log("MaxDay: " + MaxDay);
        NextDay();
    }
    // 传入Xlsx文件夹下的文件名，读取游戏配置数据
    public void ReadData(string fileName)
    {
        Data = LoadExcelData.instance.LoadData(fileName);
    }

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Initiate();
    }
    
    public void NextDay()
    {
        if (Day + 1 >= MaxDay)
        {
            GameOver();
            return;
        }
        // 刷新每日数据
        RefleshDay(Day + 1);
        RefleshMoney(MoneyHave);
        DialogueSys.SetActive(true);
        DialogueManager.instance.StartDialogue((Day + 1).ToString() + ".xlsx");
        LoadPatient(Day);
        NextDayButtom.SetActive(false);
        ClearUP.gameObject.SetActive(false);
        //MoneyNRentUI.SetActive(true);
        RefleshRent(int.Parse(Data[Day][1]));
    }

    private void LoadPatient(int day)
    {
        PatientSpr.sprite = Sprites[day];
    }

    public void DayEnd()
    {
        DialogueSys.SetActive(false);
        ClearUP.gameObject.SetActive(true);
        //NextDayButtom.SetActive(true);
        //MoneyNRentUI.SetActive(false);
        RefleshRent(int.Parse(Data[Day][1]));
    }

    public void RefleshDay(int day)
    {
        Day = day;
        DayText.text = "第" + (1 + day).ToString() + "天";
    }

    public void RefleshMoney(int money)
    {
        MoneyHave = money;
        MoneyT.text = MoneyHave.ToString();
    }

    public void RefleshRent(int rent)
    {
        Rent = rent;
        RentT.text = Rent.ToString();
    }

    public void GameOver()
    {
        Debug.Log("1111");
        Application.Quit();
    }
}
