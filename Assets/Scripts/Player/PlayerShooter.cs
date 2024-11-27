using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
   public event Action Shooting;
   
   public Vector3 BulletPosition => transform.position;
   
   public void Shoot()
   {
		Shooting?.Invoke();
   }
}