using UnityEngine;

namespace Game
{
	/*
	public enum Rotations
	{
		All0,
		X90,
		Y90,
		Z90,
		X90Y90,
		Y90Z90,
	}
	*/

	public class AIPlayer: Player
	{
		public float StrafeDistance = 1.0f;

		private int obstacleIndex = 0;
		private int flipIndex = 0;

		protected override void Awake()
		{
			base.Awake();
			LevelManager.Instance.AIPlayers.Add(this);
		}

		protected override void Update()
		{
			base.Update();

			if (obstacleIndex >= LevelManager.Instance.Obstacles.Length)
				return;

			//Vector3 position = transform.rotation * transform.position;
			Vector3 position = Gravity * transform.position;
			Obstacle obstacle = LevelManager.Instance.Obstacles[obstacleIndex];

			// Check for when to start strafing
			if (position.z < obstacle.transform.position.z + StrafeDistance)
			{
				float gravity = Gravity.eulerAngles.z;
				float xDiff;
				if (gravity == 270)
					xDiff = position.x + obstacle.Position;
				else
					xDiff = position.x - obstacle.Position;

				// Move into correct gravity
				if (gravity != obstacle.Gravity)
				{
					if (IsFlipping)
						return;

					FlipDirection direction = GetStrafeDirection(obstacle, gravity, position.x);
					if (CanFlipGravity(direction))
						Flip(direction);
					else
					{
						if (direction == FlipDirection.Left)
							StrafeLeft();
						else
							StrafeRight();
					}
				}
				// Strafe till it aligns with horizontal target
				else if (Mathf.Abs(xDiff) > StrafeSpeed * Time.deltaTime)
				{
					if (xDiff > 0)
						StrafeLeft();
					else
						StrafeRight();
				}
				// Execute flips
				else if (flipIndex < obstacle.Flips.Count)
				{
					if (IsFlipping)
						return;

					Flip(obstacle.Flips[flipIndex]);
					flipIndex++;
				}
			}
			else
			{
				obstacleIndex++;
				flipIndex = 0;
			}
		}

		private static FlipDirection GetStrafeDirection(Obstacle obstacle, float gravity, float xPos)
		{
			// Wrap around direction
			if (gravity == 270 && obstacle.Gravity == 0)
				return FlipDirection.Right;
			else if (gravity == 0 && obstacle.Gravity == 270)
				return FlipDirection.Left;
			// Edge cases for quicker movement
			else if (gravity == 0 && obstacle.Gravity == 180 && (xPos < 0 && obstacle.Position > 0))
				return FlipDirection.Left;
			else if (gravity == 90 && obstacle.Gravity == 270 && (xPos < 0 && obstacle.Position < 0))
				return FlipDirection.Left;
			// Otherwise right for greater gravity angle, left for lesser
			else
				return obstacle.Gravity > gravity ? FlipDirection.Right : FlipDirection.Left;
		}
	}
}