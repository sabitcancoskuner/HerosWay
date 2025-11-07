using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI stackText;
    [SerializeField] protected GameObject itemTooltip;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem _item)
    {
        item = _item;
        itemImage.color = new Color32(255, 255, 255, 20);
        if (item != null)
        {
            itemImage.sprite = item.itemData.icon;
            itemImage.color = Color.white;

            if (item.stackSize > 1)
            {
                stackText.text = item.stackSize.ToString();
            }
            else {
                stackText.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {

        item = null;
        itemImage.sprite = null;
        itemImage.color = new Color32(255, 255, 255, 0);

        stackText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item == null)
            {
                return;
            }

            if (item.itemData.itemType == ItemType.Equipment)
            {
                Inventory.instance.EquipItem(item.itemData);
                itemTooltip.gameObject.SetActive(false);
            }
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        itemTooltip.gameObject.SetActive(true);
        itemTooltip.GetComponent<UI_ItemTooltip>().SetupTooltip(item.itemData.name, item.itemData.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }
        
        itemTooltip.gameObject.SetActive(false);
        itemTooltip.GetComponent<UI_ItemTooltip>().SetupTooltip("", "");
    }

}
