using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0 || yInput != 0) {
            player.SetDirection(new Vector2(xInput, yInput));
            facingDirection = player.GetDirection();

            player.animator.SetFloat("Horizontal", Mathf.RoundToInt(facingDirection.x));
            player.animator.SetFloat("Vertical", Mathf.RoundToInt(facingDirection.y));

            if (player.CanRun())
            {
                stateMachine.ChangeState(player.runState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
