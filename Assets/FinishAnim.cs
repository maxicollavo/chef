using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnim : MonoBehaviour
{
    public void DestroyAnim()
    {
        Destroy(transform.parent.gameObject);
    }
}
