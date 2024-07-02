using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishAnim : MonoBehaviour
{
    [SerializeField] EnemyAI ai;
    [SerializeField] BoxCollider coll;

    public void DestroyAnim()
    {
        if (ai.isBoss)
        {
            SceneManager.LoadScene("WinScene");
        }

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
