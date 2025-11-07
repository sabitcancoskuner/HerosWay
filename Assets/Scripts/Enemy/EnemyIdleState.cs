using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Player _player) : base(_enemy, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
