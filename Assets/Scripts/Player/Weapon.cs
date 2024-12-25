using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public event Action Shooting;
   
   public Vector3 Position => transform.position;
   
   public void Shoot()
   {
		Shooting?.Invoke();
   }
}