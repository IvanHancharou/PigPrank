using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace PigPrank
{
	/// <summary>
	/// Base class for Pig, Dog
	/// </summary>
	public class Unit : MonoBehaviour, IGetDamage
	{
		protected Animator _animator;
		protected float _speed;
		protected Transform _thisTransform;
		protected MoveDirections _dir;
		protected Vector3 _targetPos = Vector3.zero;
		protected Node _curNode;
		protected Node _previousNode;
		protected BoxCollider2D _coll;
		protected float _startCollX;
		private readonly float _moveUpAndDownLimit = 0.9f;
		protected SpriteRenderer _sr;
		public Transform ThisTransform => _thisTransform;
		public Node CurNode => _curNode;

		/// <summary>
		/// Set the starting position for unit in scene
		/// </summary>
		/// <param name="startNode"></param>
		public void SetStartPos(Node startNode)
		{
			_thisTransform = transform;
			_thisTransform.position = startNode.ThisTransform.position;
			_curNode = startNode;
			_previousNode = _curNode;
		}

		/// <summary>
		/// Get next target pos for AI units
		/// </summary>
		/// <returns></returns>
		public void GetNextTargetPos()
		{
			Node targetNode;
			System.Random rand = new System.Random();
			IEnumerable<Node> targetNodes = _curNode.AvailablePath.Where(n => n.IsFree && n.GetHashCode() != _previousNode.GetHashCode());
			if (!targetNodes.Any())
			{
				targetNode = _curNode.AvailablePath.First(n => n.IsFree);
			}
			else
			{
				targetNode = targetNodes.ElementAt(rand.Next(targetNodes.Count()));
			}
			
			_previousNode = _curNode;
			_curNode.SetIsFreeNode(true);
			targetNode.SetIsFreeNode(false);
			_curNode = targetNode;
			_targetPos = _curNode.ThisTransform.position;
			Vector2 t = (_targetPos - _thisTransform.position).normalized;
			if (t == Vector2.right)
			{
				MoveRight();
			}
			else if (t == -Vector2.right)
			{
				MoveLeft();
			}
			else if (t[1] > _moveUpAndDownLimit)
			{
				MoveTop();
			}
			else if (t[1] < -_moveUpAndDownLimit)
			{
				MoveDown();
			}
		}

		/// <summary>
		/// First reset all animator triggers than activate actual animation
		/// </summary>
		public void SetAnimatorTriggers()
		{
			foreach (var trigger in _animator.parameters)
			{
				if (trigger.type == AnimatorControllerParameterType.Bool)
				{
					_animator.ResetTrigger(trigger.name);
				}
			}

			_animator.SetBool(_dir.ToString(), true);
		}

		/// <summary>
		/// Depending on the direction of the object change its collider size 
		/// </summary>
		/// <param name="i">Increase or decrease by 2</param>
		public void UpdateCollider(float i)
		{
			if ((_startCollX == _coll.size.x && i < 2) || (_startCollX > _coll.size.x && i == 2))
			{
				_coll.size = new Vector2(_coll.size.x * i, _coll.size.y);
			}
		}

		/// <summary>
		/// Move Ai units from one node to another
		/// </summary>
		public void MoveAIUnit()
		{
			_thisTransform.position = Vector3.MoveTowards(
				_thisTransform.position,
				_targetPos,
				_speed * Time.deltaTime);
			if (_thisTransform.position == _targetPos)
			{				
				_targetPos = Vector3.zero;
				GetNextTargetPos();
			}
		}

		/// <summary>
		/// Bomb explodes or dogs bite
		/// </summary>
		public void GetDamage()
		{
			this.DestroyObj();
		}		

		public virtual void MoveRight() { }
		public virtual void MoveLeft() { }
		public virtual void MoveTop() { }
		public virtual void MoveDown() { }
		public virtual void DestroyObj() { }
	}
}