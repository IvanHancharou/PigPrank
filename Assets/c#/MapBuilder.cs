using System.Collections.Generic;
using UnityEngine;

namespace PigPrank
{
	/// <summary>
	/// Set position for all objects on the map: Nodes, Stones, Dogs and Pig
	/// </summary>
	public class MapBuilder : MonoBehaviour
    {
		[SerializeField] private Transform _startPos;
		[SerializeField] private Transform _nodePrefab;
		[SerializeField] private Transform _map;
		readonly private int mapWidth = 17;
		readonly private int mapHeight = 9;
		readonly private float stepX = 1.1f;
		readonly private float stepY = 1f;
		readonly private float offsetX = 0.12f;
		readonly private int[] startPigPos = new int[2] { 0, 4 };
		readonly private IList<int[]> startDogPos = new List<int[]>() { new int[2] { 12, 0 }, new int[2] { 13, 4 }, new int[2] { 15, 8 } };

		/// <summary>
		/// Set position for all objects on the map: Nodes, Stones, Dogs and Pig
		/// For each Node look for neighboring Node. I do it so that dogs on the map
		/// move clearly and do not repeat their path
		/// </summary>
		internal void AddObjectsToScene()
		{
			for (int i = 0; i < mapWidth; i++)
			{
				Node n = null;
				for (int j = 0; j < mapHeight; j++)
				{
					// Add Node to the map
					Transform t = Instantiate(_nodePrefab, _map);
					t.localPosition = new Vector2(
						_startPos.localPosition.x + i * stepX - j * offsetX,
						_startPos.localPosition.y - j * stepY
						);
					Node curNode = t.GetComponent<Node>();

					// vertical neighbor
					if (j != 0 && n != null)
					{
						n.AvailablePath.Add(curNode);
						curNode.AvailablePath.Add(n);
					}

					// horizontal neighbor
					if (i != 0)
					{
						Node pastNode = _map.GetChild(_map.childCount - (mapHeight + 1)).GetComponent<Node>();
						pastNode.AvailablePath.Add(curNode);
						curNode.AvailablePath.Add(pastNode);
					}

					n = curNode;
					n.SetNodePosition(i, j);
					if (startDogPos.Count != 0 && i == startDogPos[0][0] && j == startDogPos[0][1])
					{
						GameObject obj = GameManager.Ins.Pool.Spawn("dog");
						Dog dog = obj.GetComponent<Dog>();
						dog.SetStartPos(n);
						n.SetIsFreeNode(false);
						startDogPos.RemoveAt(0);
					}

					if (n.PosX % 2 != 0 && n.PosY % 2 != 0)
					{
						GameObject obj = GameManager.Ins.Pool.Spawn("stone");
						Stone s = obj.GetComponent<Stone>();
						s.SetLayer(n.PosY);
						s.SetCurNode(n);
						n.SetIsFreeNode(false);
					}

					if (i == startPigPos[0] && j == startPigPos[1])
					{
						GameObject obj = GameManager.Ins.Pool.Spawn("pig");
						Pig pig = obj.GetComponent<Pig>();
						pig.SetStartPos(n);
						GameManager.Ins.SetPigPlayer(pig);
					}
				}
			}
		}
	}
}