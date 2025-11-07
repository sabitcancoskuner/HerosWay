using System;
using UnityEngine;

public class AttackSpeedPassiveSkill : PassiveSkill
{
    private AttackSpeedLevels attackSpeedLevels;
    private AttackSpeedPassiveInfo currentAttackSpeedInfo;

    public override void Start()
    {
        base.Start();

        attackSpeedLevels = JsonUtility.FromJson<AttackSpeedLevels>(levelJson.text);
        passiveSkillNextLevelDescription = attackSpeedLevels.attackSpeedPassive[0].skillDescription;
        this.maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateAttackSpeedInfo();

        player.stats.UpdateStat(passiveType, value);
    }

    private void UpdateAttackSpeedInfo()
    {
        currentAttackSpeedInfo = attackSpeedLevels.attackSpeedPassive[skillLevel - 1];
        this.value = currentAttackSpeedInfo.attackSpeedBoost;
        this.passiveSkillCurrentLevelDescription = currentAttackSpeedInfo.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            this.passiveSkillNextLevelDescription = attackSpeedLevels.attackSpeedPassive[skillLevel].skillDescription;
        }
    }

    private int GetMaxPassiveSkillLevel()
    {
        int lastIndex = attackSpeedLevels.attackSpeedPassive.Length - 1;
        return attackSpeedLevels.attackSpeedPassive[lastIndex].skillLevel;
    }

}

#region Skill Classes
[Serializable]
class AttackSpeedPassiveInfo
{
    public int skillLevel;
    public int attackSpeedBoost;
    public string skillDescription;
}

[Serializable]
class AttackSpeedLevels
{
    public AttackSpeedPassiveInfo[] attackSpeedPassive;
}
#endregion