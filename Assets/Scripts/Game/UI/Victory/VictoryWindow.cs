using Game.UI.Menu;
using Kit;
using Kit.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Victory
{
	public class VictoryWindow: Window
	{
		public Text WinnerText;
		public UnityEngine.UI.Button NextButton;
		public Text NextText;
		public AudioClip WinSound;
		public AudioClip LoseSound;

		protected override void Awake()
		{
			base.Awake();
			NextButton.onClick.AddListener(Next);
		}

		protected override void OnShowing()
		{
			if (HasWon)
			{
				AudioManager.PlaySound(WinSound);
				WinnerText.text = "YOU WIN!";

				int index = SceneDirector.ActiveIndex + 1;
				if (index < SceneDirector.TotalScenes)
					NextText.text = $"ROUND {index - PlayButton.StartingIndex}";
				else
					NextText.text = "FINISH";
			}
			else
			{
				AudioManager.PlaySound(LoseSound);
				NextText.text = "RETRY";
				WinnerText.text = "YOU LOST...";
			}
		}

		private void Next()
		{
			int index = SceneDirector.ActiveIndex;
			if (HasWon)
			{
				index++;
				SettingsManager.Set("Levels", "Player", "CurrentLevel", index);
				SettingsManager.Save();
			}

			PlayButton.Play(index);
		}

		public bool HasWon => Winner is HumanPlayer;
		public Player Winner => (Player) Data;
	}
}