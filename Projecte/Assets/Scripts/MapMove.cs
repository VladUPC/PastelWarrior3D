using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public float rotationMap = 1.0f;
    public PlayerMove player;
    public GameObject enemies;

    private Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if(!player.isColliding)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.Rotate(new Vector3(0, rotationMap * Time.deltaTime, 0));

                rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, rotationMap * Time.deltaTime, 0)));

                enemies.transform.Rotate(new Vector3(0, rotationMap * Time.deltaTime, 0));

            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, -rotationMap * Time.deltaTime, 0)));
                enemies.transform.Rotate(new Vector3(0, -rotationMap * Time.deltaTime, 0));


            }
        }
        

    }
}
