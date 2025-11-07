using System.Collections;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;

    private float moveSpeed = 0f;

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.player.transform.position, moveSpeed * Time.deltaTime);
    }

    public void SetupItem(ItemData _item, Vector2 _velocity)
    {
        itemData = _item;

        SetupItemVisual();
        StartCoroutine("SetMoveSpeed", 3f);
    }

    public void SetupItemVisual()
    {
        if (itemData == null)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object - " + itemData.itemName;
    }

    private IEnumerator SetMoveSpeed(float _speed)
    {
        yield return new WaitForSeconds(0.5f);
        moveSpeed = _speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>() != null)
        {
            Inventory.instance.AddToStash(itemData);
            Destroy(gameObject);
        }
    }
}
