using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable S2696

namespace PigPrank
{
	/// <summary>
	/// Class initialize the map and organizes the connections between the types
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private BombBtn _bombBtn;
		[SerializeField] private GameObject _finishPanel;
		[SerializeField] private ObjectPool _pool;
		[SerializeField] private Pig _player;
		[SerializeField] private MapBuilder _mapBuilder;
		private static GameManager _ins;
		public Pig Player => _player;
		public ObjectPool Pool => _pool;
		public static GameManager Ins => _ins;

		void Awake()
		{
			if (_ins == null)
			{
				_ins = this;				
			}
		}

		/// <summary>
		/// Init pool objects and build the map
		/// </summary>
		void Start()
		{
			_pool.StartPool();
			_mapBuilder.AddObjectsToScene();
		}	

		/// <summary>
		/// Event handler of the pressed pig control button MoveRight
		/// </summary>
		public void MoveRight() => _player.MoveRight();

		/// <summary>
		/// Event handler of the pressed pig control button MoveLeft
		/// </summary>
		public void MoveLeft() => _player.MoveLeft();

		/// <summary>
		/// Event handler of the pressed pig control button MoveTop
		/// </summary>
		public void MoveTop() => _player.MoveTop();

		/// <summary>
		/// Event handler of the pressed pig control button MoveDown
		/// </summary>
		public void MoveDown() => _player.MoveDown();

		/// <summary>
		/// Event handler no one pig control button is pressed
		/// </summary>
		public void Stop() => _player.Stop();

		public void GameEnd() => _finishPanel.SetActive(true);

		public void RestartGame() => SceneManager.LoadScene(0);

		public void SetPigPlayer(Pig pig) => _player = pig;
	}
}
