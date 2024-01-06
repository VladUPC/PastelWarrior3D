using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{

    public GameObject blackNight;
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = blackNight.GetComponent<Animator>();
        playerAnimator.SetBool("Running", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left") || Input.GetKey("right")) {
            playerAnimator.SetBool("Running", true);
        }
        else {
            playerAnimator.SetBool("Running", false);
        }
        if (Input.GetKey(KeyCode.C)) {
            playerAnimator.SetBool("Attack", true);
        }
        else {
            playerAnimator.SetBool("Attack", false);
        }
    }
}
