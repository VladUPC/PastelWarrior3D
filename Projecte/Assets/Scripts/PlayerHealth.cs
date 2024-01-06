using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;
    public int shield = 20;
    public float dieTime;
    public float elapsedTime = 0;

    public void TakeDamage(int damage)
    {
        Debug.Log(health + ": " + damage);
        if(shield > 0 ) 
        {
            shield -= damage;
            if (shield < 0) health += shield;
        }
        else
        {
            health -= damage;

        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (elapsedTime == 0) {}
        elapsedTime += Time.deltaTime;
        if (elapsedTime > dieTime) Destroy(gameObject);
    }
}
