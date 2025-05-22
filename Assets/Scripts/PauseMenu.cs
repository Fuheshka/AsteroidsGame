using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    float previousTimeScale = 1f;
    //public TMPro.TMP_Text pauseLabel;
    public GameObject PauseLabel;
    public static bool isPaused;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            //pauseLabel.enabled = true;
            PauseLabel.SetActive(true);
            //gameObject.SetActive(true);
            isPaused = true;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            //pauseLabel.enabled = false;
            PauseLabel.SetActive(false);
            //gameObject.SetActive(false);
            isPaused = false;
        }
    }

    public void ContinueButton()
    {
        PauseLabel.SetActive(false);
        Time.timeScale = previousTimeScale;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}