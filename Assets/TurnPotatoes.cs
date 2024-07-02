using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPotatoes : MonoBehaviour
{
    [SerializeField] GameObject papas;

    public void TurnOn()
    {
        papas.SetActive(true);
    }
}
