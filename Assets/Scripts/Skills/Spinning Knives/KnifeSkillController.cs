using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class KnifeSkillController : SkillController
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private int amountOfKnives;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float lifeTime;

    private CircleOfKnifeLevels knifeInfos;
    private CircleOfKnifeInfo currentKnife;

    public override void Start()
    {
        base.Start();

        knifeInfos = JsonUtility.FromJson<CircleOfKnifeLevels>(levelJson.text);
        this.skillNextLevelDescription = knifeInfos.knife[0].skillDescription;
        this.maxSkillLevel = GetMaxSkillLevel();
    }

    public override void Update()
    {
        base.Update();

        if (canUseSkill && cooldownTimer <= 0)
        {
            StartCoroutine(CreateKnives());
        }
    }

    private IEnumerator CreateKnives()
    {
        cooldownTimer = skillCooldown;
        for(int i = 0; i < amountOfKnives; i++)
        {
            GameObject knife = Instantiate(knifePrefab, player.transform.position, Quaternion.identity, player.transform);
            knife.GetComponent<KnifeSkill>().SetupKnife(skillDamage, angularSpeed, lifeTime);
            yield return new WaitForSeconds(Mathf.PI * 2 / amountOfKnives / angularSpeed);
        }
        
    }

    public override void LevelUpSkill()
    {
        base.LevelUpSkill();
        UpdateKnifeInfo();
    }

    private void UpdateKnifeInfo()
    {
        currentKnife = knifeInfos.knife[skillLevel - 1];
        this.skillCooldown = currentKnife.skillCooldown;
        this.skillDamage = currentKnife.skillDamage;
        this.amountOfKnives = currentKnife.amountOfKnives;
        
        if (skillLevel != GetMaxSkillLevel())
        {
            this.skillNextLevelDescription = knifeInfos.knife[skillLevel].skillDescription;
        }

    }

    private int GetMaxSkillLevel()
    {
        return knifeInfos.knife[knifeInfos.knife.Length - 1].skillLevel;
    }
}

#region Circle of knife level classes
[Serializable] // WHY ARE YOU USING THIS
class CircleOfKnifeInfo
{
    public int skillLevel;
    public float skillCooldown;
    public float skillDamage;
    public string skillDescription;
    public int amountOfKnives;
}

[Serializable]
class CircleOfKnifeLevels
{
    public CircleOfKnifeInfo[] knife;
}
#endregion