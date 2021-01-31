using System.Collections.Generic;
using Kit;
using UnityEngine;

namespace Game
{
	public class Obstacle: MonoBehaviour
	{
		public float Position;
		public float Gravity;
		//public Rotations Rotation;
		public List<FlipDirection> Flips;

		public void Activate(float delay)
		{
			ControlHelper.Delay(delay, Activate);
		}

		public void Activate()
		{
			gameObject.SetActive(true);
			LevelManager.Instance.AddObstacle(this);
		}

		public void Deactivate(float delay)
		{
			ControlHelper.Delay(delay, Deactivate);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
			LevelManager.Instance.RemoveObstacle(this);
		}
	}
}