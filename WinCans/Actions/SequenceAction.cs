using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WinCans.Actors;

namespace WinCans.Actions
{
	public class SequenceAction : ActionBase
	{
		protected List<ActionBase> actions = new List<ActionBase>();
		private bool hasCompleted;
		private int index = 0;

		/// <summary>
		/// 
		/// </summary>
		public override Actor Target
		{
			get { return base.Target; }
			set
			{
				base.Target = value;
				foreach (ActionBase action in actions)
				{
					action.Target = value;
				}
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public SequenceAction()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="list"></param>
		public SequenceAction(params ActionBase[] list)
		{
			for (int i = 0; i < list.Length; i++)
			{
				AddAction(list[i]);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override bool Act(GameTime gameTime)
		{
			if (index >= actions.Count)
			{
				// All actions have been completed
				return true;
			}

			if (actions[index].Act(gameTime))
			{
				if (Target == null) return true; // This action was removed.
				index++;
				if (index >= actions.Count) return true;
			}

			return false;
		}

		/// <summary>
		/// Restarts the sequence
		/// </summary>
		public override void Restart()
		{
			index = 0;

			hasCompleted = false;

			foreach (ActionBase action in actions)
			{
				action.Restart();
			}
		}

		/// <summary>
		/// Adds an action to be run in sequence
		/// </summary>
		/// <param name="action">Action to add</param>
		public void AddAction(ActionBase action)
		{
			actions.Add(action);
		}
	}
}
