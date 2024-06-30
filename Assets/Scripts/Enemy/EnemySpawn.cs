using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    private WaitForSeconds spawnInterval = new WaitForSeconds(5f);
    private GameObject enemy;
    [SerializeField] private GameObject target;
    [SerializeField] List<Transform> waypoints = new List<Transform>();

    private void Update()
    {
        if (!GameManager.Instance.readyForEnemies)
        {
            StopCoroutine(SpawnCoroutine());
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.readyForEnemies = true;
        StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        GameManager.Instance.readyForEnemies = false;
        StopCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (GameManager.Instance.readyForEnemies)
        {
            if (enemy == null)
            {
                enemy = Instantiate(objectToSpawn, transform.position, transform.rotation);
                GameManager.Instance.enemies.Add(enemy);
                var eai = enemy.GetComponent<EnemyAI>();
                eai.target = target;
                eai.Waypoints = waypoints;

                yield return spawnInterval;
            }
            yield return spawnInterval;
        }
    }
}