using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BulletView : MonoBehaviour
{
	[SerializeField] private BulletData _bulletData;
	
	private SpriteRenderer _spriteRenderer;
	
	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = _bulletData.Sprite;
		
		transform.localScale = _bulletData.Scale;
	}
}