using UnityEngine;

public class EnemyDyingState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Fallecí");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        throw new System.NotImplementedException();
    }
}
