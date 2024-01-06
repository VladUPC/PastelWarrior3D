using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{


    [SerializeField] private Text ammo;
    [SerializeField] public GameObject frame;


    public void SetAmmo(int value) {
        ammo.text = value.ToString();
    }

    public void Activate() {
        frame.SetActive(true);
    }

    public void Deactivate() {
        frame.SetActive(false);
    }
}