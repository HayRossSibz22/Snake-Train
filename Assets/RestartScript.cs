using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    
    public void RestartGame()
    {
        UnityEngine.Debug.Log("Button clicked");
        // Reloads the current scene, effectively restarting the game

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}