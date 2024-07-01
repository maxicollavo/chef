using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFire : MonoBehaviour
{
    [SerializeField] List<GameObject> fire = new List<GameObject>();
    int enabledFire = 0;
    int i = 0;
    bool isCoroutineRunning = false;

    private void Update()
    {
        if (GameManager.Instance.onBoss && !isCoroutineRunning)
        {
            StartCoroutine(TurnFireCoroutine());
        }
    }

    IEnumerator TurnFireCoroutine()
    {
        isCoroutineRunning = true;
        while (IsEnemyAlive())
        {
            yield return new WaitForSeconds(3f);
            Debug.LogWarning("POR ARRANCAR EL FUEGO");
            //Poner sonido de ALERTA y capaz de prender hornallas
            //Algun SHAKE de camera
            yield return new WaitForSeconds(3f);
            //Poner sonido de FUEGO
            TurnOn();
            yield return new WaitForSeconds(5f);
            //Apagar todos los sonidos
            TurnOff();
        }
        isCoroutineRunning = false;
    }

    public bool IsEnemyAlive()
    {
        return GameManager.Instance.bossAlive;
    }

    void TurnOff()
    {
        foreach (var particles in fire)
        {
            particles.SetActive(false);
        }
    }

    void TurnOn()
    {
        i = 0;
        enabledFire = 0;
        int targetEnabledFire = GameManager.Instance.goodChef ? Random.Range(1, 3) : 3;

        while (enabledFire < targetEnabledFire)
        {
            if (i >= fire.Count)
            {
                i = 0;
            }

            var random = Random.Range(0, fire.Count);

            if (random == i && !fire[i].activeSelf)
            {
                fire[i].SetActive(true);
                enabledFire++;
            }

            i++;

            if (enabledFire >= targetEnabledFire)
            {
                break;
            }
        }
    }
}
