using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBetweenRings : MonoBehaviour
{
    public float heightTeleportation = 2f;
    public GameObject otherRing;
    public bool isInnerRing;

    public AudioSource audio;
    public AudioClip interact;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKey(KeyCode.F))
        {
            audio.clip = interact;
            audio.Play();
            Transform posOtherRing = otherRing.GetComponent<Transform>();

            other.gameObject.transform.position = otherRing.transform.position + Vector3.up * heightTeleportation;
            
            if(isInnerRing) 
            {
                other.gameObject.GetComponent<PlayerMove>().radius = 12f;

            }
            else
            {
                other.gameObject.GetComponent<PlayerMove>().radius = 5f;

            }


        }
    }


}
