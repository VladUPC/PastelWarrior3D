using UnityEngine;

public class OrbImpact : MonoBehaviour
{

    public int orbDamage = 20;


    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            PlayerStats player = other.GetComponent<PlayerStats>();
            player.TakeDamage(orbDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

    }
}
