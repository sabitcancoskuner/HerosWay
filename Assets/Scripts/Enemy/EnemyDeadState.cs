using UnityEngine;

public class EnemyDeadState : EnemyState
{

    public EnemyDeadState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Player _player) : base(_enemy, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.StartCoroutine("Die");
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
