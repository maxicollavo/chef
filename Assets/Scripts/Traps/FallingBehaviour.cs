using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBehaviour : MonoBehaviour
{
    [SerializeField] AudioSource fruitSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            fruitSource.Play();
        }
    }
}
