using System;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;

namespace Kit
{
	/// <summary>Helper functions for events and control flow.</summary>
	public class ControlHelper: MonoBehaviour
	{
		/// <summary>Event fired when the app loses/gains focus.</summary>
		public static event Action<bool> ApplicationFocus;

		/// <summary>Event fired when the app pauses/unpauses.</summary>
		public static event Action<bool> ApplicationPause;

		/// <summary>Event fired when the app quits.</summary>
		public static event Action ApplicationQuit;

		static ControlHelper()
		{
			GameObject go = new GameObject(nameof(ControlHelper));
			go.AddComponent<ControlHelper>();
			DontDestroyOnLoad(go);
		}

		/// <summary>Perform an action next frame.</summary>
		/// <param name="action">Action to perform.</param>
		public static void Delay(Action action)
		{
			Delay(1, action);
		}

		/// <summary>Call a method after specified number of frames.</summary>
		/// <param name="frames">Number of frames to hold out for.</param>
		/// <param name="action">Action to perform.</param>
		public static void Delay(int frames, Action action)
		{
			UniTaskAsyncEnumerable.TimerFrame(frames).ForEachAsync(_ => action());
		}

		/// <summary>Keep calling a method after a specified number of frames.</summary>
		/// <param name="delayFrames">Number of frames to hold out for before calling for the first time.</param>
		/// <param name="intervalFrames">Number of frames to hold out for before calling subsequent times.</param>
		/// <param name="action">Action to perform.</param>
		public static void Delay(int delayFrames, int intervalFrames, Action action)
		{
			UniTaskAsyncEnumerable.TimerFrame(delayFrames, intervalFrames).ForEachAsync(_ => action());
		}

		/// <summary>Call a method after specified number of seconds.</summary>
		/// <param name="seconds">Number of seconds to hold out for.</param>
		/// <param name="action">Action to perform.</param>
		public static void Delay(float seconds, Action action)
		{
			UniTaskAsyncEnumerable.Timer(TimeSpan.FromSeconds(seconds)).ForEachAsync(_ => action());
		}

		/// <summary>Keep calling a method after a specified number of seconds.</summary>
		/// <param name="delaySeconds">Number of seconds to hold out for before calling for the first time.</param>
		/// <param name="intervalSeconds">Number of seconds to hold out for before calling subsequent times.</param>
		/// <param name="action">Action to perform.</param>
		public static void Delay(float delaySeconds, float intervalSeconds, Action action)
		{
			UniTaskAsyncEnumerable.Timer(TimeSpan.FromSeconds(delaySeconds), TimeSpan.FromSeconds(intervalSeconds)).ForEachAsync(_ => action());
		}

		/// <summary>Call a method after a specified condition is satisfied.</summary>
		/// <param name="until">Function</param>
		/// <param name="action">Action to perform.</param>
		public static void Delay(Func<bool> until, Action action)
		{
			UniTaskAsyncEnumerable.EveryUpdate().SkipWhile(_ => !until()).ForEachAsync(_ => action());
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			ApplicationFocus?.Invoke(hasFocus);
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			ApplicationPause?.Invoke(pauseStatus);
		}

		private void OnApplicationQuit()
		{
			ApplicationQuit?.Invoke();
		}
	}
}