using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("Snake.1");
    }
}