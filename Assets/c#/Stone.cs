using UnityEngine;

namespace PigPrank
{
	/// <summary>
	/// Obstacle for moving around the map
	/// can explode
	/// </summary>
	public class Stone : MonoBehaviour, IGetDamage
	{
		private Node _curNode;
		private int _layer;
		private Transform _thisTransform;
		public Transform ThisTransform => _thisTransform;
		public int Layer => _layer;

		void Start()
		{
			_thisTransform = transform;
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.sortingOrder = _layer;
			transform.position = _curNode.ThisTransform.position;
		}

		public void GetDamage()
		{
			gameObject.SetActive(false);
			_curNode.SetIsFreeNode(true);
		}

		public void SetCurNode(Node curNode)
		{
			_curNode = curNode;
		}

		public void SetLayer(int layer)
		{
			_layer = layer;
		}
	}
}