namespace WinCans
{
	public class CanData
	{
		public enum CanState
		{
			NORMAL,
			SELECTED,
			DYING
		};

		public int x;
		public int y;
		public int index;
		public CanState state;

		public CanData(int x, int y, int index, CanState state)
		{
			this.x = x;
			this.y = y;
			this.index = index;
			this.state = state;
		}
	}
}
