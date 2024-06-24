using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    bool playerInSightRange;
    bool playerInAttackRange;
    float sightRange = 8f;
    float attackRange = 2f;
    private LayerMask whatIsPlayer;
    public override void EnterState(EnemyStateManager enemy)
    {
        //Animaciones, movimiento de IA, Daño a player
        Debug.Log("Atacando al chef");
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

        if (playerInSightRange && !playerInAttackRange)
        {
            enemy.SwitchState(enemy.HuntingState);
        }

        if (!playerInSightRange && !playerInAttackRange)
        {
            enemy.SwitchState(enemy.PattrolingState);
        }
    }
}
