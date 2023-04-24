using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;

    private float spawnRate = 1.0f;
    private int score;
    public int lives = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public Slider VolumeSlider;
    public GameObject pauseMenu;

    public bool isGameActive;
    public bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive && lives>0)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);


        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToTake)
    {
        lives += livesToTake; 
        livesText.text = "Lives: " + lives;
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficultly)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        score = 0;
        lives = 3;
        spawnRate /= difficultly;

        titleScreen.gameObject.SetActive(false);
        VolumeSlider.gameObject.SetActive(false);
    }
    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !gamePaused;
        }
        if(gamePaused == true)
        {
            Time.timeScale = 0.0f;
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseMenu.gameObject.SetActive(false);
        }
        
    }
}
