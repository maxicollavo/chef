using UnityEngine;

public class EnemyHurtedState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        //Take Damage al enemigo
        Debug.Log("He sido atacado :(");
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
