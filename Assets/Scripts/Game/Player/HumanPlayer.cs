using System;
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
			if (Input.GetKey(KeyCode.A))
				SlideLeft();

			if (Input.GetKey(KeyCode.D))
				SlideRight();
		}
	}
}