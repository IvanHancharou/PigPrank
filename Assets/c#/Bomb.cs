using UnityEngine;
using System.Collections;

namespace PigPrank
{
	/// <summary>
	/// Class Bomb control
	/// </summary>
	public class Bomb : MonoBehaviour
	{
		[SerializeField] private GameObject _radiusImg;
		private readonly float _waitTime = 5f;
		private readonly float _scaleSpeed = 0.5f;
		private bool _isUpBtn = false;
		private bool _scaleUpBtn => _thisTransform.localScale.x < 1.5f && _isUpBtn;
		private bool _scaleDownBtn => _thisTransform.localScale.x > 1f && !_isUpBtn;
		private Transform _thisTransform;
		private SpriteRenderer _sr;

		internal void ActivateBomb()
		{
			_thisTransform = transform;
			_thisTransform.position = GameManager.Ins.Player.ThisTransform.position;
			StartCoroutine(Explosion());
			_sr = _radiusImg.GetComponent<SpriteRenderer>();
			_thisTransform.DetachChildren();
		}

		/// <summary>
		/// Gradually changing the color
		/// </summary>
		void Update()
		{
			_sr.color = Color.Lerp(_sr.color, new Color(1f, 0, 0, 0.8f), Time.deltaTime / _waitTime);
			ScaleBtn();
		}

		IEnumerator Explosion()
		{
			yield return new WaitForSeconds(_waitTime);
			float radius = _radiusImg.GetComponent<Renderer>().bounds.size.x * 0.5f;
			ExplosionDamage(radius);
		}

		/// <summary>
		/// The effect of changing the scale of the bomb
		/// </summary>
		void ScaleBtn()
		{
			if (_scaleDownBtn)
			{
				_thisTransform.localScale = new Vector2(
					_thisTransform.localScale.x - _scaleSpeed * Time.deltaTime,
					_thisTransform.localScale.y - _scaleSpeed * Time.deltaTime
					);
			}
			else
			{
				_isUpBtn = true;
			}

			if (_scaleUpBtn)
			{
				_thisTransform.localScale = new Vector2(
					_thisTransform.localScale.x + _scaleSpeed * Time.deltaTime,
					_thisTransform.localScale.y + _scaleSpeed * Time.deltaTime
					);
			}
			else
			{
				_isUpBtn = false;
			}
		}

		/// <summary>
		/// When the bomb exploded, look for nearby objects to destroy them
		/// </summary>
		/// <param name="radius"></param>
		void ExplosionDamage(float radius)
		{
			_radiusImg.transform.SetParent(_thisTransform);
			gameObject.SetActive(false);			
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_thisTransform.position, radius);
			foreach (Collider2D hitCollider in hitColliders)
			{
				IGetDamage obj = hitCollider.GetComponent(typeof(IGetDamage)) as IGetDamage;
				if (obj != null)
					obj.GetDamage();
			}
		}
	}
}