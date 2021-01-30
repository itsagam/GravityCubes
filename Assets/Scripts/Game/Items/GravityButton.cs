using UnityEngine;

namespace Game
{
	public class GravityButton: MonoBehaviour
	{
		public void Switch()
		{
			foreach (Player player in LevelManager.Instance.AllPlayers)
				player.ChangeGravity(Quaternion.Euler(0, 0, transform.eulerAngles.z));
		}
	}
}