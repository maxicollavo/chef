using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemies : MonoBehaviour
{
    [SerializeField] List<GameObject> enemySpawner = new List<GameObject>();
    [SerializeField] List<GameObject> texts = new List<GameObject>();


    [SerializeField] GameObject ingredientCanva;
    [SerializeField] GameObject subtitleCanva;
    [SerializeField] GameObject limitGoBack;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject lever;

    [SerializeField] GameObject findLever;
    [SerializeField] GameObject fPick;
    [SerializeField] GameObject factivate;

    [SerializeField] GameObject readyText;
    [SerializeField] GameObject notReadyText;
    [SerializeField] AudioSource enemiesDone;

    [SerializeField] AudioSource grabSource;
    [SerializeField] AudioSource grabLeverSource;
    [SerializeField] AudioSource putLever;
    private bool isDone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartEnemies"))
        {
            ingredientCanva.SetActive(true);
            GameManager.Instance.readyToInstantiate = true;

            for (int i = 0; i < enemySpawner.Count; i++)
            {
                enemySpawner[i].SetActive(true);
            }
        }

        if (other.CompareTag("Ingredient"))
        {
            grabSource.Play();
        }

        if (other.CompareTag("StartBoss"))
        {
            GameManager.Instance.onBoss = true;
            limitGoBack.SetActive(true);
            boss.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TradeStation"))
        {
            subtitleCanva.SetActive(true);
            foreach (var item in texts)
            {
                item.SetActive(false);
            }

            if (GameManager.Instance.ingredientCount >= 10)
            {
                if (isDone)
                {
                    ingredientCanva.SetActive(false);
                    //subtitleCanva.SetActive(false);
                    return;
                }

                notReadyText.SetActive(false);
                readyText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F) && !isDone)
                {
                    enemiesDone.Play();
                    subtitleCanva.SetActive(true);
                    GameManager.Instance.readyToInstantiate = false;
                    GameManager.Instance.ingredientReady = true;
                    isDone = true;
                    readyText.SetActive(false);
                }
            }
            else
            {
                notReadyText.SetActive(true);
            }
        }

        if (other.CompareTag("Lever"))
        {
            fPick.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                fPick.SetActive(false);
                Debug.Log("Colisiona con palanca");
                GameManager.Instance.hasLever = true;
                grabLeverSource.Play();
                //Activar carteles y feedback
                Destroy(other.transform.parent.gameObject);
            }
        }

        if (other.CompareTag("PutLever"))
        {
            if (GameManager.Instance.leverDone)
            {
                return;
            }

            if (!GameManager.Instance.hasLever)
            {
                //Activar carteles y feedback de que me falta una palanca
                findLever.SetActive(true);
                factivate.SetActive(false);
            }
            else
            {
                factivate.SetActive(true);
                findLever.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("No tienes una palanca");

                if (GameManager.Instance.hasLever)
                {
                    factivate.SetActive(false);
                    findLever.SetActive(false);
                    Debug.Log("Pongo palanca");
                    GameManager.Instance.leverDone = true;
                    putLever.Play();
                    lever.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TradeStation"))
        {
            //subtitleCanva.SetActive(false);
            notReadyText.SetActive(false);
            readyText.SetActive(false);
        }

        if (other.CompareTag("Lever"))
        {
            fPick.SetActive(false);
        }

        if (other.CompareTag("PutLever"))
        {
            factivate.SetActive(false);
            findLever.SetActive(false);
        }

    }
}
