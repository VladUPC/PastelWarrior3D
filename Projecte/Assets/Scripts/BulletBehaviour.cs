using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float timeLife = 200f;

    private void Awake()
    {
       Destroy(gameObject, timeLife);
    }



}
