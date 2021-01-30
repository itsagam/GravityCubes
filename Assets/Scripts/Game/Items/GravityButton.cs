using UnityEngine;

namespace Game
{
	public class GravityButton: MonoBehaviour
	{
		public void Switch()
		{
			Quaternion newGravity = Quaternion.Euler(0, 0, transform.eulerAngles.z);
			foreach (Player player in LevelManager.Instance.AllPlayers)
				player.ChangeGravity(newGravity);
		}
	}
}