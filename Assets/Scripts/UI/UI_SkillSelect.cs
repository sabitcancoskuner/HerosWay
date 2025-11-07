using TMPro;
using UnityEngine;

public class UI_SkillSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardsToSelectText;

    private Transform position;
    private bool canPopulate = true;

    private int amountOfSkillsToSelect;

    public System.Action onAllCardsSelected;
    
    private void Update() {
        // Using unscaled delta time because when skill select UI is loaded Time Scale is set to zero
       position.localPosition  = Vector2.MoveTowards(position.localPosition, new Vector2(position.localPosition.x, -50), 850 * Time.unscaledDeltaTime);

       if (canPopulate)
       {
            UpdateCardUI();
       }
    }

    private void OnEnable() {
        position = GetComponent<Transform>();
        CanPopulateCards(true);
        UpdateCardUI();
    }

    private void UpdateCardUI()
    {
        amountOfSkillsToSelect = PlayerManager.instance.player.stats.GetCurrentWaveLevelUpCount();

        if (amountOfSkillsToSelect > 0)
        {
            cardsToSelectText.text = "Cards To Select: " + amountOfSkillsToSelect;
            PopulateSkillCards();
        }
        else {
            CanPopulateCards(false);
            cardsToSelectText.text = "";

            if (onAllCardsSelected != null)
            {
                onAllCardsSelected();
            }
        }
    }

    private void PopulateSkillCards()
    {
        UI_SkillCard[] cards = GetComponentsInChildren<UI_SkillCard>();

        foreach(UI_SkillCard card in cards) // %70 passive, %30 skill chance
        {
            float randomFloat = Random.Range(0f, 1f);

            if (randomFloat < 0.7f)
            {
                PassiveSkill passive = SkillManager.instance.GetRandomPassive();
                if (passive != null)
                {
                    card.SetupPassiveSkill(passive, passive.GetSkillSprite());
                }
            }
            else
            {
                SkillController skill = SkillManager.instance.GetRandomSkill();
                if (skill != null)
                {
                    card.SetupSkill(skill, skill.GetSkillSprite());
                }
            }
        }

        canPopulate = false;
    }

    public void CanPopulateCards(bool _populate)
    {
        canPopulate = _populate;
    }

    private void OnDisable() {
        position.localPosition = new Vector2(position.localPosition.x, -900);
    }
}
