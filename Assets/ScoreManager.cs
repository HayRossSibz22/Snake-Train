using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore = 0; 
    public static int highScore = 0; 

    public UnityEngine.UI.Text currentScoreText; 
    public UnityEngine.UI.Text highScoreText; 

    // Update the UI Text components every frame
    void Update()
    {
        currentScoreText.text = "Current Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
    }
}