using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	public class ColorAction : TimedActionBase
	{
		private Color startColor;
		private Color targetColor;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="targetColor">The final color when the color action is complete.</param>
		/// <param name="duration">Amount of time before the color reaches the value of targetColor.</param>
		public ColorAction(Color targetColor,float duration)
		{
			this.targetColor = targetColor;
			Duration = duration;
		}

		/// <summary>
		/// Called when the action first starts
		/// </summary>
		protected override void OnBegin()
		{
			startColor = Target.Color; // Save the target actors start color
		}

		/// <summary>
		/// Updates the current color to be closer to the target color, depending on current pecentage.
		/// </summary>
		/// <param name="percent"></param>
		protected override void Update(float percent)
		{
			Color newColor = new Color();

			newColor.R = (byte)(startColor.R + (targetColor.R - startColor.R) * percent);
			newColor.G = (byte)(startColor.G + (targetColor.G - startColor.G) * percent);
			newColor.B = (byte)(startColor.B + (targetColor.B - startColor.B) * percent);
			newColor.A = (byte)(startColor.A + (targetColor.A - startColor.A) * percent);

			Target.Color = newColor;
		}

		/// <summary>
		/// 
		/// </summary>
		public override void Reset()
		{
			Target.Color = startColor;
		}
	}
}
