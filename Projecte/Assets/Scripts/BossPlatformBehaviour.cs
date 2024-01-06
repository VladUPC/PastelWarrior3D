using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformBehaviour : MonoBehaviour
{
    public GameObject boss;

    public AudioSource audio;
    public AudioClip bossTheme;
    public bool bossThemeIsPlaying = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            boss.SetActive(true);
            if(!bossThemeIsPlaying) PlayBossTheme();
        }
        
    }

    private void PlayBossTheme()
    {
        bossThemeIsPlaying=true;
        audio.clip = bossTheme; 
        audio.Play();
    }
}
