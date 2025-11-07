using System;
using UnityEngine;

public class ArrowSkillController : SkillController
{
    [SerializeField] private GameObject arrowPrefab;
    private ArrowLevels arrowInfos;
    private ArrowInfo currentArrowInfo;

    public override void Start()
    {
        base.Start();

        arrowInfos = JsonUtility.FromJson<ArrowLevels>(levelJson.text);
        this.skillNextLevelDescription = arrowInfos.arrow[0].skillDescription;
        this.maxSkillLevel = GetMaxSkillLevel();
    }

    public override void Update()
    {
        base.Update();

        if (canUseSkill && cooldownTimer <= 0)
        {
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        GameObject arrow = Instantiate(arrowPrefab, player.transform.position, Quaternion.identity, player.transform);
        arrow.GetComponent<ArrowSkill>().SetupArrow(skillDamage);

        cooldownTimer = skillCooldown;
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateArrowInfo();
    }

    public void UpdateArrowInfo()
    {
        currentArrowInfo = arrowInfos.arrow[skillLevel - 1];
        this.skillDamage = currentArrowInfo.skillDamage;
        this.skillCooldown = currentArrowInfo.skillCooldown;
        
        if (skillLevel != GetMaxSkillLevel())
        {
            this.skillNextLevelDescription = arrowInfos.arrow[skillLevel].skillDescription;
        }

    }

    private int GetMaxSkillLevel()
    {
        return arrowInfos.arrow[arrowInfos.arrow.Length - 1].skillLevel;
    }
}

#region Level classes
[Serializable]
class ArrowInfo
{
    public int skillLevel;
    public float skillCooldown;
    public float skillDamage;
    public string skillDescription;
}

[Serializable]
class ArrowLevels
{
    public ArrowInfo[] arrow;
}
#endregion
