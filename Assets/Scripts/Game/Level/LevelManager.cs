using System.Collections.Generic;
using System.Linq;
using Kit;
using UnityEngine;

namespace Game
{
	public enum LevelState
	{
		Waiting,
		Playing,
		Ended
	}

	public class LevelManager: Singleton<LevelManager>
	{
		public CameraManager Camera;
		public HumanPlayer HumanPlayer;
		public AIPlayer[] AIPlayers;
		public float EndDelay = 2.0f;
		public EndVolume LevelEnd;
		public AudioClip Music;

		public LevelState State = LevelState.Waiting;

		private void Start()
		{
			StartLevel();
		}

		public void StartLevel()
		{
			if (State != LevelState.Waiting)
				return;

			AudioManager.PlayMusic(Music);
			foreach (Player player in AllPlayers)
				player.StartMoving();
			State = LevelState.Playing;
		}

		public void EndLevel()
		{
			if (State != LevelState.Playing)
				return;

			foreach (Player player in AllPlayers)
				player.StopMoving();
			State = LevelState.Ended;
			ControlHelper.Delay(EndDelay, () => SceneDirector.ReloadScene());
		}

		public IEnumerable<Player> AllPlayers => AIPlayers.Union(EnumerableExtensions.One((Player) HumanPlayer));
	}
}