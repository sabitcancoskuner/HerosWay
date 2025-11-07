using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    attackDamage,
    armor,
    health,
    evasion,
    criticalChance,
    criticalDamage,
    attackSpeed
}

[Serializable]
public class Stat
{
    public StatType statType;

    [SerializeField] private float baseValue;
    public List<float> modifiers;

    [SerializeField] private float baseMultiplier = 100f;
    public List<float> multiplierModifiers;

    public System.Action onMaxHpChanged;

    public void AddModifier(float _modifier)
    {
        modifiers.Add(_modifier);

        if (onMaxHpChanged != null)
        {
            onMaxHpChanged();
        }
    }

    public void RemoveModifier(float _modifier)
    {
        modifiers.Remove(_modifier);

        if (onMaxHpChanged != null)
        {
            onMaxHpChanged();
        }
    }
 
    public void SetDefaultValue(float _value)
    {
        baseValue = _value;
    }

    public float GetValue()
    {
        float multiplier = CalculateMultiplier();

        float finalValue = baseValue * multiplier;
        foreach(float modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void SetMultiplier(float _multiplier)
    {
        this.baseMultiplier = _multiplier;
    }

    public void AddMultiplierModifier(float _value)
    {
        multiplierModifiers.Add(_value);
    }

    public void RemoveMultiplierModifier(float _value)
    {
        multiplierModifiers.Remove(_value);
    }

    public float CalculateMultiplier()
    {
        float finalMultiplier = baseMultiplier;
        foreach (float multiplier in multiplierModifiers)
        {
            finalMultiplier += multiplier;
        }

        return finalMultiplier / 100;
    }
}
