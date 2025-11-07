using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Character : MonoBehaviour
{
    private List<PassiveSkill> passivesInThePanel = new List<PassiveSkill>();

    private void OnEnable() {
        List<PassiveSkill> passives = SkillManager.instance.activePassiveSkills;

        foreach (PassiveSkill passive in passives)
        {
            if (!passivesInThePanel.Contains(passive))
            {
                passivesInThePanel.Add(passive);
            }
        }

        if (passivesInThePanel != null)
        {
            FillPassiveSkillSlots();
        }

    }

    private void FillPassiveSkillSlots()
    {
        List<Image> allSlots = GetComponentsInChildren<Image>().ToList();
        allSlots.RemoveAt(0); // this is the panel itself
        for (int i = 0; i < passivesInThePanel.Count; i++)
        {
            PassiveSkill currentPassive = passivesInThePanel[i];
            allSlots[i].GetComponent<UI_PassiveSkillSlot>().SetPassive(currentPassive);
        }
    }
}
