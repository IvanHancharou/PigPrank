using UnityEngine;

namespace PigPrank
{
	/// <summary>
	/// Dog management class 
	/// </summary>
	public class Dog : Unit
	{
        void Start()
		{
			_speed = 1f;
			_animator = GetComponent<Animator>();
			_coll = GetComponent<BoxCollider2D>();
			_startCollX = _coll.size.x;
			GetNextTargetPos();
		}

		/// <summary>
		/// Dog bites a pig
		/// </summary>
		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				GameManager.Ins.Player.DestroyObj();
			}
		}

		/// <summary>
		/// Dog movement
		/// </summary>
		void Update()
		{
			if (_targetPos != Vector3.zero)
				MoveAIUnit();
		}

		/// <summary>
		/// The following four overridden methods:
		/// move in a given direction, reset and set animation triggers,
		/// change the size of the collider when turning
		/// </summary>
		public override void MoveRight()
		{
			_dir = MoveDirections.goRight;
			SetAnimatorTriggers();
			UpdateCollider(2);
		}

		public override void MoveLeft()
		{
			_dir = MoveDirections.goLeft;
			SetAnimatorTriggers();
			UpdateCollider(2);
		}

		public override void MoveTop()
		{
			_dir = MoveDirections.goUp;
			SetAnimatorTriggers();
			UpdateCollider(0.5f);
		}

		public override void MoveDown()
		{
			_dir = MoveDirections.goDown;
			SetAnimatorTriggers();
			UpdateCollider(0.5f);
		}

		/// <summary>
		/// Dog got stuck on a bomb
		/// </summary>
		public override void DestroyObj()
		{
			gameObject.SetActive(false);
			_curNode.SetIsFreeNode(true);
		}
	}
}