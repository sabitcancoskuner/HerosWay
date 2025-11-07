using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SkillController skill;
    [SerializeField] private PassiveSkill passiveSkill;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI skillTypeText;
    [SerializeField] private Image skillImage;

    private void OnDisable() {

        CleanSkillInfo();

        if (skill != null && skill.GetCurrentSkillLevel() != 0)
        {
            skill.CanUseSkill(true);
        }
        
    }

    public void SetupSkill(SkillController _skill, Sprite _skillSprite)
    {
        this.skill = _skill;
        this.skillImage.sprite = _skillSprite;

        skill.CanUseSkill(false);
        skillTypeText.text = "Skill";
        skillTypeText.color = new Color32(255, 0, 0, 255);
        skillNameText.text = skill.GetSkillName();

        if ((skill.GetCurrentSkillLevel() + 1) == skill.GetMaxLevel()) // to check if it is last level or not
        {
            levelText.text = "Level: MAX";
        }
        else{
            levelText.text = "Level: " + (skill.GetCurrentSkillLevel() + 1).ToString();
        }

        descriptionText.text = skill.GetNextLevelDescription();
    }

    public void SetupPassiveSkill(PassiveSkill _passiveSkill, Sprite _skillSprite)
    {
        this.passiveSkill = _passiveSkill;
        this.skillImage.sprite = _skillSprite;

        skillTypeText.text = "Passive";
        skillTypeText.color = new Color32(0, 0, 255, 255);
        skillNameText.text = passiveSkill.GetSkillName();

        if ((passiveSkill.GetCurrentSkillLevel() + 1) == passiveSkill.GetMaxLevel()) // to check if it is last level or not
        {
            levelText.text = "Level: MAX";
        }
        else 
        {
            levelText.text = "Level: " + (passiveSkill.GetCurrentSkillLevel() + 1).ToString();
        }
        
        descriptionText.text = passiveSkill.GetNextLevelDescription();
    }

    private void CleanSkillInfo()
    {
        skillImage.sprite = null;
        skillNameText.text = "";
        skillTypeText.text = "";
        levelText.text = "";
        descriptionText.text = "";
    }

    public void AddSkillToPool()
    {
        if (skill != null && skill.GetCurrentSkillLevel() != skill.GetMaxLevel())
        {
            PlayerManager.instance.player.skills.AddToSkillPool(skill);
        }
        else if (passiveSkill != null && passiveSkill.GetCurrentSkillLevel() != passiveSkill.GetMaxLevel()) 
        {
            PlayerManager.instance.player.skills.AddToPassiveSkillPool(passiveSkill);
        }

        skill = null;
        passiveSkill = null;
    }

    public void LevelUpSkill()
    {
        if (skill != null)
        {
            this.skill.LevelUpSkill();

            AddSkillToPool();
        }

        if (passiveSkill != null)
        {
            this.passiveSkill.LevelUpSkill();

            AddSkillToPool();
        }


        AudioManager.instance.PlaySfx(1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color32(200, 200, 200, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
    }
}
