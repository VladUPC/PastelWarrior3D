using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour
{
    public int baseHealth = 100;
    public int health = 100;
    public int baseShield = 20;
    public int shield = 20;
    public float dieTime;
    public float elapsedTime = 0;
    public Animator animator;

    public virtual void TakeDamage(int damage)
    {
        if(shield > 0 ) 
        {
            damage = damage/2;
            shield -= damage;
            if (shield < 0) health += shield;
        }
        else
        {
            if (shield < 0) shield = 0;
            health -= damage;

        }

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int addedHealth)
    {
        health += addedHealth;
        if (health > baseHealth) {
            shield += (health-baseHealth)/2;
            health = baseHealth;
        }
    }



    public virtual void Die() {
        StartCoroutine(ClearDeath());
        animator.SetBool("Die", true);
    }
    
    IEnumerator ClearDeath() {
        yield return new WaitForSeconds(dieTime);      
        Destroy(gameObject);
    }
}
