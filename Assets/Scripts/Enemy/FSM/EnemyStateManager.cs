using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState; //Contiene la referencia al estado activo de la FSM

    public EnemyPatrollingState PattrolingState = new EnemyPatrollingState();
    public EnemyHuntingState HuntingState = new EnemyHuntingState();
    public EnemyAttackingState AttackingState = new EnemyAttackingState();
    public EnemyHurtedState HurtedState = new EnemyHurtedState();
    public EnemyDyingState DyingState = new EnemyDyingState();

    void Start()
    {
        //Estado en el que empieza el enemigo
        currentState = PattrolingState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
