using DG.Tweening;
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
				Transform playerTransform = player.transform;
				playerTransform.DOKill(true);
				Quaternion previousRotation = playerTransform.rotation;
				player.Flip(FlipDirection.Backward);
				player.StopMovingImmediate();
				playerTransform.DOBlendableMoveBy(new Vector3(0, 0, -1.5f), player.FlipTime);
				ControlHelper.Delay(player.MoveTime,
									() =>
									{

										playerTransform.rotation = previousRotation;
										playerTransform.position = playerTransform.position.AddZ(RespawnDistance);
										player.StartMoving();
									});
			}
		}
	}
}
