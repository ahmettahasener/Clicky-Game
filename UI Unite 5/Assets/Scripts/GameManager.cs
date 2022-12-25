using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameManager> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Image background;
    public Button restartButton;
    public Target target;
    public GameObject titleScreen;

    private int score;
    public float spawnRate = 1.0f;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Sensor").GetComponent<Target>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.live == 0)
        {
            GameOver();
        }
        if (target.live > 0)
        {
            livesText.text = "Lives: " + target.live;
        }
        else
        {
            livesText.text = "Dead";
        }
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void ScoreUpdate(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        ScoreUpdate(0);
        titleScreen.SetActive(false);
    }
}
