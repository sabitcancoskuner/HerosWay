using System;
using UnityEngine;

public class CriticalDamagePassiveSkill : PassiveSkill
{
    private CriticalDamageLevels critDamagePassiveLevels;
    private CriticalDamagePassiveSkillInfo currentCritDamagePassiveInfo;

    public override void Start()
    {
        base.Start();

        critDamagePassiveLevels = JsonUtility.FromJson<CriticalDamageLevels>(levelJson.text);
        passiveSkillNextLevelDescription = critDamagePassiveLevels.critDamagePassive[0].skillDescription;
        maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateCritDamageInfo();

        player.stats.UpdateStat(passiveType, value);
    }

    private void UpdateCritDamageInfo()
    {
        currentCritDamagePassiveInfo = critDamagePassiveLevels.critDamagePassive[skillLevel - 1];
        this.value = currentCritDamagePassiveInfo.critDamage;
        this.passiveSkillCurrentLevelDescription = currentCritDamagePassiveInfo.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            passiveSkillNextLevelDescription = critDamagePassiveLevels.critDamagePassive[skillLevel].skillDescription;
        }
    }

    private int GetMaxPassiveSkillLevel()
    {
        int lastIndex = critDamagePassiveLevels.critDamagePassive.Length - 1;
        return critDamagePassiveLevels.critDamagePassive[lastIndex].skillLevel;
    }

}

#region Skill Classes
[Serializable]
class CriticalDamagePassiveSkillInfo
{
    public int skillLevel;
    public int critDamage;
    public string skillDescription;
}

[Serializable]
class CriticalDamageLevels
{
    public CriticalDamagePassiveSkillInfo[] critDamagePassive;
}
#endregion
