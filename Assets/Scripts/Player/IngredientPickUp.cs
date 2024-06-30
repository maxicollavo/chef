using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickUp : MonoBehaviour
{
    private int ingredientValue = 1;

    [SerializeField] AudioSource ingredientsReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            if (GameManager.Instance.ingredientCount < 10)
            {
                GameManager.Instance.ingredientCount += ingredientValue;
                Destroy(gameObject);
            }

            else if (GameManager.Instance.ingredientCount == 10)
            {
                Hide();
                StartCoroutine(Sound());
            }
        }
    }

    private void Hide()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
    }

    private IEnumerator Sound()
    {
        ingredientsReached.Play();
        yield return new WaitUntil(() => !ingredientsReached.isPlaying);
        Destroy(gameObject);
    }
}