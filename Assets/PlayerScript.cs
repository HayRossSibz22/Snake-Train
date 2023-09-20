using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


using System.Runtime.InteropServices;

public class SnakeMovement : MonoBehaviour
{
    public Transform movePoint;
    public float moveRate = 0.10f;
    public float moveDistance = .5f;
    public int startLength = 3;

    public GameObject bodyPartPrefab;  // Assign this in the Unity Editor
    public GameObject bodyPartPrefab2;

    public GameObject bodyPartChosen;

    public ScoreManager ScoreManagerScript;

    public ScreenFlash screenFlash;

    public Vector3 direction = new Vector3(1, 0, 0); // Start moving to the right
    public List<Transform> bodyParts = new List<Transform>();
    public bool alive = true;

    public bool gameStarted = false;


    public bool isDead()
    {
        return !(alive);
    }
    private List<GameObject> inactiveParts = new List<GameObject>();
    public int nonCollidingPartsCount = 3;

    private void Start()
    {
        GameObject ScoreManager = GameObject.Find("ScoreManager");
        ScoreManagerScript = ScoreManager.GetComponent<ScoreManager>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !gameStarted)
        {

            StartGame();
        }
        ChangeDirection();

        if (isGameStarted() == true && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key was pressed
        {
            Application.Quit(); // Exit the game
        }
    }

    void StartGame()
    {
        float x = -0.24f;
        float y = 0.24f;
        // Reset everything to the initial state
        nonCollidingPartsCount = 3;
        StartCoroutine(SpawnMultipleParts(3));
        movePoint.position = new Vector3(x, y, 0);
        gameStarted = true;
        alive = true;
        direction = new Vector3(1, 0, 0); // Reset the initial direction
        nonCollidingPartsCount = 3;
        ScoreManagerScript.reset();
        StopCoroutine("Move"); // Stop existing coroutine
        StartCoroutine("Move"); // Start new coroutine
    }

    void RestartGame()
    {
        // Resetting the state of the game
        StopCoroutine("Move"); // Stop existing coroutine

        // Destroy all body parts
        foreach (Transform part in bodyParts)
        {
            Destroy(part.gameObject);
        }
        bodyParts.Clear();

        // Reset Score
        ScoreManagerScript.reset();

        alive = true;

        gameStarted = true;

        // Restart the Game
        StartGame();
    }

    void EndGame()
    {
        StopCoroutine("Move"); // Stop existing coroutine
        alive = false;
        screenFlash.FlashRed();
    }
    public bool isGameStarted()
    {
        return (gameStarted);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            ScoreManager.currentScore += 10;
            StartCoroutine(AddNewBodyPart());
            screenFlash.FlashGreen();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            SnakeBody snakeBody = other.gameObject.GetComponent<SnakeBody>();
            if (snakeBody == null || !snakeBody.ignoreCollision)
            {
                EndGame();
                alive = false;
            }
        }
        else if (other.gameObject.CompareTag("Walls"))
        {
            alive = false;
            EndGame();
        }
    }

    public bool canSpawn = false;

    IEnumerator Move()
    {
        while (alive)
        {
            canSpawn = false;
            Vector3 prevPosition = transform.position;
            transform.position = movePoint.position;
            movePoint.position += direction * moveDistance;


            // Rotates the snake head to the direction it is moving
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);


            // Handle the movement of body parts
            for (int i = 0; i < bodyParts.Count; i++)
            {
                Vector3 temp = bodyParts[i].position;
                bodyParts[i].position = prevPosition;
                prevPosition = temp;
            }

            canSpawn = true;
            yield return new WaitForSeconds(moveRate);
        }
    }

    public IEnumerator SpawnMultipleParts(int numberOfParts)
    {
        for (int i = 0; i < numberOfParts; i++)
        {
            yield return StartCoroutine(AddNewBodyPart());
        }
    }

    public IEnumerator AddNewBodyPart()
    {
        yield return new WaitUntil(() => canSpawn);
        Vector3 newPosition = (bodyParts.Count > 0) ? bodyParts[bodyParts.Count - 1].position : transform.position;
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            bodyPartChosen = bodyPartPrefab;
        }
        else
        {
            bodyPartChosen = bodyPartPrefab2;
        }
        GameObject newPart = Instantiate(bodyPartChosen, newPosition, Quaternion.identity);


        newPart.tag = "Player";

        // Add the BoxCollider2D component and set it initially as disabled
        BoxCollider2D newCollider = newPart.AddComponent<BoxCollider2D>();
        newCollider.enabled = false;
        newCollider.size = new Vector2(0.4f, 0.4f);

        // Add the SnakeBody script
        newPart.AddComponent<SnakeBody>();

        Transform newPartTransform = newPart.transform;
        bodyParts.Add(newPartTransform);
        inactiveParts.Add(newPart);

        if (nonCollidingPartsCount > 0)
        {
            nonCollidingPartsCount--;
        }
    }

    void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = new Vector3(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = new Vector3(1, 0, 0);
        }
    }

    public void SendHighScore(int highScore)
    {
        StartCoroutine(SendHighScoreCoroutine(highScore));
    }

    private IEnumerator SendHighScoreCoroutine(int highScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("highScore", highScore.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("http://yourwebsite.com/save_score.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error sending score: " + www.error);
            }
            else
            {
                Debug.Log("Successfully sent high score");
            }
        }
    }

}
