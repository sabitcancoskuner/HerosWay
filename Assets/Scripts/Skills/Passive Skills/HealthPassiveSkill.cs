using System;
using UnityEngine;

public class HealthPassiveSkill : PassiveSkill
{
    private HealthPassiveLevels healthPassiveLevels;
    private HealthPassiveSkillInfo currentHealthPassive;

    public override void Start()
    {
        base.Start();

        healthPassiveLevels = JsonUtility.FromJson<HealthPassiveLevels>(levelJson.text);
        passiveSkillNextLevelDescription = healthPassiveLevels.healthPassive[0].skillDescription;
        maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateHealthPassiveSkillInfo();

        player.stats.UpdateStat(passiveType, value);
    }

    private void UpdateHealthPassiveSkillInfo()
    {
        currentHealthPassive = healthPassiveLevels.healthPassive[skillLevel - 1];
        this.value = currentHealthPassive.hp;
        this.passiveSkillCurrentLevelDescription = currentHealthPassive.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            passiveSkillNextLevelDescription = healthPassiveLevels.healthPassive[skillLevel].skillDescription;
        }

    }

    public int GetMaxPassiveSkillLevel()
    {
        int lastIndex = healthPassiveLevels.healthPassive.Length - 1;
        return healthPassiveLevels.healthPassive[lastIndex].skillLevel;
    }


}

#region Skill classses
[Serializable]
class HealthPassiveSkillInfo
{
    public int skillLevel;
    public int hp;
    public string skillDescription;
}

[Serializable]
class HealthPassiveLevels
{
    public HealthPassiveSkillInfo[] healthPassive;
}
#endregion
