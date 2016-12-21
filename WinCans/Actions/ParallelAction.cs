using System.Collections.Generic;
using WinCans.Actors;
using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	/// <summary>
	/// Action that allows multiple actions to run at once
	/// </summary>
	public class ParallelAction : ActionBase
	{
		protected List<ActionBase> actions = new List<ActionBase>();
		private bool hasCompleted;

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
		public ParallelAction()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="list"></param>
		public ParallelAction(params ActionBase[] list)
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
			if (hasCompleted)
			{
				return true;
			}

			hasCompleted = true; // Default to completed

			foreach (ActionBase action in actions)
			{
				if (action.Target != null && !action.Act(gameTime))
				{
					hasCompleted = false;
				}
				if (Target == null)
				{
					return true; // This action was removed.
				}
			}

			return hasCompleted;
		}

		/// <summary>
		/// 
		/// </summary>
		public override void Restart()
		{
			hasCompleted = false;

			foreach (ActionBase action in actions)
			{
				action.Restart();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			actions.Clear();
		}

		/// <summary>
		/// Adds an action to be run in parallel
		/// </summary>
		/// <param name="action">Action to add</param>
		public void AddAction(ActionBase action)
		{
			actions.Add(action);
		}
	}
}
