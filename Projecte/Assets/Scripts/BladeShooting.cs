using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BladeShooting : GunShooting
{

    override public void ShootWeapon()
    {
        if(canAttack)
        {
            base.PlayShootSound();

            StartCoroutine(ShootThreeTimes());
            base.StartCoroutine(DelayBetweenShoots());

        }

    }

    IEnumerator ShootThreeTimes() 
    {
        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 3; i++) 
        {
            base.Shoot();
            yield return new WaitForSeconds(0.1f);
        }

    }

}
