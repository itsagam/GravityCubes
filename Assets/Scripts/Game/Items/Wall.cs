using Game;
using Kit;
using UnityEngine;

namespace Game
{
	public class Wall : MonoBehaviour
	{
		public float RespawnDistance = 5.0f;

		private void OnTriggerEnter(Collider other)
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				player.Flip(FlipDirection.Backward);
				player.StopMovingImmediate();
				ControlHelper.Delay(player.MoveTime,
									() =>
									{
										Transform playerTransform = player.transform;
										playerTransform.rotation = Quaternion.identity;
										playerTransform.position = player.transform.position.AddZ(RespawnDistance);
										player.StartMoving();
									});
			}
		}
	}
}
