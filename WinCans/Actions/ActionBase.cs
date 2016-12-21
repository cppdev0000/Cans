using Microsoft.Xna.Framework;
using WinCans.Actors;

namespace WinCans.Actions
{
	public abstract class ActionBase
	{
		public virtual Actor Target { get; set; }

		public abstract bool Act(GameTime gameTime);

		/// <summary>
		/// Resets the state of the action
		/// </summary>
		public virtual void Reset()
		{
			Target = null;
			Restart();
		}

		/// <summary>
		/// Restarts the action
		/// </summary>
		public virtual void Restart() { }
	}
}
