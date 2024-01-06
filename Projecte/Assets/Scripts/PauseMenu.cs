using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject HUD;
    public bool isPaused;
    public bool musicOn;
    public GameObject backgroundMusic;

    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        musicOn = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused) 
            {
                Resume();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        HUD.SetActive(false);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        HUD.SetActive(true);

    }

    public void Menu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Music()
    {
        if(musicOn)
        {
            backgroundMusic.gameObject.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            backgroundMusic.gameObject.GetComponent<AudioSource>().mute = false;
        }

        musicOn = !musicOn;
    }
}
