using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.15f;
    private float ogSpeed;
    public float radius = 12f;
    private float angle;
    private Rigidbody rb;
    private float elapsedTime;
    private bool attack;
    private bool sense;
    public float senseRadius = 1.2f;
    public float jumpForce = 0.05f;
    public GameObject crabMonster;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
        elapsedTime = 0;
        sense = false;
        ogSpeed = speed;
    }

    private void Update()
    {
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        
        elapsedTime += Time.deltaTime;

        bool prevSense = sense;
        sense = Vector3.Distance(player.transform.position, transform.position) < senseRadius;

        speed = ogSpeed;

        if (!attack) {
            if (sense) {
                speed = 0;
                elapsedTime = 0;
                crabMonster.GetComponent<Animator>().SetBool("Attack", true);
                attack = true;
                speed = ogSpeed;
                Vector3 aux = transform.position;
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));  
             }
            else {
                crabMonster.GetComponent<Animator>().SetBool("Attack", false);
                Vector3 newPosition = new Vector3(x, transform.position.y, z);
                transform.LookAt(newPosition);
                rb.MovePosition(newPosition);
            }

        

       

            if (elapsedTime < 4) angle += speed * Time.deltaTime;
            else {
                speed = -speed;
                ogSpeed = -ogSpeed;
                angle += 2*speed * Time.deltaTime;
                elapsedTime = 0;
            }
        }
        else {
            if (elapsedTime > 2.5) attack = false;   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            speed = -speed;
            ogSpeed = -ogSpeed;
            angle += 2*speed * Time.deltaTime;
            elapsedTime = 0;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            
            angle += 2*speed * Time.deltaTime;
            elapsedTime = 0;
        }

    }
}
