using UnityEngine;
using System.Collections.Generic;

namespace PigPrank
{
	/// <summary>
	/// When we start the game, we create a pool of necessary objects
	/// </summary>
	public class ObjectPool : MonoBehaviour
	{

		[System.Serializable]
		public class Pool
		{
			[SerializeField] private string _tag;
			[SerializeField] private int _size;
			[SerializeField] private GameObject _prefab;
			public int Size => _size;
			public string Tag => _tag;
			public GameObject Prefab => _prefab;
		}

		[SerializeField] private Dictionary<string, Queue<GameObject>> poolDict;
		[SerializeField] private List<Pool> pools;

		public void StartPool()
		{
			poolDict = new Dictionary<string, Queue<GameObject>>();
			foreach (Pool p in pools)
			{
				Queue<GameObject> queuePool = new Queue<GameObject>();
				for (int i = 0; i < p.Size; i++)
				{
					GameObject obj = Instantiate(p.Prefab);
					obj.SetActive(false);
					queuePool.Enqueue(obj);
				}
				poolDict.Add(p.Tag, queuePool);
			}
		}

		public GameObject Spawn(string tag)
		{
			if (!poolDict.ContainsKey(tag)) return null;
			GameObject objSpawn = poolDict[tag].Dequeue();
			objSpawn.SetActive(true);
			poolDict[tag].Enqueue(objSpawn);
			return objSpawn;
		}
	}
}