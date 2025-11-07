using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private List<ItemData> possibleDrops;
    [SerializeField] private int amountOfItemsToDrop;
    [SerializeField] private GameObject emptyDropPrefab;
    
    [SerializeField] private GameObject xpOrb;
    [SerializeField] private GameObject healingHeart;
    
    public void DropItems()
    {
        float randomFloat = Random.Range(0, 1f);
        if (randomFloat < 0.2f)
        {
            ItemData randomItemData = PickRandomDrop();
            GameObject randomItem = Instantiate(emptyDropPrefab, transform.position, Quaternion.identity);
            randomItem.GetComponent<ItemObject>().SetupItem(randomItemData, GetComponent<Enemy>().GetDirection());
        }

        if (randomFloat < 0.1f)
        {
            Instantiate(healingHeart, transform.position, Quaternion.identity);
        }

        Instantiate(xpOrb, transform.position, Quaternion.identity);
    }

    private ItemData PickRandomDrop() // Change its implementation
    {
        float totalWeight = 0;
        foreach(ItemData item in possibleDrops)
        {
            totalWeight += item.dropChance;
        }

        float cumulativeWeight = 0;
        float random = Random.Range(0, totalWeight);

        foreach(ItemData item in possibleDrops)
        {
            if (random < (item.dropChance + cumulativeWeight))
            {
                possibleDrops.Remove(item);
                return item;
            }

            cumulativeWeight += item.dropChance;
        }

        return null;
    }
}
