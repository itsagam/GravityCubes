using Cysharp.Threading.Tasks;
using Game.UI.Menu;
using Kit;
using Kit.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI.Victory
{
	public class VictoryWindow: Window
	{
		public Text WinnerText;
		public UnityEngine.UI.Button NextButton;
		public Text NextText;

		protected override void Awake()
		{
			base.Awake();
			NextButton.onClick.AddListener(Next);
		}

		protected override void OnShowing()
		{
			if (HasWon)
			{
				WinnerText.text = "You win!";

				int index = SceneDirector.ActiveIndex + 1;
				if (index < SceneDirector.TotalScenes)
					NextText.text = $"Round {index}";
				else
					NextText.text = "Finish";
			}
			else
			{
				NextText.text = "Retry";
				WinnerText.text = "You lost...";
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