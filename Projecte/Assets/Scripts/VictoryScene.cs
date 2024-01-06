using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }

}
