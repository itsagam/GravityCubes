using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Obstacle: MonoBehaviour
	{
		public float Position;
		public float Gravity;
		//public Rotations Rotation;
		public List<FlipDirection> Flips;
	}
}