using System;
using UnityEngine;

public class AttackDamagePassiveSkill : PassiveSkill
{   
    private AttackDamageLevels passiveSkillInfos;
    private AttackDamageInfo currentPassiveInfo;

    public override void Start()
    {
        base.Start();

        passiveSkillInfos = JsonUtility.FromJson<AttackDamageLevels>(levelJson.text);
        passiveSkillNextLevelDescription = passiveSkillInfos.attackDamagePassive[0].skillDescription;
        maxPassiveSkillLevel = GetMaxPassiveSkillLevel();
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdatePassiveSkillInfo();

        player.stats.UpdateStat(passiveType, value);
    }

    private void UpdatePassiveSkillInfo()
    {
        currentPassiveInfo = passiveSkillInfos.attackDamagePassive[skillLevel - 1];
        this.value = currentPassiveInfo.attackBoostMultiplier;
        this.passiveSkillCurrentLevelDescription = currentPassiveInfo.skillDescription;

        if (skillLevel != maxPassiveSkillLevel)
        {
            passiveSkillNextLevelDescription = passiveSkillInfos.attackDamagePassive[skillLevel].skillDescription;
        }

    }

    private int GetMaxPassiveSkillLevel()
    {
        int lastIndex = passiveSkillInfos.attackDamagePassive.Length - 1;
        return passiveSkillInfos.attackDamagePassive[lastIndex].skillLevel;
    }

}

#region Skill Classes
[Serializable]
class AttackDamageInfo 
{
    public int skillLevel;
    public int attackBoostMultiplier;
    public string skillDescription;
}

[Serializable]
class AttackDamageLevels
{
    public AttackDamageInfo[] attackDamagePassive;
}
#endregion
