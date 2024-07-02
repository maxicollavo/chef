using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAnim : MonoBehaviour
{
    [SerializeField] Animator cinta;

    private void OnEnable()
    {
        cinta.SetTrigger("OnFunc");
    }
}
