using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinCans.Actors;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace WinCans.Screens
{
	public class MainScreen
	{
		static int BOARD_WIDTH = 13;
		static int BOARD_HEIGHT = 10;
		static int LEFT_BORDER = 10;
		static int TOP_BORDER = 98;
		static int CAN_WIDTH = 54;
		static int CAN_HEIGHT = 60;
		static int SCORE_POS_X = 35;
		static int SCORE_POS_Y = 25;
		static int SELECTED_POS_X = 430;
		static int SELECTED_POS_Y = 25;

		private Group canGroup = new Group();
		private GameBoard gameBoard;
		private int score;
		private Point currentMousePos;
		private MouseState lastState = Mouse.GetState();
		private Vector2 scorePos = new Vector2(SCORE_POS_X,SCORE_POS_Y);
		private Vector2 selectedPos = new Vector2(SELECTED_POS_X, SELECTED_POS_Y);

		/// <summary>
		/// Constructor
		/// </summary>
		public MainScreen()
		{
			gameBoard = new GameBoard(BOARD_WIDTH, BOARD_HEIGHT);
			NewGame();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			MouseState ms = Mouse.GetState();

			currentMousePos = ms.Position;
			if (ms.LeftButton == ButtonState.Pressed && ms.LeftButton != lastState.LeftButton)
			{
				Actor actor = canGroup.HitTest(currentMousePos.X, currentMousePos.Y);
				if (actor != null)
				{
					CanData data = (actor as CanActor).Data;
					if (gameBoard.Select(data.x, data.y))
					{
						// Soundfx
						MediaPlayer.Play(Assets.Instance.FadeSFX);
						score += gameBoard.Collect();

						// Check for game end
						if (gameBoard.CheckGameOver())
						{
							// TODO: Show "Game Over" text and a restart button
						}
					}
				}
			}
			lastState = ms;

			canGroup.Act(gameTime);
		}

		/// <summary>
		/// Called every frame to draw the screen
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			// Draw background
			spriteBatch.Draw(Assets.Instance.Back, Vector2.Zero, Color.White);

			// Draw cans
			canGroup.Draw(gameTime, spriteBatch, 1.0f);

			// Draw score and selected points
			spriteBatch.DrawString(Assets.Instance.HudFont, string.Format("{0}", score), scorePos, Color.Red);
			spriteBatch.DrawString(Assets.Instance.HudFontSmall, string.Format("{0}",gameBoard.SelectedPoints), selectedPos, Color.Red);

			// Pointer
			spriteBatch.Draw(Assets.Instance.Pointer, currentMousePos.ToVector2(), Color.White);

			spriteBatch.End();
		}

		/// <summary>
		/// Creates a new game and set of can objects
		/// </summary>
		public void NewGame()
		{
			gameBoard.NewBoard();

			canGroup.Clear();
			for (int x = 0; x < BOARD_WIDTH; x++)
			{
				for (int y = 0; y < BOARD_HEIGHT; y++)
				{
					CanActor b = new CanActor(gameBoard.GetCanData(x,y));
					b.Position = new Vector2(LEFT_BORDER + x * CAN_WIDTH, TOP_BORDER + y * CAN_HEIGHT);
					canGroup.Add(b);
				}
			}
		}
	}
}
