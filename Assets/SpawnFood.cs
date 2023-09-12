using System.Collections;
using System.Security.Permissions;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject apple;
    public float minX = 100; // minX is the minimum x coordinate where food can spawn
    public float maxX = 100; // maxX is the maximum x coordinate where food can spawn
    public float minY = 100; // minY is the minimum y coordinate where food can spawn
    public float maxY = 100; // maxY is the maximum y coordinate where food can spawn

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            SpawnFoodFunc();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnFoodFunc();
    }

    void SpawnFoodFunc()
    {
        float randomX = Random.Range(minX, maxX); // minX and maxX are the minimum and maximum x coordinates where food can spawn
        float randomY = Random.Range(minY, maxY); // minY and maxY are the minimum and maximum y coordinates where food can spawn

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);
        Instantiate(apple, randomPosition, Quaternion.identity);
    }
}
