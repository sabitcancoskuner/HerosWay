using System.Collections;
using UnityEngine;

public class HealingHeart : MonoBehaviour
{
    private Player player;
    [SerializeField] private float healingAmount;
    [SerializeField] private float moveSpeed;
    
    void Start()
    {
        player = PlayerManager.instance.player;
    }

    void Update()
    {
        StartCoroutine("MoveToPlayer");
    }

    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>() != null)
        {
            player.stats.IncreaseCurrentHealth(healingAmount);
            AudioManager.instance.PlaySfx(12);
            Destroy(gameObject);
        }
    }
}
