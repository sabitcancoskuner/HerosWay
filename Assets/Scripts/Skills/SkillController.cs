using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected Player player;

    [SerializeField] protected string skillName;
    [SerializeField] protected Sprite skillImage;
    [SerializeField] protected bool canUseSkill;
    [SerializeField] protected int skillLevel = 1;
    [SerializeField] protected float skillCooldown;
    [SerializeField] protected float skillDamage;
    [SerializeField] protected TextAsset levelJson;

    protected int maxSkillLevel;
    
    protected string skillNextLevelDescription;

    protected float cooldownTimer = 0.1f;

    public virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    
    public virtual void Update()
    {
        if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            return;
        }
        cooldownTimer -= Time.deltaTime;
    }

    public void ActivateSkill()
    {
        if (skillLevel != 0)
        {
            canUseSkill = true;
        }
    }

    public void DisableSkill()
    {
        canUseSkill = false;
    }

    public float GetRemainingSkillTime()
    {
        return cooldownTimer / skillCooldown;
    }

    public virtual void UseSkill()
    {

    }

    public virtual void LevelUpSkill()
    {
        skillLevel++;

        if (skillLevel == maxSkillLevel)
        {
            player.skills.RemoveSkillFromPool(this);
        }

        if (skillLevel == 1)
        {
            // canUseSkill = true;
            player.skills.AddSkillToActiveSkill(this);
        }
        else
        {
            SkillManager.instance.onSkillSelected();
        }
        ResetCooldown();
    }

    public virtual int GetCurrentSkillLevel()
    {
        return skillLevel;
    }

    public int GetMaxLevel()
    {
        return maxSkillLevel;
    }

    public virtual string GetNextLevelDescription()
    {
        return skillNextLevelDescription;
    }

    public virtual void CanUseSkill(bool _status)
    {
        canUseSkill = _status;
    }

    public virtual void ResetCooldown()
    {
        cooldownTimer = 0;
    }

    public Sprite GetSkillSprite()
    {
        return this.skillImage;
    }

    public string GetSkillName()
    {
        return this.skillName;
    }

}
