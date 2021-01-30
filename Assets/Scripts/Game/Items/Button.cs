using DG.Tweening;
using Kit;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
	public class Button: MonoBehaviour
	{
		public float PressTime = 0.1f;
		public float PressDistance = 0.28f;
		public AudioClip PressSound;
		public UnityEvent OnPressed;
		public BoxCollider Collider { get; protected set; }
		public Bounds Bounds { get; private set; }

		protected virtual void Awake()
		{
			Collider = GetComponent<BoxCollider>();
			Bounds = Collider.bounds;
		}

		private void OnTriggerEnter(Collider other)
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
				PressButton();
		}

		protected virtual void PressButton()
		{
			transform.DOBlendableMoveBy(transform.up * -PressDistance, PressTime);
			OnPressed?.Invoke();
			AudioManager.Play(PressSound);
		}
	}
}