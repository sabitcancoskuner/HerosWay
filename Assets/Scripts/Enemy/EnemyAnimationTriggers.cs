using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private Enemy enemy;

    private void Awake() {
        enemy = GetComponentInParent<Enemy>();
    }

    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                // Do damage to the enemy
                enemy.stats.DoDamage(hit.GetComponent<PlayerStats>(), enemy.stats.attackDamage.GetValue());
                // Play sound
            }
        }
    }
}
