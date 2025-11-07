using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;
    
    private void Awake() {
        player = GetComponentInParent<Player>();
    }

    private void AnimationTrigger()
    {
        player.stateMachine.currentState.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                // Do damage to the enemy
                player.stats.DoDamage(hit.GetComponent<EnemyStats>(), player.stats.attackDamage.GetValue());   
                // Play sound
            }
        }
    }
}
