using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    private EnemyHUD hud;


    public void Start() {
        hud = GetComponent<EnemyHUD>();
        hud.UpdateHealth(health, baseHealth, shield, baseShield);
        hud.Hide();
    }

    public override void  TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        hud.UpdateHealth(health, baseHealth, shield, baseShield);
    }

    
}
