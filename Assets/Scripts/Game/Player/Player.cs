using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public enum FlipDirection
	{
		Left,
		Right,
		Forward,
		Backward
	}

	public abstract class Player: MonoBehaviour
	{
		public float MoveSpeed = 10.0f;
		public float MoveTime = 2.0f;
		public float SlideSpeed = 10.0f;
		public float SlideTime = 0.5f;

		public Vector3 StartPosition { get; private set; }
		public Bounds Bounds { get; private set; }
		public bool IsMoving { get; private set; }

		protected new Transform transform;
		protected new BoxCollider collider;
		protected new Rigidbody rigidbody;
		protected float moveSpeed = 0;

		protected virtual void Awake()
		{
			transform = base.transform;
			collider = GetComponent<BoxCollider>();
			rigidbody = GetComponent<Rigidbody>();
			Bounds = collider.bounds;
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
			transform.Translate(Vector3.left * (movement * Time.deltaTime), Space.World);
		}

		public void Flip(FlipDirection direction)
		{
			float angle = 90.0f;
			Vector3 vector = Vector3.zero;
			switch (direction)
			{
				case FlipDirection.Left:
					vector = new Vector3(0, 0, angle);
					break;

				case FlipDirection.Right:
					vector = new Vector3(0, 0, -angle);
					break;

				case FlipDirection.Forward:
					vector = new Vector3(angle, 0, 0);
					break;

				case FlipDirection.Backward:
					vector = new Vector3(-angle, 0, 0);
					break;
			}

			Vector3 extents = Quaternion.Euler(vector) * transform.rotation * Bounds.extents;
			transform.DOMoveY(Mathf.Abs(extents.y), SlideTime);
			transform.DOBlendableRotateBy(vector, SlideTime);
		}

		protected virtual void Update()
		{
			if (moveSpeed > 0)
				transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime), Space.World);
		}
	}
}