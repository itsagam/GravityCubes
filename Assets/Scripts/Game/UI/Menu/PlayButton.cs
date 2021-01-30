using Cysharp.Threading.Tasks;
using Kit;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI.Menu
{
	public class PlayButton: MonoBehaviour
	{
		public int StartingIndex = 2;

		private void Awake()
		{
			UnityEngine.UI.Button button = GetComponent<UnityEngine.UI.Button>();
			button.onClick.AddListener(() =>
									   {
										   int index = SettingsManager.Get("Levels", "Player", "CurrentLevel", StartingIndex);
										   Play(index);
									   });
		}

		public static void Play(int sceneIndex)
		{
			if (sceneIndex < SceneDirector.TotalScenes)
				LoadScene(sceneIndex).Forget();
			else
#if UNITY_EDITOR
				SceneDirector.FadeOut(onComplete: () => EditorApplication.isPlaying = false);
#else
				SceneDirector.FadeOut(onComplete: Application.Quit);
#endif
		}

		public static async UniTaskVoid LoadScene(int index)
		{
			await SceneDirector.FadeOut();
			await SceneManager.LoadSceneAsync(index);
			await SceneDirector.FadeIn();
		}
	}
}