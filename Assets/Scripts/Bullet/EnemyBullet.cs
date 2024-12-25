using UnityEngine;

public class EnemyBullet : Bullet 
{
	protected override bool GetAchieveTarget(Collision2D collision)
    {
        return collision.gameObject.TryGetComponent<Player>(out _);
	}
}