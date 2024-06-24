using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private WaitForSeconds spawnInterval = new WaitForSeconds(5f);
    private GameObject powerUp;

    private void Update()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (GameManager.Instance.readyForEnemies)
        {
            if (powerUp == null)
            {
                powerUp = Instantiate(objectToSpawn, transform.position, transform.rotation);
                yield return spawnInterval;
            }
            yield return spawnInterval;
        }
    }
}
