using System;
using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	class ScaleTo : TemporalAction
	{
		private Vector2 startScale;
		private Vector2 endScale;

		public ScaleTo() {}

		public ScaleTo(Vector2 endScale)
		{
			this.endScale = endScale;
		}

		protected override void OnBegin()
		{
			startScale = Target.Scale;
		}

		protected override void Update(float percent)
		{
			Target.Scale = new Vector2(startScale.X + (endScale.X - startScale.X) * percent, startScale.Y + (endScale.Y - startScale.Y) * percent);
		}
	}
}
