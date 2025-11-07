using System.Collections;
using UnityEngine;

public class Player : Entity
{

    public float moveSpeed;

    [SerializeField] private bool canRun;

    public SkillManager skills;

    #region Components
    public Rigidbody2D rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public PlayerStats stats { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    #region Attack 
    public bool canAttack;
    public Transform attackCheck;
    public float attackCheckRadius;
    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        runState = new PlayerRunState(this, stateMachine, "Run");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }

    void Start()
    {
        skills = SkillManager.instance;

        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
        
        stateMachine.Initialize(idleState);
    }


    void Update()
    {
        if (Time.timeScale == 0) // stop changing directions and playing animations etc.
        {
            return;
        }
        stateMachine.currentState.Update();

        if (stats.isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            stateMachine.ChangeState(attackState);
        }
        
    }

    public void SetVelocity(float _xVelocity, float _yVelocity) 
    {
        rigidBody.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }

    public void SetZeroVelocity()
    {
        rigidBody.linearVelocity = new Vector2(0, 0);
    }


    public override void SetDirection(Vector2 _input)
    {
        base.SetDirection(_input);

        RotateAttack(facingDir);
    }

    public bool CanRun()
    {
        return canRun;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public void RotateAttack(FacingDir _direction)
    {
        if (_direction == FacingDir.East)
        {
            attackCheck.position = new Vector3(0.7f, 0, 0);
        }
        else if (_direction == FacingDir.West)
        {
            attackCheck.position = new Vector3(-0.7f, 0, 0);
        }
        else if (_direction == FacingDir.North)
        {
            attackCheck.position = new Vector3(0, 0.7f, 0);
        }
        else 
        {
            attackCheck.position = new Vector3(0, -0.7f, 0);
        }

    }

    public IEnumerator Die()
    {
        // ADD IMPLEMENTATION
        skills.DisableAllSkills();
        DestroyAllSkillObjects();
        yield return new WaitForSeconds(2f);
        GameObject.FindAnyObjectByType<UI>().LoadEndScreen();
    }

    public void DestroyAllSkillObjects()
    {
        foreach(Skill skillObject in GetComponentsInChildren<Skill>())
        {
            Destroy(skillObject.gameObject);
        }
    }

    public void DisableAllSkills()
    {
        foreach(SkillController skill in skills.skillPool)
        {
            skill.DisableSkill();
        }
    }

    public void EnableAllSkills()
    {
        foreach(SkillController skill in skills.skillPool)
        {
            skill.ActivateSkill();
            skill.ResetCooldown();
        }
    }

    public void SetAnimationSpeed(float _speed)
    {
        animator.speed = _speed;
    }

    public void SetAttackState(bool _state)
    {
        canAttack = _state;
    }
}
