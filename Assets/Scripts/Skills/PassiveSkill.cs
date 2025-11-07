using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    protected Player player;

    [SerializeField] protected StatType passiveType;
    [SerializeField] protected string passiveSkillName;
    [SerializeField] protected Sprite passiveSkillImage;
    [SerializeField] protected TextAsset levelJson;
    [SerializeField] protected int skillLevel;
    [SerializeField] protected float value;

    public int maxPassiveSkillLevel;

    protected string passiveSkillNextLevelDescription;
    protected string passiveSkillCurrentLevelDescription;

    public virtual void Start() 
    {
        player = PlayerManager.instance.player;
    }

    public virtual void LevelUpSkill()
    {
        PlayerStats playerStats = player.stats;

        skillLevel++;

        if (skillLevel == maxPassiveSkillLevel)
        {
            player.skills.RemoveFromPassiveSkillPool(this);
        }
        
        if (skillLevel == 1)
        {
            player.skills.AddPassiveToActiveSkills(this);
        }
        else
        {
            SkillManager.instance.onSkillSelected();
            playerStats.RemoveStat(passiveType, value); // removing the passive skill value so the new value can be added to the modifier
        }
    }

    public Sprite GetSkillSprite()
    {
        return passiveSkillImage;
    }

    public int GetCurrentSkillLevel()
    {
        return this.skillLevel;
    }

    public string GetNextLevelDescription()
    {
        return this.passiveSkillNextLevelDescription;
    }

    public string GetSkillName()
    {
        return this.passiveSkillName;
    }

    public int GetMaxLevel()
    {
        return maxPassiveSkillLevel;
    }

    public string GetDescription()
    {
        return passiveSkillCurrentLevelDescription;
    }

}

