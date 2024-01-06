using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{


    [SerializeField] private Image health;
    [SerializeField] private Image shield;

    public void SetValues(int fvalue, int fbValue, int svalue, int sbvalue) {
        health.fillAmount = ((float) fvalue) / ((float) fbValue);
        shield.fillAmount = ((float) svalue) / ((float) sbvalue);
    }
}
