using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public Transform createBulletPoint;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public GameObject player;
    public int ammo;
    public int maxAmmo = 100;
    Animator playerAnimator;
    public GameObject blackKnight;

    public bool canAttack = true;
    public float fireDelay = 0.1f;

    public AudioSource audio;
    public AudioClip attackSound;


    private void Start()
    {
        playerAnimator = blackKnight.GetComponent<Animator>();
        playerAnimator.SetBool("Attack", false);
        ammo = 50;
    }


    public virtual void ShootWeapon()
    {
        PlayShootSound();
        StartCoroutine(ShootWithDelay());
        StartCoroutine(DelayBetweenShoots());

    }

    protected void PlayShootSound()
    {
        audio.clip = attackSound;
        audio.Play();
    }



    void OnEnable()
    {
        playerAnimator = blackKnight.GetComponent<Animator>();
        playerAnimator.SetBool("Attack", false);
        canAttack = true;
    }

    protected IEnumerator DelayBetweenShoots() 
    {
        canAttack = false;
        playerAnimator.SetBool("Attack", true);

        yield return new WaitForSeconds(fireDelay);
        playerAnimator.SetBool("Attack", false);

        canAttack = true;
    }

    IEnumerator ShootWithDelay()
    {
        yield return new WaitForSeconds(0.4f);

        Shoot();
    }

    protected void Shoot()
    {

        var bullet = Instantiate(bulletPrefab, createBulletPoint.position, createBulletPoint.rotation);

        Transform transform = bullet.GetComponent<Transform>();
        float angle = Mathf.PI / 8;

        if (player.GetComponent<PlayerMove>().movingRight) angle *= -1;

        StartCoroutine(MoveAround(transform, angle));

    }


    protected IEnumerator MoveAround(Transform bulletTransform, float angle)
    {
        while (bulletTransform != null) 
        {
            Vector3 center = new Vector3(0, bulletTransform.position.y, 0);
            bulletTransform.RotateAround(center, Vector3.up, angle * bulletSpeed * Time.deltaTime) ;
            yield return null;
        }
    }

}


