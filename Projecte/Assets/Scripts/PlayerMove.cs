using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float radius = 5f;

    public float radiusCamara = 15f;
    public float velocity = 5f;
    private float angle;
    public bool isColliding = false;
    public GameObject camara;
    private float horizontal;
    public bool moving = true;
    public bool movingRight = true;

    //para dash
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    private bool isDashing = false;
    private PlayerStats playerStats;

    Animator playerAnimator;
    public GameObject blackKnight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angle = Mathf.Atan2(transform.position.z, transform.position.x);
        
        playerAnimator = blackKnight.GetComponent<Animator>();
        playerAnimator.SetBool("Running", false);

        playerStats = this.GetComponent<PlayerStats>();


    }

    private void Update()
    {

        if (angle > Mathf.PI*2) angle -= Mathf.PI*2;
        if (angle < 0) angle += Mathf.PI*2;

        horizontal = Input.GetAxis("Horizontal");

        //Varible para disparar
        if (horizontal > 0 ) movingRight = true;
        if (horizontal < 0 ) movingRight = false;
        
        if (horizontal != 0) {
            playerAnimator.SetBool("Running",true);
        }
        else {
            playerAnimator.SetBool("Running", false);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing && !isColliding)
        {
            StartCoroutine(Dash());
            StartCoroutine(Invulnerable());

        }

    }

    void FixedUpdate()
    {

        angle += velocity * Time.fixedDeltaTime * horizontal;


        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        float xCam =  radiusCamara* Mathf.Cos(angle);
        float zCam = radiusCamara * Mathf.Sin(angle);



        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        camara.transform.position = new Vector3(xCam, camara.transform.position.y, zCam);
        camara.transform.LookAt(new Vector3(newPosition.x, camara.transform.position.y, newPosition.z));

        transform.LookAt(newPosition);

        if(isColliding) 
        {
            angle -= velocity * Time.fixedDeltaTime * horizontal;

        }
        else
        {
            rb.MovePosition(newPosition);

        }



    }

    IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime && !isColliding)
        {
            angle += dashSpeed * Time.deltaTime * (movingRight ? 1 : -1);
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            Vector3 newPosition = new Vector3(x, transform.position.y, z);
            rb.MovePosition(newPosition);
            yield return null;
        }

        if (isColliding)
        {
            angle -= 8 * velocity * Time.fixedDeltaTime * (movingRight ? 1 : -1);
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            Vector3 newPosition = new Vector3(x, transform.position.y, z);
            rb.MovePosition(newPosition);

        }
        yield return null;



        isDashing = false;
    }

    IEnumerator Invulnerable()
    {
        bool isInGodMode = playerStats.IsInGodMode();
            yield return new WaitForSeconds(0.01f);
        if(!isInGodMode) playerStats.SetInvulnerable(true);
        yield return new WaitForSeconds(dashTime - 0.01f);
        if (!isInGodMode) playerStats.SetInvulnerable(false);



    }


    public float GetAngle() {
        return angle;
    }


}
