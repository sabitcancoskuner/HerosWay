using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public ArrowSkillController arrowSkill { get; private set; }
    public ShurikenSkillController shurikenSkill { get; private set; }
    public KnifeSkillController knifeSkill { get; private set; }

    public List<SkillController> skillPool;
    public List<PassiveSkill> passiveSkillPool;

    public List<SkillController> activeSkills;
    public List<PassiveSkill> activePassiveSkills;

    public System.Action onSkillSelected;
    
    private void Awake() {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start() {
        arrowSkill = GetComponent<ArrowSkillController>();
        shurikenSkill = GetComponent<ShurikenSkillController>();
        knifeSkill = GetComponent<KnifeSkillController>();

        skillPool = GetComponents<SkillController>().ToList();
        passiveSkillPool = GetComponents<PassiveSkill>().ToList();

        activeSkills = new List<SkillController>();
        activePassiveSkills = new List<PassiveSkill>();

        UI.instance.skillSelectUI.GetComponent<UI_SkillSelect>().onAllCardsSelected += ActivateAllSkills;
    }

    public void AddSkillToActiveSkill(SkillController _skill)
    {
        activeSkills.Add(_skill);

        if (onSkillSelected != null)
        {
            onSkillSelected();
        }
    }

    public void AddToSkillPool(SkillController _skill)
    {
        skillPool.Add(_skill);
    }

    public void RemoveSkillFromPool(SkillController _skill)
    {
        skillPool.Remove(_skill);
    }

    public void AddPassiveToActiveSkills(PassiveSkill _passive)
    {
        activePassiveSkills.Add(_passive);

        if (onSkillSelected != null)
        {
            onSkillSelected();
        }
    }

    public void AddToPassiveSkillPool(PassiveSkill _passive)
    {
        passiveSkillPool.Add(_passive);
    }

    public void RemoveFromPassiveSkillPool(PassiveSkill _passive)
    {
        passiveSkillPool.Remove(_passive);
    }

    public void DisableAllSkills()
    {
        foreach (SkillController skill in activeSkills)
        {
            skill.DisableSkill();
        }
    }

    public void ActivateAllSkills()
    {
        foreach (SkillController skill in activeSkills)
        {
            skill.ActivateSkill();
        }
    }

    public void ResetAllSkillCooldowns()
    {
        foreach (SkillController skill in activeSkills)
        {
            skill.ResetCooldown();
        }
    }

    public SkillController GetRandomSkill()
    {   
        if (skillPool.Count == 0)
        {
            return null;
        } 

        int randomIndex = Random.Range(0, skillPool.Count);
        if (randomIndex >= 0)
        {
            SkillController randomSkill = skillPool[randomIndex];
            skillPool.Remove(randomSkill);

            return randomSkill;
        }

        return null;
    }

    public PassiveSkill GetRandomPassive()
    {
        int randomIndex = Random.Range(0, passiveSkillPool.Count);
        if (randomIndex >= 0)
        {   
            PassiveSkill randomPassive = passiveSkillPool[randomIndex];
            passiveSkillPool.Remove(randomPassive);

            return randomPassive;
        }

        return null;
    }
    
}
