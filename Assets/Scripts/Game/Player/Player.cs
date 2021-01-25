using DG.Tweening;
using Kit;
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
		public float FlipTime = 0.5f;

		public Quaternion Gravity { get; private set; } = Quaternion.identity;
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
			transform.position += Gravity * Vector3.left * (movement * Time.deltaTime);
		}

		public void Flip(FlipDirection direction)
		{
			if (direction == FlipDirection.Left || direction == FlipDirection.Right)
			{
				if (Mathf.Abs((Gravity * transform.position).x) > 1.9f)
					FlipGravity(direction);
			}

			float angle = 90.0f;
			Vector3 rotation = Vector3.zero;
			switch (direction)
			{
				case FlipDirection.Left:
					rotation = new Vector3(0, 0, angle);
					break;

				case FlipDirection.Right:
					rotation = new Vector3(0, 0, -angle);
					break;

				case FlipDirection.Forward:
					rotation = new Vector3(angle, 0, 0);
					break;

				case FlipDirection.Backward:
					rotation = new Vector3(-angle, 0, 0);
					break;
			}

			Vector3 extents = Quaternion.Euler(rotation) * transform.rotation * Bounds.extents;
			Debug.Log(extents);
			
			transform.DOMoveY(Mathf.Abs(extents.y), FlipTime);
			transform.DOBlendableRotateBy(rotation, FlipTime);
		}

		protected virtual void FlipGravity(FlipDirection direction)
		{
			if (direction == FlipDirection.Left)
				Gravity *= Quaternion.Euler(new Vector3(0, 0, -90));
			else if (direction == FlipDirection.Right)
				Gravity *= Quaternion.Euler(new Vector3(0, 0, 90));
			Debug.Log($"Gravity {direction}: { Gravity.eulerAngles}");
		}

		protected virtual void Update()
		{
			if (moveSpeed > 0)
				transform.position += Vector3.forward * (moveSpeed * Time.deltaTime);
		}
	}
}