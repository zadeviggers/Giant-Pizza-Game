using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienceSpawner : MonoBehaviour
{
    public List<GameObject> thingsToSpawn;
    public float spawnInterval;
    public float spawnWidth;
    private float spawnOffsetDistance;

    public bool spawnRandomly = true;
    public bool vertical = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnOffsetDistance = spawnWidth / 2;
        StartCoroutine(SpawnObstaclesCoroutine());
    }


    IEnumerator SpawnObstaclesCoroutine()
    {
        int indexToSpawn = Random.Range(0, thingsToSpawn.Count);

        GameObject thingToSpawn = thingsToSpawn[indexToSpawn];

        Vector2 spawnPosition = transform.position;

        if (spawnRandomly)
        {
            if (vertical)
            {
                float spawnY = Random.Range(transform.position.y - spawnOffsetDistance, transform.position.y + spawnOffsetDistance);
                spawnPosition = new Vector2(transform.position.x, spawnY);
            }
            else
            {
                float spawnX = Random.Range(transform.position.x - spawnOffsetDistance, transform.position.x + spawnOffsetDistance);
                spawnPosition = new Vector2(spawnX, transform.position.y);
            }
        }

        Instantiate(thingToSpawn, spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(SpawnObstaclesCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        DrawSpawnDebugLine();
    }


    void DrawSpawnDebugLine()
    {
        if (spawnRandomly)
        {
            if (vertical)
            {
                Vector2 topmostPoint = new Vector2(transform.position.x, transform.position.y - spawnOffsetDistance);
                Vector2 bottommostPoint = new Vector2(transform.position.x, transform.position.y + spawnOffsetDistance);
                Debug.DrawLine(topmostPoint, bottommostPoint, Color.green, 1);
            }
            else
            {
                Vector2 leftmostPoint = new Vector2(transform.position.x - spawnOffsetDistance, transform.position.y);
                Vector2 rightmostPoint = new Vector2(transform.position.x + spawnOffsetDistance, transform.position.y);
                Debug.DrawLine(leftmostPoint, rightmostPoint, Color.green, 1);
            }
        }
    }
}
