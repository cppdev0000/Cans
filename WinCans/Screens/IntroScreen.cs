using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Common.GDX;
using Common.GDX.Actors;

namespace WinCans.Screens
{
	public class IntroScreen : Screen
	{
		private Group actors = new Group();

		public IntroScreen(ScreenManager manager) : base(manager) { }

		/// <summary>
		/// 
		/// </summary>
		public override void Initialize()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			actors.Act(gameTime);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public override void MouseLeftButtonDown(int x, int y)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public override void Draw(GameTime gameTime,SpriteBatch batch, float alpha)
		{
			batch.GraphicsDevice.Clear(Color.CornflowerBlue);

			//actors.Draw(gameTime,batch,alpha);
			batch.DrawString(Assets.Instance.HudFont, "Score: 0", new Vector2(25, 50), Color.White * alpha);
		}
	}
}
