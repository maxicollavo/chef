using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingState : EnemyBaseState
{
    public NavMeshAgent agent;
    public Transform player;
    bool playerInSightRange;
    bool playerInAttackRange;
    float sightRange = 8f;
    float attackRange = 2f;

    private LayerMask whatIsPlayer;

    public override void EnterState(EnemyStateManager enemy)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("PlayerBody").transform;

        //Hacer sistema de waypoints acá
        Debug.Log("Estoy patrullando");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        //Cada vez que lo atacamos
        GameObject other = collision.gameObject;
        if (other.CompareTag("Bullet") || other.CompareTag("Knife"))
        {
            enemy.SwitchState(enemy.HurtedState);
        }
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Cambiar de estado dependiendo lo que pase, con IFS
        playerInSightRange = Physics.CheckSphere(enemy.transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(enemy.transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange)
        {
            enemy.SwitchState(enemy.HuntingState);
        }

        if (playerInSightRange && playerInAttackRange)
        {
            enemy.SwitchState(enemy.AttackingState);
        }
    }
}
