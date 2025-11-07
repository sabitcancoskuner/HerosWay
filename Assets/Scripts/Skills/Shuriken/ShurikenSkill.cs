using System.Collections;
using UnityEngine;

public class ShurikenSkill : Skill
{
    [SerializeField] private float moveSpeed = 1f;

    #region Movement
    private Vector2 startPosition;

    private float amplitude = 1f;
    private float frequency = 1.5f;   

    private float xOffset = 0f;
    private float yOffset = 0f;
    #endregion


    private Entity.FacingDir direction;

    public override void Start()
    {
        base.Start();

        Vector2 playerDir = player.GetDirection();

        startPosition = transform.position;
        Rotate(playerDir);
        SetShurikenDirection(playerDir);
    }

    public override void Update()
    {
        base.Update();
        MoveShuriken();

    }

    private void MoveShuriken()
    {
        if (direction == Entity.FacingDir.East || direction == Entity.FacingDir.West)
        {
            // Update horizontal offset
            xOffset += moveSpeed * Time.deltaTime;

            // Calculate vertical position using sine of the horizontal offset
            float y = amplitude * Mathf.Sin(frequency * xOffset);

            // Update sprite position
            transform.position = startPosition + new Vector2(xOffset, y);
        }
        else
        {
            yOffset += moveSpeed * Time.deltaTime;

            float x = amplitude * Mathf.Sin(frequency * yOffset);

            transform.position = startPosition + new Vector2(x, yOffset);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            player.stats.DoDamage(other.GetComponent<EnemyStats>(), skillDamage);
        }
    }

    public void SetupShuriken(float _skillDamage)
    {
        this.skillDamage = _skillDamage;
    }

    private void SetShurikenDirection(Vector2 _input)
    {
        if (_input == Vector2.right)
        {
            direction = Entity.FacingDir.East;
        }
        else if (_input == Vector2.left)
        {
            direction = Entity.FacingDir.West;
            moveSpeed = moveSpeed * -1;
        }
        else if (_input == Vector2.up)
        {
            direction = Entity.FacingDir.North;
        }
        else if (_input == Vector2.down)
        {
            direction = Entity.FacingDir.South;
            moveSpeed = moveSpeed * -1;
        }
    }

}
