using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUP : MonoBehaviour
{
    public ClinicManager ClinicManager;
    public Text RestMoney;
    public Text RestMoneyNumber;
    public Text Rent;
    public Text RentNumber;
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


        RestMoneyNumber.text = ClinicManager.MoneyHave.ToString();
        RentNumber.text = "- " + ClinicManager.Rent.ToString();
        // TODO 计算各药物收入
        MoneyNumber.text = (ClinicManager.MoneyHave - ClinicManager.Rent).ToString(); // TODO  将药物收入算进来

        StartCoroutine(ShowBill());
    }

    private IEnumerator ShowBill()
    {
        yield return new WaitForSeconds(WaitTime);

        RestMoney.gameObject.SetActive(true);
        RestMoneyNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitTime);

        Rent.gameObject.SetActive(true);
        RentNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitTime);

        // 售出药物的显示
        if (false)
        {
            for(int i = 0; ; i++)
            {
                yield return new WaitForSeconds(WaitTime);
            }
        }

        Money.gameObject.SetActive(true);
        MoneyNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitTime);

        ClinicManager.NextDayButtom.SetActive(true);
        yield return 0;
    }
}
