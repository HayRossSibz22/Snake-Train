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
    public UnityEngine.UI.Button restartButton;

    public UnityEngine.UI.Text restartText;
    //import script from Head: PlayerMovement.cs
    public SnakeMovement snakeMovement;


void Start()
{
    GameObject player = GameObject.Find("Head");
    snakeMovement = player.GetComponent<SnakeMovement>();
    highScore = PlayerPrefs.GetInt("highScore", 0);



    //make the game over button and text disappear
    gameOverText.enabled = false;
    restartButton.enabled = false;
    restartText.enabled = false;
}

// Update the UI Text components every frame
void Update()
{
    if (snakeMovement.isDead() == true)
    {
        currentScore = 0;
        //make the game over button appear. same for text
        gameOverText.enabled = true;
        restartButton.enabled = true;
        restartText.enabled = true;
    }
    currentScoreText.text = "Current Score: " + currentScore;
    highScoreText.text = "High Score: " + highScore;

    if (currentScore > highScore)
    {
        highScore = currentScore;
        PlayerPrefs.SetInt("highScore", highScore);
    }
}
}