using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Permissions;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    public Transform movePoint;
    public float moveRate = 0.15f;
    public float delay = 1000f;
    public float moveDistance = .3f;
    public GameObject bodyPartPrefab;  // Assign this in the Unity Editor
    public Vector3 direction = new Vector3(1, 0, 0); // Start moving to the right
    public float rotation = 0f;
    private List<GameObject> inactiveParts = new List<GameObject>();
    public List<Transform> bodyParts = new List<Transform>();
    public bool alive = true;
    private void Start()
    {

        // Set the initial position of the movePoint to the snake's head
        movePoint.position += new Vector3(0, 0, 0);

        // Start the Move coroutine
        StartCoroutine("Move");
    }

    private void Update()
    {
        // Listen for player inputs to change direction
        ChangeDirection();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            //test to see if this trigger is working
            UnityEngine.Debug.Log("Food was eaten");

            StartCoroutine(AddNewBodyPart());

        }
        if (other.gameObject.CompareTag("Player"))
        {
            //test to see if this trigger is working
            UnityEngine.Debug.Log("Snake died");

            Destroy(gameObject);
            alive = false;
        }

        if(other.gameObject.CompareTag("Walls"))
        {
            UnityEngine.Debug.Log("Snake died");

            Destroy(gameObject);
            alive = false;
        }


    }

    public bool canSpawn = false;

    IEnumerator Move()
    {
        while (alive)
        {
            canSpawn = false;  // Clear the flag
            // Cache the previous position of the head for the first body part to use
            Vector3 prevPosition = transform.position;

            // Move the head to the movePoint position
            transform.position = movePoint.position;

            // Rotates the snake head to the direction it is moving
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Update the movePoint position based on the current direction and distance
            movePoint.position += direction * moveDistance;

            for (int i = 0; i < bodyParts.Count; i++)
            {
                Vector3 temp = bodyParts[i].position;
                bodyParts[i].position = prevPosition;
                prevPosition = temp;
            }

            foreach (var part in inactiveParts)
            {
                part.SetActive(true);

                // Re-enable the collider
                BoxCollider2D collider = part.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }

            inactiveParts.Clear();

            canSpawn = true;  // Set the flag

            yield return new WaitForSeconds(moveRate);
        }
    }

    public IEnumerator SpawnMultipleParts(int numberOfParts)
    {
        for (int i = 0; i < numberOfParts; i++)
        {
            yield return StartCoroutine(AddNewBodyPart()); // Wait for the AddNewBodyPart coroutine to complete


            // Rest of the logic for each iteration, if any
            // ...
        }
    }

    public IEnumerator AddNewBodyPart()
    {
        yield return new WaitUntil(() => canSpawn);

        Vector3 newPosition = (bodyParts.Count > 0) ? bodyParts[bodyParts.Count - 1].position : transform.position;

        GameObject newPartGameObject = Instantiate(bodyPartPrefab, newPosition, Quaternion.identity);

        // Set the sorting layer
        SpriteRenderer spriteRenderer = newPartGameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 1;
        }

        // Add and disable a Collider
        BoxCollider2D collider = newPartGameObject.AddComponent<BoxCollider2D>();
        collider.enabled = false;  // disable the collider

        Transform newPartTransform = newPartGameObject.transform;
        bodyParts.Add(newPartTransform);
        inactiveParts.Add(newPartGameObject);
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
}