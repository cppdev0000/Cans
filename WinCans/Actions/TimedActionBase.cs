using Microsoft.Xna.Framework;
using WinCans.Easing;

namespace WinCans.Actions
{
	/// <summary>
	/// Base class for actions that take place over a period of time
	/// </summary>
	public abstract class TimedActionBase : ActionBase
	{
		private bool hasBegan;
		private bool hasCompleted;

		/// <summary>
		/// Interpolation in use. Optional.
		/// </summary>
		public EaseBase Ease { get; set; }

		/// <summary>
		/// True if the action is being run in reverse.
		/// </summary>
		public bool Reverse { get; set; }

		/// <summary>
		/// How long the action should take to complete one cycle.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Time elapsed for the current cycle.
		/// </summary>
		public float TimeElapsed { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public TimedActionBase()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="duration">How long the action should take to complete one cycle</param>
		/// <param name="interpolation"></param>
		public TimedActionBase(float duration, EaseBase ease = null)
		{
			this.Duration = duration;
			Ease = ease;
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
				// Nothing left to do
				return true;
			}

			if (!hasBegan)
			{
				OnBegin();
				hasBegan = true;
			}

			TimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			hasCompleted = (TimeElapsed >= Duration);

			float percent;
			if (hasCompleted)
			{
				percent = 1;
			}
			else
			{
				percent = TimeElapsed / Duration;
				if (Ease!= null)
				{
					percent = Ease.Apply(percent);
				}
			}
			Update(Reverse ? 1 - percent : percent);

			if (hasCompleted)
			{
				OnCompleted();
			}

			return hasCompleted;
		}

		/// <summary>
		/// Called the first time the Act method is called.
		/// </summary>
		protected virtual void OnBegin()
		{
		}

		/// <summary>
		/// Called when the action has completed its cycle
		/// </summary>
		protected virtual void OnCompleted()
		{
		}

		/// <summary>
		/// Called to update the state of the action.
		/// </summary>
		/// <param name="percent">A value between 0.0 and 1.0 indication the progress.</param>
		protected abstract void Update(float percent);

		/// <summary>
		/// Forces completion of the action
		/// </summary>
		public void Finish()
		{
			TimeElapsed = Duration;
		}

		/// <summary>
		/// Restarts the action
		/// </summary>
		public override void Restart()
		{
			TimeElapsed = 0;
			hasBegan = false;
			hasCompleted = false;
		}

		/// <summary>
		/// Resets the state of the temporal action.
		/// </summary>
		public override void Reset()
		{
			base.Reset();

			Reverse = false;
			Ease = null;
		}
	}
}
