using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Helmet,
    Chestplate,
    Leggings,
    Boots,
    Amulet,
    Ring,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;
    public float damage;
    public float hp;
    public float armor;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.attackDamage.AddModifier(damage);
        playerStats.maxHP.AddModifier(hp);
        playerStats.IncreaseCurrentHealth(hp);
        playerStats.armor.AddModifier(armor);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.stats;
        
        playerStats.attackDamage.RemoveModifier(damage);
        playerStats.maxHP.RemoveModifier(hp);
        playerStats.DecreaseHealth(hp);
        playerStats.armor.RemoveModifier(armor);
    }
}
