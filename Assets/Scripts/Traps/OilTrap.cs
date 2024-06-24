using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrap : MonoBehaviour
{
    [SerializeField]
    NewPlayerMovement playerMov;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
        {
            playerMov.isSliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
        {
            playerMov.isSliding = false;
        }
    }
}
