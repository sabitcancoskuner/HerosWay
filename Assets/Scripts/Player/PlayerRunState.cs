using UnityEngine;

public class PlayerRunState : PlayerState
{

    public PlayerRunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        Move();
        CheckForMovementChange();

        if (xInput == 0 && yInput == 0)
        { // when movement is zero
            stateMachine.ChangeState(player.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    private void CheckForMovementChange()
    {

        if (direction.x != xInput || direction.y != yInput)
        {
            player.SetDirection(direction);
            facingDirection = direction;

            player.animator.SetFloat("Horizontal", xInput);
            player.animator.SetFloat("Vertical", yInput);
        }
    }

    private void Move()
    {
        player.rigidBody.MovePosition(player.rigidBody.position + direction.normalized * player.moveSpeed * Time.fixedDeltaTime);
        player.animator.SetFloat("Horizontal", xInput);
        player.animator.SetFloat("Vertical", yInput);
    }
}
