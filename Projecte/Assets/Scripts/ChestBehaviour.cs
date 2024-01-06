using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public float timeDestroyChest = 1f;
    private bool chestDestroyed = false;
    private bool inContactWithPlayer = false;

    public GameObject player;

    public AudioSource audio;
    public AudioClip openChest;


    private void Update()
    {



        if (Input.GetKeyDown(KeyCode.F) && inContactWithPlayer && !chestDestroyed) 
        {
            this.GetComponent<Animator>().SetBool("Open", true);
            audio.clip = openChest;
            audio.Play();
            GiveBullets();
            ChestDestroy();
            if(!player.GetComponent<SwitchWeapon>().secondWeaponFound)
            {
                UnityEngine.Debug.Log("You have found an Axe Weapon");
                player.GetComponent<SwitchWeapon>().secondWeaponFound = true;
            }
        }
    }


    private void GiveBullets()
    {
        int numPistolBullets = Random.Range(20, 50);
        int numGunBullets = Random.Range(20, 50);
        player.GetComponent<PlayerStats>().GetAmmo(true, numPistolBullets);
        player.GetComponent<PlayerStats>().GetAmmo(false, numGunBullets);
    }

    private void ChestDestroy()
    {
        chestDestroyed = true;
        Destroy(gameObject, timeDestroyChest);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inContactWithPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inContactWithPlayer = false;
        }
    }



}
