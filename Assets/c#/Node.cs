using UnityEngine;
using System.Collections.Generic;

namespace PigPrank
{
	/// <summary>
	/// The cell of the map on witch there are stones
	/// on witch the dogs walk and set all their initial coordinates
	/// </summary>
	public class Node : MonoBehaviour
	{
		[SerializeField] private IList<Node> _availablePath = new List<Node>();
		private bool _isFree = true;
		private Transform _thisTransform;
		private int _xPos;
		private int _yPos;
		public Transform ThisTransform => _thisTransform;
		public IList<Node> AvailablePath => _availablePath;
		public bool IsFree => _isFree;

		public int PosX => _xPos;
		public int PosY => _yPos;


		void Awake()
		{
			_thisTransform = transform;
		}

		public void SetNodePosition(int x, int y)
		{
			_xPos = x;
			_yPos = y;
		}

		public void SetIsFreeNode(bool value)
		{
			_isFree = value;
		}
	}
}