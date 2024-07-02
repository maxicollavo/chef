using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickUp : MonoBehaviour
{
    private int ingredientValue = 1;

    [SerializeField] AudioSource ingredientsReached;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BoxCollider coll;

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
                StartCoroutine(Sound());
            }
        }
    }

    private void Hide()
    {
        mesh.enabled = false;
        coll.enabled = false;
    }

    private IEnumerator Sound()
    {
        ingredientsReached.Play();
        Hide();
        yield return new WaitUntil(() => !ingredientsReached.isPlaying);
        Destroy(gameObject);
    }
}