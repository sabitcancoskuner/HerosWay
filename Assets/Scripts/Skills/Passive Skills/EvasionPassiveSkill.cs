using System;
using UnityEngine;

public class EvasionPassiveSkill : PassiveSkill
{
    private EvasionPassiveLevels evasionPassiveLevels;
    private EvasionPassiveSkillInfo currentEvasionPassive;

    public override void Start()
    {
        base.Start();

        evasionPassiveLevels = JsonUtility.FromJson<EvasionPassiveLevels>(levelJson.text);
        passiveSkillNextLevelDescription = evasionPassiveLevels.evasionPassive[0].skillDescription;
        maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateEvasionPassiveInfo();
        
        player.stats.UpdateStat(passiveType, value);
    }

    public void UpdateEvasionPassiveInfo()
    {
        currentEvasionPassive = evasionPassiveLevels.evasionPassive[skillLevel - 1];
        this.value = currentEvasionPassive.evasionRate;
        this.passiveSkillCurrentLevelDescription = currentEvasionPassive.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            passiveSkillNextLevelDescription = evasionPassiveLevels.evasionPassive[skillLevel].skillDescription;
        }
    }

    private int GetMaxPassiveSkillLevel()
    {
        int lastIndex = evasionPassiveLevels.evasionPassive.Length - 1;
        return evasionPassiveLevels.evasionPassive[lastIndex].skillLevel;
    }

}

#region Skill Classes
[Serializable]
class EvasionPassiveSkillInfo
{
    public int skillLevel;
    public int evasionRate;
    public string skillDescription;
}

[Serializable]
class EvasionPassiveLevels
{
    public EvasionPassiveSkillInfo[] evasionPassive;
}
#endregion
