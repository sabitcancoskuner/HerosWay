using TMPro;
using UnityEngine;

public class UI_PassiveSkillTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI passiveSkillName;
    [SerializeField] private TextMeshProUGUI passiveSkillLevel;
    [SerializeField] private TextMeshProUGUI passiveSkillDescription;

    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    public void SetupTooltip(string _passiveName, int skillLevel, string _passiveDescription)
    {
        this.passiveSkillName.text = _passiveName;
        this.passiveSkillLevel.text = "Level: " + skillLevel.ToString();
        this.passiveSkillDescription.text = _passiveDescription;

    }

}
