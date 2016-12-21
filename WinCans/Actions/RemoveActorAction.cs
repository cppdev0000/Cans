using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	/// <summary>
	/// 
	/// </summary>
	public class RemoveActorAction : ActionBase
	{
		private bool removed;

		/// <summary>
		/// Constructor
		/// </summary>
		public RemoveActorAction()
		{
			removed = false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override bool Act(GameTime gameTime)
		{
			// Prevent multiple removals
			if (!removed)
			{
				removed = true;
				Target.Remove();
			}

			return true;
		}
	}
}
