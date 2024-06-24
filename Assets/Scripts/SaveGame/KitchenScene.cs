using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenScene : MonoBehaviour
{
    private static Vector3 lastPlayerPos = Vector3.zero;

    void Start()
    {
        if (lastPlayerPos != Vector3.zero)
        {
            gameObject.transform.position = lastPlayerPos;
        }
    }

    private void OnDestroy()
    {
        lastPlayerPos = gameObject.transform.position;
    }
}
