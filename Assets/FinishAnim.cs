using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnim : MonoBehaviour
{
    [SerializeField] EnemyAI ai;
    [SerializeField] BoxCollider coll;

    public void DestroyAnim()
    {
        Destroy(transform.parent.gameObject);
    }

    public void Attack()
    {
        ai.Shoot();
    }

    public void BoxColl()
    {
        coll.enabled = false;
    }
}
