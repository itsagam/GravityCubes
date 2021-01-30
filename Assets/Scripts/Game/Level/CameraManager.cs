using UnityEngine;

namespace Game
{
	public class CameraManager: MonoBehaviour
	{
		public float Distance = 10.0f;
		public float MoveSpeed = 10.0f;

		private new Transform transform;
		private Player player;
		private Transform playerTransform;

		private void Awake()
		{
			transform = base.transform;
			player = LevelManager.Instance.AIPlayers[0];
			//player = LevelManager.Instance.HumanPlayer;
			playerTransform = player.transform;
			transform.position = GetNextPosition;
		}

		private void LateUpdate()
		{
			Vector3 newPosition = GetNextPosition;
			transform.position = Vector3.Lerp(transform.position, newPosition, MoveSpeed * Time.deltaTime);
		}

		private Vector3 GetNextPosition
		{
			get
			{
				Vector3 playerPosition = playerTransform.position;
				float gravity = player.Gravity.eulerAngles.z;
				Vector3 nextPosition = Vector3.zero;
				if (gravity == 0)
					nextPosition = new Vector3(playerPosition.x, 0.5f);
				else if (gravity == 180)
					nextPosition = new Vector3(playerPosition.x, -0.5f);
				else if (gravity == 90)
					nextPosition = new Vector3(-0.5f, playerPosition.y);
				else if (gravity == 270)
					nextPosition = new Vector3(0.5f, playerPosition.y);
				nextPosition.z = playerPosition.z - Distance;
				return nextPosition;
			}
		}
	}
}