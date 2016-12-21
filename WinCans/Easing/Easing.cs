
namespace WinCans.Easing
{
	public abstract class EaseBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="a">Alpha value between 0 and 1</param>
		/// <returns></returns>
		public abstract float Apply(float a);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="a">Alpha value between 0 and 1</param>
		/// <returns></returns>
		public float Apply(float start, float end, float a)
		{
			return start + (end - start) * Apply(a);
		}
	}
}
