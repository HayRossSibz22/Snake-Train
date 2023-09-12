using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class StartScreenManager : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        // Display the high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + 0;//replace with high score once coded in
    }

}