using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private GameObject shieldColl;
    [SerializeField] private GameObject playerColl;
    [SerializeField] private GameObject fryingPanObj;
    private BoxCollider fryingPanColl;

    void Start()
    {
        fryingPanColl = fryingPanObj.GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (fryingPanColl.enabled)
        {
            playerColl.SetActive(false);
            shieldColl.SetActive(true);
        }
        else if (!fryingPanColl.enabled)
        {
            shieldColl.SetActive(false);
            playerColl.SetActive(true);
        }
    }
}
