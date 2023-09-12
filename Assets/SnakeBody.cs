using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public bool ignoreCollision = true;

    void Start()
    {
        Invoke("EnableCollision", 1.0f);
    }

    void EnableCollision()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }
        ignoreCollision = false;
    }
}