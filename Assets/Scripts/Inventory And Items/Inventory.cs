using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private bool isInventoryOpen = true;

    public List<ItemData> startingItems;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    public List<InventoryItem> equipment;
    public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;

    [SerializeField] private Transform inventorySlotParent;
    private ItemSlot[] inventorySlots;

    [SerializeField] private Transform stashSlotParent;
    private ItemSlot[] stashSlots;

    [SerializeField] private Transform equipmentSlotParent;
    private ItemSlot_Equipment[] equipmentSlots;

    [SerializeField] private Transform statSlotParent;
    private UI_StatSlot[] statSlots;

    private void Awake() {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start() {
        inventory = new List<InventoryItem>(); // not yet implemented
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();

        inventorySlots = inventorySlotParent.GetComponentsInChildren<ItemSlot>();
        stashSlots = stashSlotParent.GetComponentsInChildren<ItemSlot>();
        equipmentSlots = equipmentSlotParent.GetComponentsInChildren<ItemSlot_Equipment>();
        statSlots = statSlotParent.GetComponentsInChildren<UI_StatSlot>();

        AddStartingItems();
    }

    public void AddStartingItems()
    {
        for(int i = 0; i < startingItems.Count; i++)
        {
            if (startingItems[i] != null)
            {
                AddToInventory(startingItems[i]);
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlots[i].equipmentType)
                {
                    equipmentSlots[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; i < stashSlots.Length; i++)
        {
            stashSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < stash.Count; i++)
        {
            stashSlots[i].UpdateSlot(stash[i]);
        }
    }

    public void UpdateStatui()
    {
        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].UpdateStatValue();
        }
    }

    public void AddToInventory(ItemData _item)
    {
        InventoryItem newItem = new InventoryItem(_item);
        inventory.Add(newItem);
        inventoryDictionary.Add(_item, newItem);

        UpdateUI();
    }

    public void AddToStash(ItemData _item)
    {
        if(stashDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDictionary.Add(_item, newItem);
        }

        if (isInventoryOpen)
        {
            UpdateUI();
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem inventoryValue))
        {
            inventory.Remove(inventoryValue);
            inventoryDictionary.Remove(_item);
        }

        if (stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(_item);
            }
            else {
                stashValue.RemoveStack();
            }
        }

        UpdateUI();
    }

    public void OpenAndCloseStash()
    {
        if (isInventoryOpen)
        {
            StashAnimation(false);
            isInventoryOpen = false;
            foreach (Image image in stashSlotParent.GetComponentsInChildren<Image>())
            {
                if (image.GetComponent<ItemSlot>() != null)
                {
                    image.color = Color.clear;
                    image.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
        else 
        {
            StashAnimation(true);
            isInventoryOpen = true;
            UpdateUI();
        }
    }

    private void StashAnimation(bool _opening)
    {
        if (_opening)
        {
            stashSlotParent.GetComponent<Animator>().SetBool("Closing", !_opening);
            stashSlotParent.GetComponent<Animator>().SetBool("Opening", _opening);
        }
        else {
            stashSlotParent.GetComponent<Animator>().SetBool("Opening", _opening);
            stashSlotParent.GetComponent<Animator>().SetBool("Closing", !_opening);
        }

    }

    public void EquipItem(ItemData _equipment)
    {
        ItemDataEquipment newEquipment = _equipment as ItemDataEquipment;
        InventoryItem newItem = new InventoryItem(_equipment);
        ItemDataEquipment oldEquipment = null;

        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                oldEquipment = item.Key;
            }
        }

        if (oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddToInventory(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_equipment);
        UpdateStatui();

        AudioManager.instance.PlaySfx(8);
    }

    public void UnequipItem(ItemDataEquipment _equipmentToRemove)
    {
        if (equipmentDictionary.TryGetValue(_equipmentToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(_equipmentToRemove);
            _equipmentToRemove.RemoveModifiers();
        }

        UpdateUI();
        UpdateStatui();

        AudioManager.instance.PlaySfx(9);
    }
}
