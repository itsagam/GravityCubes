using Kit;
using UnityEngine;

namespace Game.UI.Game
{
	public class PlayerProgress: MonoBehaviour
	{
		public Player Player;

		private new RectTransform transform;
		private RectTransform parent;
		private float startX, endX;

		private void Awake()
		{
			transform = GetComponent<RectTransform>();
			parent = (RectTransform) transform.parent;
			startX = Player.StartPosition.z;
			endX = LevelManager.Instance.LevelEnd.transform.position.z;
		}

		private void Update()
		{
			if (!Player.IsMoving)
				return;

			float newX = MathHelper.Map(Player.transform.position.z,
										startX, endX,
										0, parent.rect.width);
			transform.anchoredPosition = transform.anchoredPosition.SetX(newX);
		}
	}
}