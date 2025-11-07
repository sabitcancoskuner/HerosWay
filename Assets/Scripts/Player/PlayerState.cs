using UnityEngine;

public class PlayerState
{
    public Player player { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public string animBoolName { get; private set; }

    protected float xInput = 0;
    protected float yInput = 0;
    protected Vector2 direction;
    protected Vector2 facingDirection = Vector2.zero;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    
    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);

        facingDirection = player.GetDirection();
        player.animator.SetFloat("Horizontal", Mathf.RoundToInt(facingDirection.x));
        player.animator.SetFloat("Vertical", Mathf.RoundToInt(facingDirection.y));
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        direction.x = xInput;
        direction.y = yInput;
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
        player.SetAnimationSpeed(1f);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
