using UnityEngine;

public class PlayerBullet : Bullet 
{
    protected override bool GetAchieveTarget(Collision2D collision)
    {
        return collision.gameObject.TryGetComponent<Enemy>(out _);
	
	}
}