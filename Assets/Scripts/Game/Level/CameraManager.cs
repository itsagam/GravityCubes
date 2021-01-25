using Kit;
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
			transform.position = GetNextPosition;
		}

		private void LateUpdate()
		{
			Vector3 newPosition = GetNextPosition;
			transform.position = Vector3.Lerp(transform.position, newPosition, MoveSpeed * Time.deltaTime);
		}

		private Vector3 GetNextPosition => new Vector3(player.position.x, transform.position.y, player.position.z - Distance);
	}
}