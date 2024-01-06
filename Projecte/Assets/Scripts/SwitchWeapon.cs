using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{

    public GameObject gun;
    public GameObject pistol;
    public bool secondWeaponFound = false;
    public PlayerStats playerStats;

    public AudioSource audio;
    public AudioClip switchWeapon;

    private bool first;

    void Start()
    {
        secondWeaponFound = false;
        first = true;
    }




    private void Update()
    {

        if (secondWeaponFound) playerStats.FoundSecondWeapon();

        if (Input.GetKeyDown("1") && !first)
        {
            Debug.Log("X");
            first = true;
            changeToPistol();
            DeactivateBullets();
            playerStats.SwitchWeapon(true);

        }

        if (Input.GetKeyDown("2") && first && secondWeaponFound)
        {
            first = false;
            changeToGun();
            DeactivateBullets();
            playerStats.SwitchWeapon(false);

        }
    }

    public void changeToGun()
    {

        gun.SetActive(true);
        pistol.SetActive(false);

        audio.clip = switchWeapon;
        audio.Play();
    }

    public void changeToPistol()
    {

        gun.SetActive(false);
        pistol.SetActive(true);

        audio.clip = switchWeapon;
        audio.Play();


    }

    public void DeactivateBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }
    }

}
