using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMPro.TMP_Text ScoreText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        ScoreText.text = score.ToString() + " Î×ÊÎÂ";
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Asteroids");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
