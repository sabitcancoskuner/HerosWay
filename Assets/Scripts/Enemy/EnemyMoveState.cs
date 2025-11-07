using UnityEngine;

public class EnemyMoveState : EnemyState
{

    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Player _player) : base(_enemy, _stateMachine, _animBoolName, _player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(enemy.transform.position, player.transform.position) < .7f)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        
        if (!player.stats.isDead)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
        }
        else {
            stateMachine.ChangeState(enemy.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
