using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriCell : MonoBehaviour
{
    public GameObject tri0;
    public GameObject tri1;
    
    public void UpdatePresentation(uint mask)
    {
        uint bit = mask & 1;
        if(bit == 1)
        {
            tri0.SetActive(true);
        }
        else
        {
            tri0.SetActive(false);
        }

        bit = mask >> 1 & 1;
        if (bit == 1)
        {
            tri1.SetActive(true);
        }
        else
        {
            tri1.SetActive(false);
        }
    }
}
