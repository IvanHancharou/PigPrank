using UnityEngine;

namespace PigPrank
{
	/// <summary>
	/// Our character management class
	/// </summary>
	public class Pig : Unit
	{
		private float _vert;
		private float _hor;

		void Start()
		{			
			_speed = 3f;
			_animator = GetComponent<Animator>();
			_coll = GetComponent<BoxCollider2D>();
			_startCollX = _coll.size.x;
			_sr = GetComponent<SpriteRenderer>();
		}
		
		void Update()
		{
			if (_vert != 0 || _hor != 0)
			{
				_thisTransform.Translate(_hor * _speed * Time.deltaTime, _vert * _speed * Time.deltaTime, 0);
			}
		}

		/// <summary>
		/// The following four overridden methods:
		/// move in a given direction, reset and set animation triggers,
		/// change the size of the collider when turning
		/// </summary>
		public override void MoveRight()
		{
			_hor = 1;
			_vert = 0;			
			_dir = MoveDirections.goRight;
			SetAnimatorTriggers();
			UpdateCollider(2);
		}

		public override void MoveLeft()
		{
			_hor = -1;
			_vert = 0;			
			_dir = MoveDirections.goLeft;
			SetAnimatorTriggers();
			UpdateCollider(2);
		}

		public override void MoveTop()
		{
			_hor = 0;			
			_vert = 1;
			_dir = MoveDirections.goUp;
			SetAnimatorTriggers();
			UpdateCollider(0.5f);
		}

		public override void MoveDown()
		{
			_hor = 0;			
			_vert = -1;
			_dir = MoveDirections.goDown;
			SetAnimatorTriggers();
			UpdateCollider(0.5f);
		}

		/// <summary>
		/// Stop the pig if none of the dir buttons are pressed
		/// </summary>
		public void Stop()
		{
			_vert = 0;
			_hor = 0;
		}

		/// <summary>
		/// A pig explodes on a bomb or gets bitten by a dog
		/// </summary>
		public override void DestroyObj()
		{
			gameObject.SetActive(false);
			GameManager.Ins.GameEnd();
		}

		/// <summary>
		/// When hitting a stone, change the character sorting layer
		/// So that the character is correctly drawn in front of or behind a stone
		/// </summary>
		/// <param name="stonePosY"></param>
		/// <param name="stoneLayer"></param>
		public void ChangeSortingLayer(float stonePosY, int stoneLayer)
		{
			if (_thisTransform.position.y < stonePosY)
			{
				_sr.sortingOrder = stoneLayer + 1;
			}
			else
			{
				_sr.sortingOrder = stoneLayer - 1;
			}
		}
	}
}