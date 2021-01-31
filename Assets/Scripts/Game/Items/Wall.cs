using DG.Tweening;
using Game;
using Kit;
using UnityEngine;

namespace Game
{
	public class Wall : MonoBehaviour
	{
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
				ControlHelper.Delay(player.RespawnDelay,
									() =>
									{
										playerTransform.rotation = previousRotation;
										playerTransform.position = playerTransform.position.AddZ(player.RespawnDistance);
										player.StartMoving();
									});
			}
		}
	}
}
