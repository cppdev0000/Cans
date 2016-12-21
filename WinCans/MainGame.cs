using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinCans.Screens;

namespace WinCans
{
	/// <summary>
	/// 
	/// </summary>
	public class MainGame : Game
	{
		private const int GAME_SCREEN_WIDTH = 720;
		private const int GAME_SCREEN_HEIGHT = 750;

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private MainScreen mainScreen;

		/// <summary>
		/// 
		/// </summary>
		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = GAME_SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = GAME_SCREEN_HEIGHT;
			IsMouseVisible = false;
		}

		protected override void Initialize()
		{
			mainScreen = new MainScreen();
			spriteBatch = new SpriteBatch(GraphicsDevice);
			mainScreen.NewGame();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			Assets.Instance.Load(this);
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			mainScreen.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			mainScreen.Draw(gameTime,spriteBatch);
		}
	}
}
