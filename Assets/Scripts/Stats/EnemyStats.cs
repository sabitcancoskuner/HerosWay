using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    public override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void DoDamage(CharacterStats _targetStats, float _amount)
    {
        base.DoDamage(_targetStats, _amount);
        _targetStats.TakeDamage(_amount);
    }

    public override void TakeDamage(float _damage)
    {
        if (enemy.isVulnerable)
        {
            return;
        }
        base.TakeDamage(_damage);

        enemy.DamageImpact();
        fx.StartCoroutine("FlashFX");

        AudioManager.instance.PlaySfx(4);
    }

    public override void DecreaseHealth(float _amount)
    {
        base.DecreaseHealth(_amount);

        if (currentHP <= 0)
        {
            isDead = true;
            enemy.stateMachine.ChangeState(enemy.deadState);
        }
    }
}
