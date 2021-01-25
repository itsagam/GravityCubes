using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kit;

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
		public HumanPlayer HumanPlayer;
		public AIPlayer[] AIPlayers;
		public float EndDelay = 2.0f;
		public EndVolume LevelEnd;

		public LevelState State = LevelState.Waiting;

		private void Start()
		{
			StartLevel();
		}

		public void StartLevel()
		{
			if (State != LevelState.Waiting)
				return;

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
			ControlHelper.Delay(EndDelay, () => SceneDirector.FadeOut());
		}

		public IEnumerable<Player> AllPlayers => AIPlayers.Union(EnumerableExtensions.One((Player) HumanPlayer));
	}
}