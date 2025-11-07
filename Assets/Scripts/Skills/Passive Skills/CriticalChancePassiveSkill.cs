using System;
using UnityEngine;

public class CriticalChancePassiveSkill : PassiveSkill
{
    private CriticalChanceLevels criticalChanceLevels;
    private CriticalChancePassiveSkillInfo currentCritChanceInfo;

    public override void Start()
    {
        base.Start();
        
        criticalChanceLevels = JsonUtility.FromJson<CriticalChanceLevels>(levelJson.text);
        passiveSkillNextLevelDescription = criticalChanceLevels.critChancePassive[0].skillDescription;
        maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateCritChancePassiveInfo();

        player.stats.UpdateStat(passiveType, value);
    }

    private void UpdateCritChancePassiveInfo()
    {
        currentCritChanceInfo = criticalChanceLevels.critChancePassive[skillLevel - 1];
        this.value = currentCritChanceInfo.critChance;
        this.passiveSkillCurrentLevelDescription = currentCritChanceInfo.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            passiveSkillNextLevelDescription = criticalChanceLevels.critChancePassive[skillLevel].skillDescription;
        }
    }
    
    private int GetMaxPassiveSkillLevel()
    {
        int lastIndex = criticalChanceLevels.critChancePassive.Length - 1;
        return criticalChanceLevels.critChancePassive[lastIndex].skillLevel;
    }

}

#region Skill Classes
[Serializable]
class CriticalChancePassiveSkillInfo
{
    public int skillLevel;
    public int critChance;
    public string skillDescription;
}

[Serializable]
class CriticalChanceLevels
{
    public CriticalChancePassiveSkillInfo[] critChancePassive;
}
#endregion
