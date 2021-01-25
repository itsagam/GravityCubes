using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
	public class HumanPlayer: Player
	{
		protected override void Update()
		{
			HandleInput();
			base.Update();
		}

		protected virtual void HandleInput()
		{
			if (LevelManager.Instance.State != LevelState.Playing)
				return;

#if UNITY_EDITOR
			if (Input.GetKey(KeyCode.A))
				SlideLeft();

			if (Input.GetKey(KeyCode.D))
				SlideRight();

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (Input.GetKey(KeyCode.A))
					Flip(FlipDirection.Left);

				else if (Input.GetKey(KeyCode.D))
					Flip(FlipDirection.Right);

				else if (Input.GetKey(KeyCode.W))
					Flip(FlipDirection.Forward);

				else if (Input.GetKey(KeyCode.S))
					Flip(FlipDirection.Backward);
			}
#endif
		}

		protected override void FlipGravity(FlipDirection direction)
		{
			base.FlipGravity(direction);
			Vector3 rotation = Vector3.zero;
			
			if (direction == FlipDirection.Left)
				rotation = new Vector3(0, 0, -90);
			else if (direction == FlipDirection.Right)
				rotation = new Vector3(0, 0, 90);

			LevelManager.Instance.Camera.transform.DOBlendableRotateBy(rotation, FlipTime);
		}
	}
}