using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce = 1f;
    private bool onGround = false;
    private bool jumpRequest = false;
    private bool doubleJump = false;

    public AudioSource audio;
    public AudioClip jumpUp, land;

    public GameObject camara;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*
        // Mirar a izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }


        // Mirar a derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }*/
        
        if ((onGround || doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;
        }
        
    }

    //Salta
    void FixedUpdate()
    {


        if (jumpRequest)
        {
            if (onGround)
            {
                onGround = false;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                doubleJump = false;
            }
            audio.clip = jumpUp;
            audio.Play();
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpRequest = false;
        }

        float xCam = camara.transform.position.x;
        float zCam = camara.transform.position.z;



        camara.transform.position = new Vector3(xCam, transform.position.y+3, zCam);


    }

 
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacle")
        {
            audio.clip = land;
            audio.Play();
            onGround = true;
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Obstacle")
        {
            onGround = false;
        }
    }
    */

}
