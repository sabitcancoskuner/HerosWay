using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PassiveSkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PassiveSkill passive;
    [SerializeField] private UI_PassiveSkillTooltip tooltip;
    private string skillname;
    private int skillLevel = 0;
    private string skillDescription;

    public void SetPassive(PassiveSkill _passive)
    {
        passive = _passive;
    }

    private void OnEnable() {
        if (passive != null)
        {
            Image passiveImage = GetComponent<Image>();
            passiveImage.sprite = passive.GetSkillSprite();
            skillname = passive.GetSkillName();
            skillLevel = passive.GetCurrentSkillLevel();
            skillDescription = passive.GetDescription();
        }
    }

    private void OnDisable() {
        if (tooltip.gameObject.activeSelf)
        {
            tooltip.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (passive != null)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.SetupTooltip(skillname, skillLevel, skillDescription);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }

}
