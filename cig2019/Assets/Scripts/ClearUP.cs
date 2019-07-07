using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUP : MonoBehaviour
{
    public Text RestMoney;
    public Text RestMoneyNumber;
    public Text Rent;
    public Text RentNumber;
    public Text Sold;
    public List<Text> SoldMedicine;
    public Text Money;
    public Text MoneyNumber;

    public float WaitTime = 0;

    private void OnEnable()
    {
        // 先隐藏各部分
        RestMoney.gameObject.SetActive(false);
        RestMoneyNumber.gameObject.SetActive(false);
        Rent.gameObject.SetActive(false);
        RentNumber.gameObject.SetActive(false);
        Money.gameObject.SetActive(false);
        MoneyNumber.gameObject.SetActive(false);
        Sold.gameObject.SetActive(false);
        for(int i = 0; i < SoldMedicine.Count; i++)
        {
            SoldMedicine[i].transform.parent.gameObject.SetActive(false);
        }

        RestMoneyNumber.text = ClinicManager.instance.MoneyHave.ToString();
        RentNumber.text = "- " + ClinicManager.instance.Rent.ToString();

        int medicineIndex = 0;
        int mediUI = 0;
        int showMedicine = 0;

        for(int i = mediUI; i < SoldMedicine.Count; i++)
        {
            for(int j = medicineIndex;j< ClinicManager.instance.MedicinesCost.Length; j++)
            {
                medicineIndex++;
                if (ClinicManager.instance.MedicinesCost[j] > 0)
                {
                    SoldMedicine[i].text = "X " + ClinicManager.instance.MedicinesCost[j].ToString() + " = " + (int.Parse(Medicine.Instance.Data[j][9]) * ClinicManager.instance.MedicinesCost[j]).ToString();
                    SoldMedicine[i].transform.parent.GetChild(0).GetComponent<Image>().sprite = Resources.Load(Medicine.Instance.Data[j][8]) as Sprite;
                    showMedicine++;
                    break;   
                }
            }
        }

        int mediSold = 0;// TODO 计算各药物收入
        for(int i = 0; i<ClinicManager.instance.MedicinesCost.Length; i++)
        {
            mediSold += ClinicManager.instance.MedicinesCost[i] * int.Parse(Medicine.Instance.Data[i][9]);
        }

        int all = ClinicManager.instance.MoneyHave - ClinicManager.instance.Rent + mediSold;
        MoneyNumber.text = all.ToString(); // TODO  将药物收入算进来
        ClinicManager.instance.RefleshMoney(all);

        StartCoroutine(ShowBill(showMedicine));
    }

    private IEnumerator ShowBill(int show)
    {
        yield return new WaitForSeconds(WaitTime);

        RestMoney.gameObject.SetActive(true);
        RestMoneyNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitTime);

        Rent.gameObject.SetActive(true);
        RentNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitTime);

        // 售出药物的显示
        if (show > 0)
        {
            Sold.gameObject.SetActive(true);
            yield return new WaitForSeconds(WaitTime);
            for (int i = 0;i< show; i++)
            {
                SoldMedicine[i].transform.parent.gameObject.SetActive(true);
                yield return new WaitForSeconds(WaitTime);
            }
        }


        Money.gameObject.SetActive(true);
        MoneyNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitTime);

        ClinicManager.instance.NextDayButtom.SetActive(true);
        yield return 0;
    }
}
