using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public EntityFX fx;

    public bool isDead;

    [Header("Offensive Stats")]
    public Stat attackDamage;
    public Stat attackSpeed;
    public Stat criticalChance;
    public Stat criticalDamage;

    [Header("Defensive Stats")]
    public Stat maxHP;
    public Stat armor;
    public Stat evasion;

    [Space]
    public float currentHP;
    protected bool evaded;
    protected bool blocked;

    public virtual void Start() {
        fx = GetComponent<EntityFX>();
        
        currentHP = maxHP.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats, float _amount)
    {
        
    }

    public virtual void TakeDamage(float _damage)
    {
        if (CanEvade())
        {
            evaded = true;
            return;
        }
        _damage = CalculateBlockDamage(_damage);

        if (_damage == 0)
        {
            blocked = true;
            return;
        }

        DecreaseHealth(_damage);
    }

    private float CalculateBlockDamage(float _damage)
    {
        float blockPerc = (armor.GetValue() / 200); // 200 armor equals to %100 block
        float damage = _damage * (1f - blockPerc);
        return damage;
    }

    public virtual void DecreaseHealth(float _amount)
    {
        currentHP -= _amount;

        if (currentHP <= 0)
        {
            isDead = true;
        }

    }

    public virtual bool CanEvade()
    {
        float evasionChance = evasion.GetValue() / 100;
        float randomFloat = Random.Range(0f, 1f);

        if (randomFloat < evasionChance)
        {
            return true;
        }

        return false;
    }

    public virtual bool CanCrit()
    {
        float chanceToCrit = criticalChance.GetValue() / 100;
        float randomFloat = Random.Range(0, 1f);

        if (randomFloat < chanceToCrit)
        {
            return true;
        }

        return false;
    }
}
