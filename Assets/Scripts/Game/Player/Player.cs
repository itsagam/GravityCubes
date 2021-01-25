using DG.Tweening;
using UnityEngine;

namespace Game
{
	public abstract class Player: MonoBehaviour
	{
		public float MoveSpeed = 10.0f;
		public float MoveTime = 2.0f;
		public float SlideSpeed = 10.0f;

		public Vector3 StartPosition { get; private set; }
		public bool IsMoving { get; private set; }

		protected new Transform transform;
		protected float moveSpeed = 0;

		protected virtual void Awake()
		{
			transform = base.transform;
			StartPosition = transform.position;
		}

		public void StartMoving()
		{
			IsMoving = true;
			DOTween.To(value => moveSpeed = value, 0, MoveSpeed, MoveTime);
		}

		public void StopMoving()
		{
			IsMoving = false;
			DOTween.To(value => moveSpeed = value, moveSpeed, 0, MoveTime);
		}

		public void SlideLeft()
		{
			Slide(SlideSpeed);
		}

		public void SlideRight()
		{
			Slide(-SlideSpeed);
		}

		public void Slide(float movement)
		{
			if (IsMoving)
				transform.Translate(Vector3.left * (movement * Time.deltaTime));
		}

		protected virtual void Update()
		{
			if (moveSpeed > 0)
				transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
		}
	}
}