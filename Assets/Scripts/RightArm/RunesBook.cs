using System;
using System.Collections;
using UnityEngine;
public class RunesBook : MonoBehaviour
{
    // [SerializeField] Camera mainCamera;
    
    // public static RunesBook Instance { get; private set; }

    // void Awake()
    // {
    //     Instance = this;
    // }

    // public void SendEnemyToDemon()
    // {
    //     Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

    //     if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("Enemy"))
    //     {
    //         hit.transform.parent.gameObject.SetActive(false);
    //         EventManager.Instance.Dispatch(GameEventTypes.OnPact, this, EventArgs.Empty);
    //     }
    // }
}