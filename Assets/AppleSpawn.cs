using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private AppleSpawner appleSpawner;
    Boolean apple = false;
    private void Start()
    {
        // Get a reference to the AppleSpawner script
        appleSpawner = FindObjectOfType<AppleSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy and respawn apple if it collides with the Player or Walls
        if (collision.CompareTag("Player") || collision.CompareTag("Walls"))
        {
            Destroy(gameObject);  // Destroy this apple
            apple = false;

            if (appleSpawner != null||apple == false)
            {
                appleSpawner.SpawnApple();  // Spawn a new apple
                apple = true;
            }
        }
    }
}