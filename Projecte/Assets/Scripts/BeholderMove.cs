using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderMove : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.15f;
    private float ogSpeed;
    public float radius = 12f;
    public float bulletSpeed;
    public Transform createOrbPoint;
    public GameObject orbPrefab;
    private float angle;
    private Rigidbody rb;
    private float elapsedTime;
    private bool attack;
    private bool sense;
    public bool movingRight;
    public float senseRadius = 7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
        elapsedTime = 0;
        sense = false;
        movingRight = true;
        ogSpeed = speed;
    }

    private void Update()
    {
        if (angle > Mathf.PI*2) angle -= Mathf.PI*2;
        if (angle < 0) angle += Mathf.PI*2;

        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        elapsedTime += Time.deltaTime;

        bool prevSense = sense;
        sense = Vector3.Distance(player.transform.position, transform.position) < senseRadius;
        if (sense) transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        speed = ogSpeed;

        if (!attack) {
            if (sense) {
                speed = 0;
                if (elapsedTime > 0.2) {
                    elapsedTime = 0;
                    attack = true;
                    GetComponent<Animator>().SetBool("Attack", true);
                    speed = ogSpeed;
                    float newY = 3f;
                    if (player.transform.position.y > newY) newY = player.transform.position.y;
                    Transform actual = transform;
                    float yRotation = transform.rotation.y;
                    transform.LookAt(new Vector3(player.transform.position.x, newY, player.transform.position.z));
                    if (yRotation*transform.rotation.y < 0) movingRight = !movingRight;
                }
            }
            else {
                Vector3 newPosition = new Vector3(x, transform.position.y, z);
                transform.LookAt(new Vector3(x, transform.position.y, z));
                rb.MovePosition(newPosition);
                if (elapsedTime < 4) angle += speed * Time.deltaTime;
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
            if (elapsedTime > 1.8) {
                GetComponent<Animator>().SetBool("Attack", false);
                attack = false;
                elapsedTime = 0;
                Transform objective = transform;
                objective.LookAt(player.transform.position);
                var orb = Instantiate(orbPrefab, objective.position, objective.rotation);

                Transform newTransform = orb.GetComponent<Transform>();
                float shootAngle = Mathf.PI/8;

                if (angle < player.GetComponent<PlayerMove>().GetAngle()) shootAngle *= -1;
                StartCoroutine(MoveAround(newTransform, shootAngle));
            }
        }

    }


    IEnumerator MoveAround(Transform orbTransform, float shootAngle)
    {
        while (orbTransform != null) 
        {
            Vector3 center = new Vector3(0, orbTransform.position.y, 0);
            orbTransform.RotateAround(center, Vector3.up, shootAngle * bulletSpeed * Time.deltaTime) ;
            yield return null;
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
