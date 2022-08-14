using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienceSpawner : MonoBehaviour
{
    public List<GameObject> thingsToSpawn;
    public float spawnInterval;
    public float spawnWidth;
    private float spawnOffsetDistance;


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

        float spawnX = Random.Range(transform.position.x - spawnOffsetDistance, transform.position.x + spawnOffsetDistance);
        Vector2 spawnPosition = new Vector2(spawnX, transform.position.y);

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
        Vector2 leftmostPoint = new Vector2(transform.position.x - spawnOffsetDistance, transform.position.y);
        Vector2 rightmostPoint = new Vector2(transform.position.x + spawnOffsetDistance, transform.position.y);
        Debug.DrawLine(leftmostPoint, rightmostPoint, Color.green, 1);

    }
}
