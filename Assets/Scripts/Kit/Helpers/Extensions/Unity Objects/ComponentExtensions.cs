using UnityEngine;

namespace Kit
{
	/// <summary><see cref="Component" /> extensions.</summary>
	public static class ComponentExtensions
	{
		/// <summary>Returns whether the <see cref="Component" /> is a part of a prefab.</summary>
		public static bool IsPrefab(this Component component)
		{
			return component.gameObject.IsPrefab();
		}

        /// <summary>
        ///     <para>Returns the bounds of the <see cref="Component" />.</para>
        /// </summary>
        /// <remarks>
        /// 	Works directly for <see cref="Renderer" />s and <see cref="Collider" />s, otherwise returns the
        /// 	<see cref="GameObjectExtensions.GetBounds">bounds of the GameObject</see>.
        /// </remarks>
        public static Bounds GetBounds(this Component component)
		{
			switch (component)
			{
				case Transform t:
					return t.GetBounds();

				case Renderer r:
					return r.bounds;

				case Collider c:
					return c.bounds;

				default:
					return component.transform.GetBounds();
			}
		}
	}
}