using UnityEngine;

namespace Game
{
	public class EndVolume: MonoBehaviour
	{
		public BoxCollider Collider { get; protected set; }
		public Bounds Bounds { get; private set; }

		private void Awake()
		{
			Collider = GetComponent<BoxCollider>();
			Bounds = Collider.bounds;
		}

		private void OnTriggerEnter(Collider other)
		{
			Player player = other.GetComponent<Player>();
			if (player == null)
				return;

			player.StopMoving();
			if (player is HumanPlayer)
				LevelManager.Instance.EndLevel();
		}
	}
}