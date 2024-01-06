using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBetweenLevels : MonoBehaviour
{
    public GameObject camara;

    public AudioSource audio;
    public AudioClip interact;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.F))
        {
            audio.clip = interact;
            audio.Play();
            other.gameObject.transform.position += Vector3.up * 11;
            camara.transform.position += Vector3.up * 10;

        }

    }
}
