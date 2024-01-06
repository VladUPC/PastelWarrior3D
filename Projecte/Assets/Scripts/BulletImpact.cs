using UnityEngine;

public class BulletImpact : MonoBehaviour
{

    public int bulletDamage = 10;
    private AudioSource audio;
    private MeshRenderer meshRenderer;
    private TrailRenderer trailRenderer;
    private bool impacted = false;



    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        trailRenderer = this.GetComponent<TrailRenderer>();


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy" && !impacted)
        {
            PlaySoundAndDestroy();
            EnemyStats enemyHealth = other.GetComponent<EnemyStats>();
            enemyHealth.TakeDamage(bulletDamage);

        }

        if (other.gameObject.tag == "Boss" && !impacted) 
        {
            PlaySoundAndDestroy();
            BossStats enemyHealth = other.GetComponent<BossStats>();
            enemyHealth.TakeDamage(bulletDamage);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

    }
    private void PlaySoundAndDestroy()
    {
        impacted = true;
        meshRenderer.enabled = false;
        trailRenderer.enabled = false;
        audio.Play();
        Destroy(gameObject, audio.clip.length);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("KLK");
        
    }


}
