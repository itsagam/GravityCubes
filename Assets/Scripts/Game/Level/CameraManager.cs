using UnityEngine;

namespace Game
{
	public class CameraManager: MonoBehaviour
	{
		public float Distance = 10.0f;
		public float MoveSpeed = 10.0f;

		private new Transform transform;
		private Transform player;

		private void Awake()
		{
			transform = base.transform;
			player = LevelManager.Instance.HumanPlayer.transform;
			transform.position = player.position - transform.forward * Distance;
		}

		private void LateUpdate()
		{
			Vector3 newPosition = player.position - transform.forward * Distance;
			transform.position = Vector3.Lerp(transform.position, newPosition, MoveSpeed * Time.deltaTime);
		}
	}
}