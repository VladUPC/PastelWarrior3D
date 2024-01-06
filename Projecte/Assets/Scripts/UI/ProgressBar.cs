using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{


    [SerializeField] private Image health;
    [SerializeField] private Text hp;
    [SerializeField] private Image shield;

    public void SetValues(int fvalue, int fbValue,int svalue, int sbValue) {
        hp.text = fvalue.ToString();
        health.fillAmount = ((float) fvalue) / ((float) fbValue);
        shield.fillAmount = ((float) svalue) / ((float) sbValue);
        
    }
}
