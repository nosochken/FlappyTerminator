using System.Net.Http.Headers;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet/Create new BulletData", order = 51)]
public class BulletData : ScriptableObject
{
	[SerializeField] private Sprite _sprite;
	[SerializeField] Vector3 _scale;
	
	public Sprite Sprite => _sprite;
	public Vector3 Scale => _scale;
}