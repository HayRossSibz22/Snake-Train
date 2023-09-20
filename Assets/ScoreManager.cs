using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore = 0;
    public static int highScore = 0;

    public UnityEngine.UI.Text currentScoreText;
    public UnityEngine.UI.Text highScoreText;

    public UnityEngine.UI.Text gameOverText;
    public UnityEngine.UI.Text startGameText;

    public SnakeMovement snakeMovement;


void Start()
{
    GameObject player = GameObject.Find("Head");
    snakeMovement = player.GetComponent<SnakeMovement>();
    highScore = PlayerPrefs.GetInt("highScore", 0);
    startGameText.enabled = true;

    gameOverText.enabled = false;

}

// Update the UI Text components every frame
void Update()
{   
    if(snakeMovement.isGameStarted() == true)
    {
        startGameText.enabled = false;
    }
    if (snakeMovement.isDead() == true)
    {
        //make the game over button appear. same for text
        gameOverText.enabled = true;
        startGameText.enabled = true;
    }
    currentScoreText.text = "Current Score: " + currentScore;
    highScoreText.text = "High Score: " + highScore;

    if (currentScore > highScore)
    {
        highScore = currentScore;
        PlayerPrefs.SetInt("highScore", highScore);
    }
}

public void resetScore()
{
    currentScore = 0;
}

public void reset()
{
    gameOverText.enabled = false;
    resetScore();
}
}