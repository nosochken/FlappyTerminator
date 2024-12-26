using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action Shooting;

    public void Shoot()
    {
        Shooting?.Invoke();
    }
}