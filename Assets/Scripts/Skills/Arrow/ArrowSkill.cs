using UnityEngine;

public class ArrowSkill : Skill
{
    [SerializeField] private float arrowSpeed;
    private Vector2 facingDir;

    public override void Start() {
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        facingDir = player.GetDirection();

        // Vector2 velocity = arrowSpeed * player.GetDirection();

        // rigidBody.linearVelocity = velocity;
        Rotate(player.GetDirection());
    }

    public override void Update()
    {
        base.Update();

        transform.position += new Vector3(arrowSpeed * facingDir.x * Time.deltaTime, arrowSpeed * facingDir.y * Time.deltaTime);
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
    }

    public void SetupArrow(float _skillDamage)
    {
        this.skillDamage = _skillDamage;
    }

}
