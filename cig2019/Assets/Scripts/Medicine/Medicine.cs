using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public static Medicine Instance;
    public List<List<string>> Data;
    public List<List<string>> Shape;

    private void Awake()
    {
        Instance = this;
        Data = LoadExcelData.instance.LoadData("Medicine.xlsx");
        Shape = LoadExcelData.instance.LoadData("Shape.xlsx");
    }
}
