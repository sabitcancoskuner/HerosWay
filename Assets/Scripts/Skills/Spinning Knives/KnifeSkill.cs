using UnityEngine;

public class KnifeSkill : Skill
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private float spinRadius;

    private CapsuleCollider2D capsuleCollider;
    
    private float currentAngle;
    private Vector2 startingPos;

    public override void Start()
    {
        base.Start();

        capsuleCollider = GetComponent<CapsuleCollider2D>();

        startingPos = player.transform.position;
    }

    public override void Update()
    {
        base.Update();
        Spin();
    }

    private void Spin()
    {
        currentAngle += angularSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle)) * spinRadius;
        transform.position = startingPos + offset;
        transform.right = (player.transform.position - transform.position).normalized; // FOR ROTATION
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            HitEnemy(other.GetComponent<EnemyStats>());
        }
    }

    public void SetupKnife(float _damage, float _angularSpeed, float _lifeTime)
    {
        this.skillDamage = _damage;
        this.angularSpeed = _angularSpeed;
        this.lifeTime = _lifeTime;
    }

    private void HitEnemy(EnemyStats enemy)
    {
        player.stats.DoDamage(enemy, skillDamage);
    }

    public float GetLifeTime()
    {
        return this.lifeTime;
    }
}
