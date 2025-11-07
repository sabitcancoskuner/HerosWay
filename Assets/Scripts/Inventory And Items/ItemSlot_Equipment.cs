using UnityEngine.EventSystems;

public class ItemSlot_Equipment : ItemSlot
{
    public EquipmentType equipmentType;

    private void OnValidate() {
        gameObject.name = "Equipment Slot - " + equipmentType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item == null || item.itemData == null)
            {
                return;
            }

            Inventory.instance.UnequipItem(item.itemData as ItemDataEquipment);
            Inventory.instance.AddToInventory(item.itemData);
            itemTooltip.gameObject.SetActive(false);
            
            CleanUpSlot();
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || item.itemData == null)
        {
            return;
        }
        base.OnPointerEnter(eventData);
    }
}
