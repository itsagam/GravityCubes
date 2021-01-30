using DG.Tweening;
using Game.UI.Game;
using Kit;
using UnityEngine;

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
		public float StrafeSpeed = 10.0f;
		public float FlipTime = 0.5f;
		public float FlipDistance = 1.0f;
		public PlayerProgress ProgressPrefab;
		public Color ProgressColor = Color.blue;

		public Quaternion Gravity { get; private set; } = Quaternion.identity;
		public Vector3 StartPosition { get; private set; }
		public Bounds Bounds { get; private set; }
		public bool IsMoving { get; private set; } = false;
		public bool IsFlipping { get; private set; } = false;

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
			SetupUI();
		}

		protected virtual void SetupUI()
		{
			PlayerProgress progress = Instantiate(ProgressPrefab, LevelManager.Instance.ProgressUI);
			progress.SetColor(ProgressColor);
			progress.Player = this;
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

		public void StopMovingImmediate()
		{
			IsMoving = false;
			moveSpeed = 0;
		}

		public void StrafeLeft()
		{
			Strafe(-StrafeSpeed);
		}

		public void StrafeRight()
		{
			Strafe(StrafeSpeed);
		}

		public void Strafe(float movement)
		{
			transform.position += Gravity * Vector3.right * (movement * Time.deltaTime);
		}

		public virtual void Flip(FlipDirection direction)
		{
			//if (IsFlipping)
			//	return;

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

			IsFlipping = true;
			transform.DOBlendableRotateBy(Gravity * rotation, FlipTime);
			ControlHelper.Delay(FlipTime, () => IsFlipping = false);

			if (CanFlipGravity(direction))
				FlipGravity(-rotation);
		}

		protected virtual void FlipGravity(Vector3 rotation)
		{
			Gravity *= Quaternion.Euler(rotation);
		}

		public bool CanFlipGravity(FlipDirection direction)
		{
			if (direction == FlipDirection.Left || direction == FlipDirection.Right)
			{
				Vector3 end = Gravity * new Vector3(direction == FlipDirection.Left ? -FlipDistance : FlipDistance, 0, 0);
				if (Physics.Linecast(transform.position, transform.position + end, out RaycastHit hit) &&
					hit.transform.gameObject.layer == LevelManager.EnvironmentLayer)
					return true;
			}

			return false;
		}

		protected virtual void Update()
		{
			if (moveSpeed > 0)
				transform.position += Vector3.forward * (moveSpeed * Time.deltaTime);
		}

		protected virtual void FixedUpdate()
		{
			rigidbody.AddForce(Gravity * Physics.gravity, ForceMode.Acceleration);
		}
	}
}