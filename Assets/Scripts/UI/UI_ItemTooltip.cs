using TMPro;
using UnityEngine;

public class UI_ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void SetupTooltip(string _name, string _description)
    {
        this.itemName.text = _name;
        this.itemDescription.text = _description;
    }
}
