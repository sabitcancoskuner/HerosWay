using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Player _player) : base(_enemy, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (player.stats.isDead)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.moveState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        triggerCalled = false;
    }

}
