using Kit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public class Teleporter : MonoBehaviour
	{
		public Vector3 Destination;
		public bool ChangeGravity = false;
		[ShowIf("ChangeGravity")]
		public float NewGravity = 0;
		public AudioClip TeleportSound;

		protected void OnTriggerEnter(Collider other)
		{
			Player player = other.GetComponent<Player>();
			if (player == null)
				return;

			player.transform.position = Destination;
			if (ChangeGravity)
				player.ChangeGravity(Quaternion.Euler(0, 0, NewGravity));
			AudioManager.PlaySound(TeleportSound);
		}
	}
}
