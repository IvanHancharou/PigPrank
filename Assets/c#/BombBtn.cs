using UnityEngine;
using UnityEngine.UI;

namespace PigPrank
{
	/// <summary>
	/// Ui bomb button
	/// </summary>
	public class BombBtn : MonoBehaviour
	{
		[SerializeField] private Image hideImage;
		private Transform _thisTransform;
		private Button btn;
		private readonly float waitTime = 2f;
		private bool _wait;
		private bool _isUpBtn = false;
		private bool _scaleUpBtn => _thisTransform.localScale.x < 0.9f && _isUpBtn;
		private bool _scaleDownBtn => _thisTransform.localScale.x > 0.5f && !_isUpBtn;
		private readonly float _scaleSpeed = 0.5f;

		void Start()
		{
			_thisTransform = transform;
			btn = GetComponent<Button>();
		}

		/// <summary>
		/// If the bomb put, than wait deactivate the bomb button and graphically display
		/// the waiting time before activating the button
		/// </summary>
		void Update()
		{
			if (_wait)
			{
				hideImage.fillAmount -= Time.deltaTime / waitTime;
				if (hideImage.fillAmount == 0)
				{
					_wait = false;
					btn.interactable = true;
				}
			}
			else 
			{
				ScaleBtn();
			}
		}

		/// <summary>
		/// Put bomb, deactivate bomb button and running the standby time
		/// </summary>
		public void PutBomb()
		{
			if (!_wait)
			{
				GameObject obj = GameManager.Ins.Pool.Spawn("bomb");
				btn.interactable = false;
				hideImage.fillAmount = 1;
				_wait = true;
				obj.GetComponent<Bomb>().ActivateBomb();
			}
		}

		/// <summary>
		/// The effect of changing the scale of the button
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
	}
}