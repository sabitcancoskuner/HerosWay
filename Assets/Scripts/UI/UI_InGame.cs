using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private SkillManager skills; // might not need it after the implementation
    private WaveManager wave;
    private SpawnManager spawner; 

    [SerializeField] private Slider healthSlider;

    [Header("Skill Bar")]
    [SerializeField] private List<Image> normalImages;
    [SerializeField] private List<Image> skillImages;
    private List<SkillController> skillControllers;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] Image waveImage;
    [SerializeField] GameObject levelUpText;

    void Start()
    {
        skills = SkillManager.instance;
        wave = WaveManager.instance;
        spawner = SpawnManager.instance;

        skillControllers = new List<SkillController>();

        playerStats.onHealthChanged += UpdateHealthUI;
        playerStats.maxHP.onMaxHpChanged += UpdateHealthUI;
        skills.onSkillSelected += UpdateActiveSkills;
        playerStats.onLevelUp += PlayerLevelUpAnimation;
    }

    void Update()
    {
        if (skillControllers != null)
        {
            UpdateSkillUI();
        }
        UpdateTexts();
        UpdateWaveStatus();
    }

    private void UpdateHealthUI()
    {
        healthSlider.maxValue = playerStats.maxHP.GetValue();
        healthSlider.value = playerStats.currentHP; 
    }

    private void UpdateSkillUI()
    {
        foreach (SkillController skill in skillControllers) 
        {
            int skillIndex = skillControllers.IndexOf(skill);
            skillImages[skillIndex].sprite = skill.GetSkillSprite();
            skillImages[skillIndex].fillAmount = skill.GetRemainingSkillTime();
        }
    }

    private void UpdateTexts()
    {
        levelText.text = playerStats.playerLevelEnd.ToString();
        waveText.text = "Wave: " + wave.GetCurrentWave();
    }

    private void UpdateWaveStatus()
    {
        if (spawner.lastWaveEnemyCount != 0)
        {
            waveImage.fillAmount = (float)spawner.killedEnemies / (float)spawner.lastWaveEnemyCount; // may be try to animate with lerp
        }
    }

    private void UpdateActiveSkills()
    {
        foreach (SkillController skill in skills.activeSkills)
        {
            if (!skillControllers.Contains(skill))
            {
                skillControllers.Add(skill);
                SetSkillCooldownImage(skill);
            }
        }
    }

    private void SetSkillCooldownImage(SkillController skill)
    {
        int index = skillControllers.IndexOf(skill);
        normalImages[index].sprite = skill.GetSkillSprite(); // this is normal image
        normalImages[index].color = new Color32(255, 255, 255, 255); // to make it white
        
        skillImages[index].sprite = skill.GetSkillSprite(); // this blacked out image
        skillImages[index].color = new Color32(155, 155, 155, 255); // to make it gray
        skillImages[index].type = Image.Type.Filled;
        skillImages[index].fillMethod = Image.FillMethod.Radial360;
    }

    private void PlayerLevelUpAnimation()
    {
        Instantiate(levelUpText, this.transform.position + new Vector3(0, 100f), Quaternion.identity, this.transform);
    }
}
