using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health = 20;
    public int shield = 20;
    public float dieTime;
    public float elapsedTime = 0;

    public void TakeDamage(int damage)
    {
        Debug.Log(health + ": " + damage);
        if(shield > 0 ) 
        {
            shield -= damage;
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
