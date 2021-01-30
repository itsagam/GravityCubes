using Kit;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game
{
	public class PlayerProgress: MonoBehaviour
	{
		public Player Player;

		private new RectTransform transform;
		private RectTransform parent;
		private float startZ, endZ;

		private void Start()
		{
			transform = GetComponent<RectTransform>();
			parent = (RectTransform) transform.parent;
			startZ = Player.StartPosition.z;
			endZ = LevelManager.Instance.LevelEnd.transform.position.z;
		}

		private void Update()
		{
			if (LevelManager.Instance.State != LevelState.Playing || !Player.IsMoving)
				return;

			float newX = MathHelper.Map(Player.transform.position.z,
										startZ, endZ,
										0, parent.rect.width);
			transform.anchoredPosition = transform.anchoredPosition.SetX(newX);
		}

		public void SetColor(Color color)
		{
			Image image = GetComponent<Image>();
			image.color = color;
		}
	}
}