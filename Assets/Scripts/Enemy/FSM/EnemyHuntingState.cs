using UnityEngine;
using UnityEngine.AI;

public class EnemyHuntingState : EnemyBaseState
{
    bool playerInSightRange;
    bool playerInAttackRange;
    float sightRange = 8f;
    float attackRange = 2f;
    private LayerMask whatIsPlayer;
    public override void EnterState(EnemyStateManager enemy)
    {
        //Logica cuando ve al jugador
        Debug.Log("He visto un lindo gatito, voy a agarrarlo");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Bullet") || other.CompareTag("Knife"))
        {
            enemy.SwitchState(enemy.HurtedState);
        }
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        playerInSightRange = Physics.CheckSphere(enemy.transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(enemy.transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && playerInAttackRange)
        {
            enemy.SwitchState(enemy.AttackingState);
        }

        if (!playerInSightRange && !playerInAttackRange)
        {
            enemy.SwitchState(enemy.PattrolingState);
        }
    }
}
