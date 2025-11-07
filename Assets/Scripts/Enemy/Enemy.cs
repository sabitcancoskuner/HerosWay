using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine;

    public EnemyStats stats;

    public Player player;

    public Rigidbody2D rb;
    public Animator animator;
    public CapsuleCollider2D capsuleCollider;

    public float moveSpeed;

    [Header("Attack Info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Knockback Info")]
    [SerializeField] protected float knockbackDuration;
    [SerializeField] protected Vector2 knockbackPower;

    [SerializeField] private float vulnerabilityTime;
    public bool isVulnerable;

    public EnemyMoveState moveState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    public EnemyDeadState deadState { get; private set; }
    public EnemyIdleState idleState { get; private set; }

    private void Awake() {
        player = PlayerManager.instance.player;

        stateMachine = new EnemyStateMachine();

        moveState = new EnemyMoveState(this, stateMachine, "Move", player);
        attackState = new EnemyAttackState(this, stateMachine, "Attack", player);
        deadState = new EnemyDeadState(this, stateMachine, "Dead", player);
        idleState = new EnemyIdleState(this, stateMachine, "Idle", player);
    }
    
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<EnemyStats>();

        if (SpawnManager.instance.gameObject.activeSelf)
        {
            SpawnManager.instance.AddEnemyInstance(this.gameObject);
        }
        SetDirection();
        stateMachine.Initialize(moveState);
    }

    public virtual void Update()
    {
        stateMachine.currentState.Update();
    }

    private void SetDirection()
    {
        Vector2 distanceToPlayer = transform.position - player.transform.position;
        Vector2 attackCheckPos = Vector2.zero;
        float xValue = distanceToPlayer.x;
        float yValue = distanceToPlayer.y;

        if (Mathf.Abs(xValue) > Mathf.Abs(yValue))
        {
            xValue = xValue > 0 ? -1f : 1f;

            animator.SetFloat("Horizontal", xValue);
            SetDirection(new Vector2(-xValue, 0));
            attackCheckPos.x = xValue > 0 ? 0.7f : -0.7f;
        }
        else 
        {
            yValue = yValue > 0 ? -1f : 1f;
            animator.SetFloat("Vertical", yValue);
            SetDirection(new Vector2(0, -yValue));
            attackCheckPos.y = yValue > 0 ? 0.7f : -0.7f;
        }

        attackCheck.localPosition = attackCheckPos;
    }

    public void DamageImpact()
    {
        StartCoroutine("HitKnockback");
        StartCoroutine("MakeVulnerable");
    }

    private IEnumerator HitKnockback()
    {
        Vector2 direction = GetDirection();
        rb.linearVelocity = new Vector2(knockbackPower.x * direction.x, knockbackPower.y * direction.y);
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector2.zero;
    }

    public IEnumerator Die()
    {
        SpawnManager.instance.RemoveEnemyInstance(this.gameObject);
        capsuleCollider.enabled = false;
        yield return new WaitForSeconds(1);
        GetComponent<ItemDrop>().DropItems();
        Destroy(gameObject);
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public IEnumerator MakeVulnerable()
    {
        isVulnerable = true;
        yield return new WaitForSeconds(vulnerabilityTime);
        isVulnerable = false;
    }
}
