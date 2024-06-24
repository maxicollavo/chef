using System;
using UnityEngine;

public class EnemyToDemoon : MonoBehaviour
{
    // float timer = 0f;
    // float timerToSendEnemy = 1f;

    // private EnemyRecipe _enemyRecipe;

    // private void Awake()
    // {
    //     _enemyRecipe = gameObject.GetComponent<EnemyRecipe>();
    // }

    // public void Interact(Bar bar)
    // {
    //     timer += Time.deltaTime;
    //     if (timer >= timerToSendEnemy)
    //     {
    //         if (_enemyRecipe.pactAchieved)
    //             EventManager.Instance.Dispatch(GameEventTypes.OnPact, this, EventArgs.Empty);
    //         else if (!_enemyRecipe.pactAchieved && GameManager.Instance.firstPact)
    //             EventManager.Instance.Dispatch(GameEventTypes.OnPactFailed, this, EventArgs.Empty);
                
    //         gameObject.SetActive(false);
    //     }
    // }
}