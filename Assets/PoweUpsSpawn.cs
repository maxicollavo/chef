using System.Collections;
using UnityEngine;

public class PoweUpsSpawn : MonoBehaviour
{
    public Transform[] waypointsPowerUps;

    public GameObject PowerUps;

    public float spawnInterval = 2.0f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            Transform chosenWaypoint = waypointsPowerUps[Random.Range(0, waypointsPowerUps.Length)];
            Instantiate(PowerUps, chosenWaypoint.position, chosenWaypoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}