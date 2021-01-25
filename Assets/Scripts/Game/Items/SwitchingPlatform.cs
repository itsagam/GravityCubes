using DG.Tweening;
using Kit;
using UnityEngine;

namespace Game
{
	public class SwitchingPlatform: MonoBehaviour
	{
		public float SwitchTime = 1.0f;
		public AudioClip SwitchSound;
		public Vector3 InitialPosition { get; protected set; }
		public Vector3 OtherPosition;
		public bool IsInitial { get; protected set; } = true;

		private void Awake()
		{
			InitialPosition = transform.position;
		}

		public void Switch()
		{
			transform.DOKill();
			transform.DOMove(IsInitial ? OtherPosition : InitialPosition, SwitchTime);
			IsInitial = !IsInitial;
			AudioManager.Play(SwitchSound);
		}
	}
}