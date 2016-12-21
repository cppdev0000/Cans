using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	/// <summary>
	/// Delays for a specified number of milliseconds
	/// </summary>
	public class DelayAction : ActionBase
	{
		private float duration;
		private float time;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="duration">Delay length</param>
		public DelayAction(float duration)
		{
			this.duration = duration;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override bool Act(GameTime gameTime)
		{
			time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			return (duration < time);
	}

		/// <summary>
		/// Immediately ends the delay action
		/// </summary>
		public void Finish()
		{
			time = duration;
		}

		/// <summary>
		/// 
		/// </summary>
		public override void Restart()
		{
			base.Restart();
			time = 0;
		}
	}
}
