using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public static Medicine Instance;
    public List<List<string>> Data;
    public List<List<string>> Shape;
    public List<Sprite> Medi;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Data = LoadExcelData.instance.LoadData("Medicine.xlsx");
        Shape = LoadExcelData.instance.LoadData("Shape.xlsx");
    }
}
