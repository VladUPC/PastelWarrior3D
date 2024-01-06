using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ReturnToGame());
    }

    IEnumerator ReturnToGame() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game");
    }

}
