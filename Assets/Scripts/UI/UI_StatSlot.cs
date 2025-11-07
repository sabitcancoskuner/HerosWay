using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private StatType stat;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;

    private void OnValidate() {
        gameObject.name = "Stat - " + stat.ToString();
    }

    private void OnEnable() {
        UpdateStatValue();
    }

    public void UpdateStatValue()
    {
        PlayerStats stats = PlayerManager.instance.player.stats;

        if (stat == StatType.attackDamage)
        {
            statName.text = "Attack Damage";
            statValue.text = stats.attackDamage.GetValue().ToString();
        }

        else if (stat == StatType.attackSpeed)
        {
            statName.text = "Attack Speed";
            statValue.text = stats.attackSpeed.GetValue().ToString();
        }

        else if (stat == StatType.criticalChance)
        {
            statName.text = "Crit. Chance";
            statValue.text = stats.criticalChance.GetValue().ToString();
        }

        else if (stat == StatType.criticalDamage)
        {
            statName.text = "Crit. Damage";
            statValue.text = stats.criticalDamage.GetValue().ToString();
        }
        
        else if (stat == StatType.health)
        {
            statName.text = "Health";
            statValue.text = stats.maxHP.GetValue().ToString();
        }

        else if (stat == StatType.armor)
        {
            statName.text = "Armor";
            statValue.text = stats.armor.GetValue().ToString();
        }

        else if (stat == StatType.evasion)
        {
            statName.text = "Evasion";
            statValue.text = stats.evasion.GetValue().ToString();
        }
    }
    
    
}
