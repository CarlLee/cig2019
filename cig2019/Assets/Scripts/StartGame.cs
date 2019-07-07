using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject Close;

    public void OnClicked()
    {
        SceneManager.LoadScene("Clinic");
    }

    public void CLose()
    {
        Close.SetActive(!Close.activeSelf);
    }
}
