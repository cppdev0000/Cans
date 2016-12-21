namespace WinCans.Actions
{
	public abstract class RelativeTemporalAction : TimedActionBase
	{
		private float lastPercent;

		protected override void OnBegin()
		{
			lastPercent = 0;
		}

		protected override void Update (float percent)
		{
			UpdateRelative(percent - lastPercent);
			lastPercent = percent;
		}

		protected abstract void UpdateRelative(float percentDelta);
	}
}
