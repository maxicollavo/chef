using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shaderTrigger : MonoBehaviour
{
    public Material newMaterial;  // Asigna el material que tiene el shader que deseas activar
    public Renderer targetRenderer;  // El renderer del objeto al que deseas cambiar el shader

    private void Start()
    {
        // Encuentra el renderer del objeto al que deseas aplicar el shader
        GameObject targetObject = GameObject.Find("PlaneFF"); // Reemplaza "TargetObjectName" con el nombre de tu objeto
        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cambia el material del objeto al entrar en el trigger
        if (targetRenderer != null && other.CompareTag("PlayerCollider"))  // Suponiendo que el player tiene el tag "Player"
        {
            targetRenderer.material = newMaterial;
        }
    }
}