using UnityEngine;

public class Skill : MonoBehaviour
{
    protected Player player;

    protected Rigidbody2D rigidBody;

    [SerializeField] protected float lifeTime = 2f;
    [SerializeField] protected float skillDamage;

    public virtual void Start() {
        player = PlayerManager.instance.player;
    }

    public virtual void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

    }

    public virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Enemy>() != null)
        {
            player.stats.DoDamage(other.GetComponent<EnemyStats>(), skillDamage);
            Destroy(gameObject);
        }
    }

    protected void Rotate(Vector2 _direction)
    {
        if (_direction.x == 1) // shoot east
        {
            transform.Rotate(0, 0, -90);
        }
        else if (_direction.x == -1) // shoot west
        {
            transform.Rotate(0, 0, 90);
        }
        else if (_direction.y == -1) // shoot south
        {
            transform.Rotate(0, 0, 180);
        }
    }

}
