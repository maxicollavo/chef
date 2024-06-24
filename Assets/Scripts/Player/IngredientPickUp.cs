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
                GameManager.Instance.ingredientCount += ingredientValue;

            if (GameManager.Instance.ingredientCount == 10)
                ingredientsReached.Play();

            Destroy(gameObject);
        }
    }
}