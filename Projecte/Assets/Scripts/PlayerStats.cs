using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : CharacterStats
{

    private PlayerHUD hud;
    private bool usingFirstWeapon;
    public int ammo1;
    public int maxAmmo1 = 50;
    public int ammo2;
    public int maxAmmo2 = 20;
    private bool noDamage = false;
    private bool godMode = false;
    public BladeShooting weapon1;
    public GunShooting weapon2;

    //audio
    public AudioSource audio;
    public AudioClip die;
    public AudioClip hit;

    private bool isDead = false;

    public void Update() {
        if (Input.GetKeyUp(KeyCode.C)) {
            if (usingFirstWeapon && ammo1 > 0 && weapon1.canAttack) {
                ammo1 -= 3;
                if (ammo1 < 0) ammo1 = 0;
                hud.SetAmmo(true, ammo1);
                weapon1.ShootWeapon();
            }
            if (!usingFirstWeapon && ammo2 > 0 && weapon2.canAttack) {
                ammo2 -= 1;
                if (ammo2 < 0) ammo2 = 0;
                hud.SetAmmo(false, ammo2);
                weapon2.ShootWeapon();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.M)) {
            ammo1 = maxAmmo1;
            ammo2 = maxAmmo2;
            hud.SetAmmo(true, ammo1);
            hud.SetAmmo(false, ammo2);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            godMode = !godMode;
            noDamage = !noDamage;
        }
    }

    public void Start() {
        ammo1 = maxAmmo1;
        ammo2 = maxAmmo2;
        hud = GetComponent<PlayerHUD>();
        hud.UpdateHealth(health, baseHealth, shield, baseShield);
        hud.SetAmmo(true, ammo1);
        usingFirstWeapon = true;
    }

    public override void  TakeDamage(int damage)
    {
        if (!noDamage) base.TakeDamage(damage);
        if (health < 0) health = 0;
        if(! isDead)PlayHitSound();
        hud.UpdateHealth(health, baseHealth, shield, baseShield);
    }

    public void SwitchWeapon(bool first) {
        usingFirstWeapon = first;
        hud.SetActiveWeapon(usingFirstWeapon);
    }

    public void FoundSecondWeapon() {
        hud.SecondWeaponFound();
        hud.SetAmmo(false,ammo2);
    }

    public void SetInvulnerable(bool b) {
        noDamage = b;
    }

    public bool IsInGodMode() 
    { 
        return godMode; 
    }

    public void GetAmmo(bool first, int ammo) {
        if (first) {
            ammo1 += ammo;
            if (ammo1 > maxAmmo1) ammo1 = maxAmmo1;
            hud.SetAmmo(true, ammo1);
        }
        else {
            ammo2 += ammo;
            if (ammo2 > maxAmmo2) ammo2 = maxAmmo2;
            hud.SetAmmo(false, ammo2);
        }
    }

    public override void Die() {
        animator.SetBool("Die", true);
        isDead = true;
        PlayDieSound();
        StartCoroutine(ChangeSceneAfterDeath());
    }
    
    IEnumerator ChangeSceneAfterDeath() {
        yield return new WaitForSeconds(dieTime);      
        SceneManager.LoadScene("DieScene");
        Destroy(gameObject);
    }

    private void PlayDieSound()
    {
        UnityEngine.Debug.Log("Die");
        audio.clip = die;
        audio.Play();
    }

    private void PlayHitSound()
    {
        audio.clip = hit;
        audio.Play();
    }
}
