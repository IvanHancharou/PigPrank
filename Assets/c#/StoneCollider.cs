using UnityEngine;

namespace PigPrank
{
	/// <summary>
	/// Changes the sorting layer of the pig when it collides
	/// </summary>
	public class StoneCollider : MonoBehaviour
	{
		[SerializeField] private Stone _stone;

		//Corresponds to the row number by y for Node
		private int _stoneLayer;

		void Start()
		{
			_stoneLayer = _stone.Layer;
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				other.GetComponent<Pig>().ChangeSortingLayer(_stone.ThisTransform.position.y, _stoneLayer);
			}
		}
	}
}