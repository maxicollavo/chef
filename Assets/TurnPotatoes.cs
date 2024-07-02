using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPotatoes : MonoBehaviour
{
    [SerializeField] GameObject papas;
    [SerializeField] AudioSource mecanism;

    public void TurnOn()
    {
        papas.SetActive(true);
    }

    public void PlaySound()
    {
        mecanism.Play();
    }
}
