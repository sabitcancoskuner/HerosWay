using System;
using UnityEngine;

public class ArmorPassiveSkill : PassiveSkill
{
    private ArmorPassiveLevels armorPassiveSkillInfos;
    private ArmorPassiveInfo currentArmorPassive;

    public override void Start()
    {
        base.Start();

        armorPassiveSkillInfos = JsonUtility.FromJson<ArmorPassiveLevels>(levelJson.text);
        passiveSkillNextLevelDescription = armorPassiveSkillInfos.armorPassive[0].skillDescription;
        maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateArmorPassiveInfo();

        player.stats.UpdateStat(passiveType, value);
    }

    private void UpdateArmorPassiveInfo()
    {
        currentArmorPassive = armorPassiveSkillInfos.armorPassive[skillLevel - 1];
        this.value = currentArmorPassive.armor;
        this.passiveSkillCurrentLevelDescription = currentArmorPassive.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            passiveSkillNextLevelDescription = armorPassiveSkillInfos.armorPassive[skillLevel].skillDescription;
        }
    }

    private int GetMaxPassiveSkillLevel()
    {
        int lastIndex = armorPassiveSkillInfos.armorPassive.Length - 1;
        return armorPassiveSkillInfos.armorPassive[lastIndex].skillLevel;
    }
    
}

#region Skill classes
[Serializable]
class ArmorPassiveInfo
{
    public int skillLevel;
    public int armor;
    public string skillDescription;
}

[Serializable]
class ArmorPassiveLevels
{
    public ArmorPassiveInfo[] armorPassive;
}

#endregion
