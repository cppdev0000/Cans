using Microsoft.Xna.Framework;
using WinCans.Easing;

namespace WinCans.Actions
{
	public class MoveByAction : TimedActionBase
	{
		private Vector2 startPosition;
		private Vector2 amountToMoveBy;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="duration"></param>
		/// <param name="interpolation"></param>
		public MoveByAction(Vector2 amount, float duration, EaseBase ease = null)
		{
			this.amountToMoveBy = amount;
			Duration = duration;
			Ease = ease;
		}

		/// <summary>
		/// Called when the action begins
		/// </summary>
		protected override void OnBegin()
		{
			// Save the original target actor position
			startPosition = Target.Position;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="percent"></param>
		protected override void Update(float percent)
		{
			Target.Position = startPosition + (amountToMoveBy * percent);
		}

		/// <summary>
		/// 
		/// </summary>
		public override void Restart()
		{
			base.Restart();

			Target.Position = startPosition;
		}
	}
}
