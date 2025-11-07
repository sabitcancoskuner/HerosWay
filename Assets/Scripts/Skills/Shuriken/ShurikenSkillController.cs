using System;
using UnityEngine;

public class ShurikenSkillController : SkillController
{
    [SerializeField] private GameObject shurikenPrefab;
    private ShurikenLevels shurikenInfos;
    private ShurikenInfo currentShurikenInfo;

    public override void Start()
    {
        base.Start();

        shurikenInfos = JsonUtility.FromJson<ShurikenLevels>(levelJson.text);
        skillNextLevelDescription = shurikenInfos.shuriken[0].skillDescription;
        maxSkillLevel = GetMaxSkillLevel();
    }

    public override void Update()
    {
        base.Update();

        if (canUseSkill && cooldownTimer <= 0)
        {
            ShootShuriken();
        }
    }

    private void ShootShuriken()
    {
        GameObject shuriken = Instantiate(shurikenPrefab, player.transform.position, Quaternion.identity, player.transform);
        shuriken.GetComponent<ShurikenSkill>().SetupShuriken(skillDamage);

        cooldownTimer = skillCooldown;
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateShurikenInfo();
    }
    
    private void UpdateShurikenInfo()
    {
        currentShurikenInfo = shurikenInfos.shuriken[skillLevel - 1];
        this.skillCooldown = currentShurikenInfo.skillCooldown;
        this.skillDamage = currentShurikenInfo.skillDamage;
        
        if (skillLevel != GetMaxSkillLevel())
        {
            skillNextLevelDescription = shurikenInfos.shuriken[skillLevel].skillDescription;
        }

    }

    private int GetMaxSkillLevel()
    {
        return shurikenInfos.shuriken[shurikenInfos.shuriken.Length - 1].skillLevel;
    }
}

#region Shuriken level classes
[Serializable]
class ShurikenInfo
{
    public int skillLevel;
    public float skillCooldown;
    public float skillDamage;
    public string skillDescription;
}

[Serializable]
class ShurikenLevels
{
    public ShurikenInfo[] shuriken;
}
#endregion
