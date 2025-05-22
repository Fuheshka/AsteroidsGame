using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem Explosion;
    public int Lives = 3;
    public float RespawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int Score = 0;
    int HighScore = 0;

    private const string YandexLeaderBoardGame = "Score";

    


    public TMPro.TMP_Text HPText;
    public TMPro.TMP_Text ScoreText;

    public GameOverScreen GameOverScreen;

    public PauseMenu PauseMenu;
    float previousTimeScale = 1f;

    void Start()
    {

        HPText.text = Lives.ToString(); // Передаем значение жизней в интерфейс
        ScoreText.text = Score.ToString(); //Очки в интерфейс
    }

    
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.Explosion.transform.position = asteroid.transform.position;
        this.Explosion.Play();

        if (asteroid.Size < 0.75f)
        {
            Score += 100;
            ScoreText.text = Score.ToString();
        }
        else if (asteroid.Size < 1.2f)
        {
            Score += 50;
            ScoreText.text = Score.ToString();
        }
        else
        {
            Score += 25;
            ScoreText.text = Score.ToString();
        }
        HighScore = Score;
    }

    public void UFODestroyed(UFO ufo)
    {
        this.Explosion.transform.position = ufo.transform.position;
        this.Explosion.Play();

        if (ufo.Size < 1.2f)
        {
            Score += 100;
            ScoreText.text = Score.ToString();
        }
        else if (ufo.Size < 1.35f)
        {
            Score += 50;
            ScoreText.text = Score.ToString();
        }
        else
        {
            Score += 25;
            ScoreText.text = Score.ToString();
        }
        HighScore = Score;
    }
    public void PlayerDied()
    {
        this.Explosion.transform.position = this.player.transform.position;
        this.Explosion.Play();
        
        this.Lives--;
        HPText.text = Lives.ToString();

        if (this.Lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.RespawnTime);
        }
        
    }

    
    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("NoCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Pause()
    {
        PauseMenu.TogglePause();
    }
    private void GameOver()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            GameOverScreen.Setup(Score);

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;

            this.Lives = 3;
            this.Score = 0;
            Invoke(nameof(Respawn), this.RespawnTime);

            Time.timeScale = 1f;

        }
        //SceneManager.LoadScene(2);



    }

    private void TrySaveHighScore()
    {
        if (Score <= HighScore)
            return;

            HighScore = Score;

            YandexGame.NewLeaderboardScores(YandexLeaderBoardGame, HighScore);
        }
    }
