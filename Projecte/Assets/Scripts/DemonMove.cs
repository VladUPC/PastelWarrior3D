using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMove : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.15f;
    public float attackRange = 1f;
    private float ogSpeed;
    public float recallSpeed;
    public float chaseSpeed;
    public float radius = 12f;
    private float angle;
    private float ogY;
    //private Rigidbody rb;
    private float elapsedTime;
    public bool attack;
    public bool sense;
    private bool movingRight;
    public float senseRadius = 7f;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
        elapsedTime = 0;
        sense = false;
        movingRight = true;
        ogSpeed = speed;
        ogY = transform.position.y;
    }

    private void Update()
    {
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        float y = transform.position.y;
        if (y > ogY) y = ogY;

        elapsedTime += Time.deltaTime;

        bool prevSense = sense;
        float distance = Vector3.Distance(player.transform.position, transform.position); 
        sense = distance < senseRadius;
        if (sense) transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        speed = ogSpeed;

        if (!attack) {
            if (distance < attackRange) {
                attack = true;
                elapsedTime = 0;
                GetComponent<Animator>().SetBool("Attack", true);
            }
            else if (sense) {
                GetComponent<Animator>().SetBool("Chase", true);
                if (y < player.transform.position.y) y += chaseSpeed*Time.deltaTime;
                else if (y > player.transform.position.y) y -= chaseSpeed*Time.deltaTime;
                float posAngle = angle + 2*speed*Time.deltaTime;
                float negAngle = angle - 2*speed*Time.deltaTime;
                Vector3 posPosition = new Vector3(radius*Mathf.Cos(posAngle), y, radius * Mathf.Sin(posAngle));
                Vector3 negPosition = new Vector3(radius*Mathf.Cos(negAngle), y, radius * Mathf.Sin(negAngle));
                float posDis = Vector3.Distance(player.transform.position, posPosition);
                float negDis = Vector3.Distance(player.transform.position, negPosition);
                if (posDis < negDis) {
                    //rb.MovePosition(posPosition);
                    transform.position = posPosition;
                    angle = posAngle;
                }
                else {
                    //rb.MovePosition(negPosition);
                    transform.position = negPosition;
                    angle = negAngle;
                }
            }
            else {
                GetComponent<Animator>().SetBool("Chase", false);
                if (y < ogY) y += recallSpeed*Time.deltaTime;
                Vector3 newPosition = new Vector3(x, y, z);
                transform.LookAt(new Vector3(x, transform.position.y, z));
                //rb.MovePosition(newPosition);
                transform.position = newPosition;
                if (elapsedTime < 2.5) angle += speed * Time.deltaTime;
                else {
                    speed = -speed;
                    ogSpeed = -ogSpeed;
                    movingRight = !movingRight;
                    angle += 2*speed * Time.deltaTime;
                    elapsedTime = 0;
                }
            }
        }
        else {
            if (elapsedTime > 1f) GetComponent<Animator>().SetBool("Attack", false);
            if (elapsedTime > 1.8f) {
                attack = false;
                elapsedTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            speed = -speed;
            ogSpeed = -ogSpeed;
            movingRight = !movingRight;
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
