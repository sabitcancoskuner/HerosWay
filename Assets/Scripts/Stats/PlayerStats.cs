using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    public int currentXp = 0;
    public float nextLevelXp = 40;
    public int playerLevelStart = 1;
    public int playerLevelEnd = 1;

    public System.Action onHealthChanged;
    public System.Action onLevelUp;

    public override void Start()
    {
        base.Start();
        player = GetComponent<Player>();

        SkillManager.instance.onSkillSelected += IncreaseStartLevel;
    }

    private void Update() {
        CheckForLevelUp();
    }

    public override void DoDamage(CharacterStats _targetStats, float _amount)
    {
        base.DoDamage(_targetStats, _amount);

        if (CanCrit())
        {
            _amount = CalculateCriticalDamage(_amount);
        }
        
        _targetStats.TakeDamage(_amount); // change this
    }

    public override void TakeDamage(float _amount)
    {
        base.TakeDamage(_amount);

        if (evaded)
        {
            evaded = false;
            return;
        }

        if (blocked)
        {
            blocked = false;
            AudioManager.instance.PlaySfx(5); // PLAY BLOCK SFX
            return;
        }

        fx.StartCoroutine("FlashFX");
    }

    public override void DecreaseHealth(float _amount)
    {
        base.DecreaseHealth(_amount);

        if (onHealthChanged != null)
        {
            onHealthChanged();
        }

        if (currentHP <= 0)
        {
            player.stateMachine.ChangeState(player.deadState);
        }
    }

    public void IncreaseCurrentHealth(float _amount)
    {
        currentHP += _amount;

        if (currentHP > maxHP.GetValue())
        {
            currentHP = maxHP.GetValue();
        }

        onHealthChanged();
    }

    public void IncreaseExperience(int _amount)
    {
        currentXp += _amount;
    }

    private void CheckForLevelUp()
    {
        if (currentXp >= nextLevelXp)
        {
            AudioManager.instance.PlaySfx(0);

            playerLevelEnd++;
            onLevelUp();
            nextLevelXp += nextLevelXp * 0.25f;
            currentXp = 0;
            CheckForLevelUp();
        }
    }

    private void IncreaseStartLevel()
    {
        playerLevelStart++;
    }

    public int GetCurrentWaveLevelUpCount()
    {
        return playerLevelEnd - playerLevelStart;
    }

    private int CalculateCriticalDamage(float _damage)
    {
        float multiplier = criticalDamage.GetValue() / 100;
        return Mathf.CeilToInt(_damage * multiplier);
    }

    public float CalculateAnimationSpeed()
    {
        return attackSpeed.GetValue() / 100;
    }

    public void UpdateStat(StatType _passive, float _value)
    {
        if (_passive == StatType.attackDamage)
        {
            attackDamage.AddMultiplierModifier(_value);
        }

        else if (_passive == StatType.attackSpeed)
        {
            attackSpeed.AddMultiplierModifier(_value);
        }

        else if (_passive == StatType.criticalChance)
        {
            criticalChance.AddModifier(_value);
        }

        else if (_passive == StatType.criticalDamage)
        {
            criticalDamage.AddModifier(_value);
        }

        else if (_passive == StatType.armor)
        {
            armor.AddModifier(_value);
        }

        else if (_passive == StatType.health)
        {
            maxHP.AddModifier(_value);
            IncreaseCurrentHealth(_value);
        }

        else if (_passive == StatType.evasion)
        {
            evasion.AddModifier(_value);
        }

    }

    public void RemoveStat(StatType _passive, float _value)
    {
        if (_passive == StatType.attackDamage)
        {
            attackDamage.RemoveMultiplierModifier(_value);
        }

        else if (_passive == StatType.attackSpeed)
        {
            attackSpeed.RemoveMultiplierModifier(_value);
        }

        else if (_passive == StatType.criticalChance)
        {
            criticalChance.RemoveModifier(_value);
        }

        else if (_passive == StatType.criticalDamage)
        {
            criticalDamage.RemoveModifier(_value);
        }

        else if (_passive == StatType.armor)
        {
            armor.RemoveModifier(_value);
        }

        else if (_passive == StatType.health)
        {
            maxHP.RemoveModifier(_value);
        }

        else if (_passive == StatType.evasion)
        {
            evasion.RemoveModifier(_value);
        }
    }

}
