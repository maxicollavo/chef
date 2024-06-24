using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorFunc : MonoBehaviour
{
    public Transform[] endpoint;
    public int currentSpeed;

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.Instance.goForward)
        {
            other.transform.parent.transform.position = Vector3.MoveTowards(other.transform.position, endpoint[0].position, currentSpeed * Time.deltaTime);
        }
        else
        {
            other.transform.parent.transform.position = Vector3.MoveTowards(other.transform.position, endpoint[1].position, currentSpeed * Time.deltaTime);
        }
    }
}
