using System.Collections;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    private Player player;
    [SerializeField] private int expAmount;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        player = PlayerManager.instance.player;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("MoveToPlayer");
    }

    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(1f);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>() != null)
        {
            player.stats.IncreaseExperience(expAmount);
            Destroy(gameObject);
        }
    }
}
