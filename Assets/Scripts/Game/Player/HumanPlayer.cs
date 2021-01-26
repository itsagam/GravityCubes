using System;
using DG.Tweening;
using Kit;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

namespace Game
{
	public class HumanPlayer: Player
	{
		public AudioClip FlipSound;
		public AudioClip FlipGravitySound;

		public FlickGesture FlickGesture;
		public ScreenTransformGesture TransformGesture;

		protected override void Awake()
		{
			base.Awake();
			TransformGesture.Transformed += OnTransformed;
			FlickGesture.Flicked += OnFlicked;
		}

		protected void OnTransformed(object sender, EventArgs e)
		{
			if (! CanInput)
				return;

			if (TransformGesture.DeltaPosition.x > 0)
				SlideRight();
			else if (TransformGesture.DeltaPosition.x < 0)
				SlideLeft();
		}

		protected void OnFlicked(object sender, EventArgs e)
		{
			if (! CanInput)
				return;

			if (Mathf.Abs(FlickGesture.ScreenFlickVector.x) > Mathf.Abs(FlickGesture.ScreenFlickVector.y))
			{
				if (FlickGesture.ScreenFlickVector.x >= 0)
					Flip(FlipDirection.Right);
				else
					Flip(FlipDirection.Left);
			}
			else
			{
				if (FlickGesture.ScreenFlickVector.y >= 0)
					Flip(FlipDirection.Forward);
				else
					Flip(FlipDirection.Backward);
			}
		}

		protected override void Update()
		{
			HandleInput();
			base.Update();
		}

		protected virtual void HandleInput()
		{
			if (! CanInput)
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

		public override void Flip(FlipDirection direction)
		{
			base.Flip(direction);
			AudioManager.Play(FlipSound);
		}

		protected override void FlipGravity(Vector3 rotation)
		{
			base.FlipGravity(rotation);
			LevelManager.Instance.Camera.transform.DORotate(Gravity.eulerAngles, FlipTime);
			AudioManager.Play(FlipGravitySound);
		}

		public bool CanInput => LevelManager.Instance.State == LevelState.Playing && IsMoving;
	}
}