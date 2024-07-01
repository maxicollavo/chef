using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFire : MonoBehaviour
{
    [SerializeField] List<GameObject> fire = new List<GameObject>();
    [SerializeField] AudioSource alarm;
    [SerializeField] AudioSource fireSound;
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
            yield return new WaitForSeconds(5f);
            Debug.LogWarning("POR ARRANCAR EL FUEGO");
            alarm.Play();
            //Algun SHAKE de camera
            yield return new WaitForSeconds(3f);
            alarm.Stop();
            fireSound.Play();
            TurnOn();
            yield return new WaitForSeconds(5f);
            fireSound.Stop();
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
        PlayerBehaviour.Instance.onFire = false;
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
