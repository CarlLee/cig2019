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
    public DialogueReader Select1;
    public DialogueReader Select2;
    public int Now = 0;

    public void ReadData(string fileName)
    {
        Data = LoadExcelData.instance.LoadData(fileName);
        for(int i = 0; i < Data.Count; i++)
        {
            for(int j = 0; j < Data[i].Count; j++)
            {
                Debug.Log(Data[i][j]);
            }
        }
    }

    public void DialogueStart()
    {

    }

    private void Start()
    {
        ReadData("1.xlsx");
        DialogueStart();
    }
}
