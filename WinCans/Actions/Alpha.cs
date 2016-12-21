using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	public class Alpha : TemporalAction
	{
		private byte start, end;

		public Alpha() {}

		public Alpha(byte end,float duration)
		{
			this.end = end;
			Duration = duration;
		}

		protected override void OnBegin()
		{
			start = Target.Color.A;
		}

		protected override void Update(float percent)
		{
			Color color = Target.Color;
			color.A = (byte)(start + (end - start) * percent);
			Target.Color = color;
		}
	}
}
