using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClinicManager : MonoBehaviour
{
    public static ClinicManager instance;

    public int MoneyHave = 0;
    public int Day = 0;
    public int[] Medicines = new int[100];
    public int[] MedicinesCost = new int[100];
    public int Rent;
    public List<List<string>> Data;
    public ClearUP ClearUP;

    public GameObject DialogueSys;
    public GameObject NextDayButtom;
    public GameObject MoneyNRentUI;

    public Text MoneyNRentText;
    public Text DayText;
    private int MaxDay;

    public void Initiate()
    {
        ReadData("GameSetting.xlsx");
        RefleshRent(int.Parse(Data[Day][1]));
        RefleshDay(Day);
        MaxDay = Data.Count;
        Debug.Log("MaxDay: " + MaxDay);
        DialogueSys.SetActive(true);
        DialogueManager.instance.StartDialogue((Day+1).ToString() + ".xlsx");
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

        DialogueSys.SetActive(true);
        DialogueManager.instance.StartDialogue((Day + 1).ToString() + ".xlsx");

        NextDayButtom.SetActive(false);
        ClearUP.gameObject.SetActive(false);
        MoneyNRentUI.SetActive(true);
        RefleshRent(int.Parse(Data[Day][1]));
    }

    public void DayEnd()
    {
        DialogueSys.SetActive(false);
        ClearUP.gameObject.SetActive(true);
        //NextDayButtom.SetActive(true);
        MoneyNRentUI.SetActive(false);
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
        MoneyNRentText.text = "现金： " + MoneyHave.ToString() + "\n" + "今日房租： " + Rent.ToString();
    }

    public void RefleshRent(int rent)
    {
        Rent = rent;
        MoneyNRentText.text = "现金： " + MoneyHave.ToString() + "\n" + "今日房租： " + Rent.ToString();
    }

    public void GameOver()
    {
        Debug.Log("1111");
        Application.Quit();
    }
}
