using UnityEngine;

public class EnemyState
{
    public Enemy enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public string animBoolName { get; private set; }

    public Player player;

    protected bool triggerCalled;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Player _player)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
        this.player = _player;
    }

    public virtual void Enter()
    {
        enemy.animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        enemy.animator.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
