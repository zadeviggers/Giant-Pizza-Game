using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienceSpawner : MonoBehaviour
{
    public List<GameObject> thingsToSpawn;
    public float spawnInterval;
    public float spawnWidth;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstaclesCorutine());
    }

    // Update is called once per frame
    void Update()
    {
        float psawnOffsetDistance = spawnWidth / 2;
        Vector2 leftmostPoint = new Vector2(transform.position.x - psawnOffsetDistance, transform.position.y);
        Vector2 rightmostPoint = new Vector2(transform.position.x + psawnOffsetDistance, transform.position.y);
        Debug.DrawLine(leftmostPoint, rightmostPoint, Color.green, 1);
    }

    IEnumerator SpawnObstaclesCorutine()
    {
        int indexToSpawn = Random.Range(0, thingsToSpawn.Count);

        GameObject thingToSpawn = thingsToSpawn[indexToSpawn];

        float psawnOffsetDistance = spawnWidth / 2;
        float spawnX = Random.Range(transform.position.x - psawnOffsetDistance, transform.position.x + psawnOffsetDistance);
        Vector2 spawnPosition = new Vector2(spawnX, transform.position.y);

        Instantiate(thingToSpawn, spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(SpawnObstaclesCorutine());
    }
}
