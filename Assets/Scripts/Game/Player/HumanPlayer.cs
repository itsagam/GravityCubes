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

		protected override void FlipGravity(Vector3 rotation)
		{
			base.FlipGravity(rotation);
			LevelManager.Instance.Camera.transform.DORotate(Gravity.eulerAngles, FlipTime);
		}
	}
}