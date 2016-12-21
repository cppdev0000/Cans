using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Common.GDX;
using Common.GDX.Actors;

namespace WinCans.Screens
{
	public class GameScreen : Screen
	{
		static int BOARD_WIDTH = 10;
		static int BOARD_HEIGHT = 10;

		private Group actors = new Group();
		private GameBoard gameBoard;
		private int score;
		private Vector2 currentMousePos;

		public GameScreen(ScreenManager manager) : base(manager) { }

		/// <summary>
		/// 
		/// </summary>
		public override void Initialize()
		{
			gameBoard = new GameBoard(BOARD_WIDTH, BOARD_HEIGHT);
			NewGame();
			score = 0;
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
			Actor actor = actors.HitTest(x,y);
			if (actor != null)
			{
				BlockData data = (actor as Block).Data;
				if (gameBoard.Select(data.x, data.y))
				{
					//soundfx
					score += gameBoard.Collect();
				}
			}
		}

		public override void MouseMove(int x, int y)
		{
			currentMousePos = new Vector2(x, y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public override void Draw(GameTime gameTime,SpriteBatch batch, float alpha)
		{
			batch.Draw(Assets.Instance.Back, Vector2.Zero, Color.White * alpha);

			actors.Draw(gameTime,batch,alpha);

			batch.DrawString(Assets.Instance.HudFont, string.Format("Score: {0}", score), new Vector2(25, 25), Color.Black * alpha);
			//batch.DrawString(Assets.Instance.HudFont, string.Format("Selected: {0}",gameBoard.SelectedPoints), new Vector2(0, 0), Color.White);

			batch.Draw(Assets.Instance.Cans.Texture, currentMousePos, Assets.Instance.GetCanNormalAnimation(0).GetFrame(0), Color.White);
			//batch.DrawString(Assets.Instance.HudFont, string.Format("{0}",currentMousePos), new Vector2(0, 0), Color.White);
		}

		/// <summary>
		/// 
		/// </summary>
		private void NewGame()
		{
			gameBoard.NewBoard();

			actors.Clear();
			for (int x = 0; x < BOARD_WIDTH; x++)
			{
				for (int y = 0; y < BOARD_HEIGHT; y++)
				{
					Block b = new Block(gameBoard.GetBlockData(x,y));
					b.Position = new Vector2(72 + x * 53, 58 + y * 60);
					actors.Add(b);
				}
			}
		}
	}
}
