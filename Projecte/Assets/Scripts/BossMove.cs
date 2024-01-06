using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameObject player;
    public float radius = 12f;
    private float elapsedTime;
    private float shootTime;
    private bool attack;
    public float speed = 50f;
    public Transform createOrbPoint;
    public GameObject orbPrefab;
    public float bulletSpeed;
    private bool hit;
    public BossStats stats;


    private AudioSource audio;
    private bool bossRoar = true;


    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
 
    }

    private void Update()
    {
        
        elapsedTime += Time.deltaTime;
        shootTime += Time.deltaTime;
        
        if (!attack) {
            Vector3 newPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            Quaternion objective = Quaternion.LookRotation(newPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, objective, speed*Time.deltaTime);


            if (elapsedTime > 5.3) {
                attack = true;
                stats.SetHit(true);;
                GetComponent<Animator>().SetBool("Attack", true);
                elapsedTime = 0;
            }

            if (shootTime > 1.5) {
                shootTime = 0;
                float angleOrb = -Mathf.Atan2(player.transform.position.x, player.transform.position.z)-Mathf.PI/2;
                Vector3 newOrbPosition = new Vector3(radius*Mathf.Cos(angleOrb), player.transform.position.y, radius*Mathf.Sin(angleOrb));

                var orb1 = Instantiate(orbPrefab, newOrbPosition, transform.rotation);
                var orb2 = Instantiate(orbPrefab, newOrbPosition, transform.rotation);
                Transform orbTransform1 = orb1.GetComponent<Transform>();
                Transform orbTransform2 = orb2.GetComponent<Transform>();
                float angle = Mathf.PI/8;



                StartCoroutine(MoveAround(orbTransform1, angle));
                StartCoroutine(MoveAround(orbTransform2, -angle));
            }
        }
        else {
            if(bossRoar)
            {
                bossRoar = false;
                StartCoroutine(BossRoarAgain());

            }
            if (elapsedTime > 3.6) {
                GetComponent<Animator>().SetBool("Attack", false);
                attack = false;
            }
            if (elapsedTime > 1.4) stats.SetHit(false);
        }
       
    }

    

    IEnumerator MoveAround(Transform orbTransform, float angle)
    {
        while (orbTransform != null) 
        {
            Vector3 center = new Vector3(0, orbTransform.position.y, 0);
            orbTransform.RotateAround(center, Vector3.up, angle * bulletSpeed * Time.deltaTime) ;
            yield return null;
        }
    }

    private void PlayBossRoar()
    {
        audio.Play();
        StartCoroutine(BossRoarAgain());
    }


    IEnumerator BossRoarAgain()
    {
        yield return new WaitForSeconds(1f);
        audio.Play();
        yield return new WaitForSeconds(3f);
        bossRoar = true;

    }


}
