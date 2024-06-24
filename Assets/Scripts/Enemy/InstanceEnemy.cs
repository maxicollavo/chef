using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InstanceEnemy : MonoBehaviour
{
    /*public GameObject enemy;
    public int PosX;
    public int PosZ;
    public int EnemeyCount;

    private void Start()
    {
        StartCoroutine(SpawnerEnemies());
    }
   

    private IEnumerator SpawnerEnemies()
    {
        while (EnemeyCount <20) 
        {
        PosX = UnityEngine.Random.Range(31, 35);
        PosZ = UnityEngine.Random.Range(30, 19);
        Instantiate(enemy , new Vector3(PosX, 3.11f, PosZ),quaternion.identity);
        yield return new WaitForSeconds(10f);
        EnemeyCount += 1;
        }
    }*/

    public GameObject enemy;
    public List<Vector3> posiciones;
    public int EnemeyCount;

    private void Start()
    {
        // Inicializar lista con pares de valores posibles para PosX y PosZ
        posiciones = new List<Vector3>
        {
            new Vector3(31, 3.11f, 20),
            new Vector3(32, 3.11f, 29),
            new Vector3(33, 3.11f, 28),
            new Vector3(34, 3.11f, 27),
            new Vector3(35, 3.11f, 26)
            // Agrega más pares de valores si es necesario
        };

        StartCoroutine(SpawnerEnemies());
    }

    private IEnumerator SpawnerEnemies()
    {
        while (EnemeyCount < 20)
        {
            int randomIndex = UnityEngine.Random.Range(0, posiciones.Count);
            Vector3 posicion = posiciones[randomIndex];

            Instantiate(enemy, posicion, quaternion.identity);

            yield return new WaitForSeconds(10f);
            EnemeyCount += 1;
        }
    }
}


