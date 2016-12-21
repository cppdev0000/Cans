using Microsoft.Xna.Framework;

namespace WinCans.Actions
{
	public class MoveTo : TemporalAction
	{
		private Vector2 startPos;
		//private float startY;
		private Vector2 endPos;
		//private float endY;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="time"></param>
		/// <param name="interpolation"></param>
		public MoveTo(Vector2 pos,float time,Interpolation interpolation = null)
		{
			endPos = pos;
			Duration = time;
			Interpolation = interpolation;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="percent"></param>
		protected override void Update(float percent)
		{
			Target.Position = new Vector2(startPos.X + (endPos.X - startPos.X) * percent, startPos.Y + (endPos.Y - startPos.Y) * percent);
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void OnBegin()
		{
			startPos = Target.Position;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/*public void SetPosition(float x, float y)
		{
			endX = x;
			endY = y;
		}*/
	}
}
