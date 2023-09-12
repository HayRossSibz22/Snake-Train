using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;  // Reference to your apple Prefab
    public Transform backgroundTransform;  // Reference to your background Transform
    public float gridStep = 0.3f;  // The step between grid points, should be equal to the snake's moveDistance

    private void Start()
    {
        SpawnApple();
    }

    public void SpawnApple()
    {
        // Get bounds of the background
        Vector2 backgroundSize = backgroundTransform.GetComponent<SpriteRenderer>().bounds.size;
        UnityEngine.Debug.Log("background size:" + backgroundSize);
        // Calculate grid dimensions based on background size and grid step
        int rows = Mathf.FloorToInt(backgroundSize.y / gridStep);
        int cols = Mathf.FloorToInt(backgroundSize.x / gridStep);

        // Generate random row and column indices
        int row = UnityEngine.Random.Range(0, rows);
        int col = UnityEngine.Random.Range(0, cols);

        // Calculate the spawn position
        Vector2 spawnPos = new Vector2(
            backgroundTransform.position.x - backgroundSize.x / 2 + (col + 0.5f) * gridStep,
            backgroundTransform.position.y - backgroundSize.y / 2 + (row + 0.5f) * gridStep
        );

        // Instantiate the apple
        Instantiate(applePrefab, spawnPos, Quaternion.identity);
    }
}