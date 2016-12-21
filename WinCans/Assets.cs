using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace WinCans
{
	public class Assets
	{
		public static Assets Instance = new Assets();

		private Animation[] normalAnimation;
		private Animation[] dieAnimation;

		public Texture2D Back { get; private set; }

		public SpriteFont HudFont { get; set; }
		public SpriteFont HudFontSmall { get; set; }

		public Texture2D CansTexture { get; private set; }
		private Dictionary<String, Rectangle> CansTextureInfo;

		public Texture2D Pointer { get; private set; }
		public Texture2D GameOver { get; private set; }

		// SFX
		public Song FadeSFX { get; private set; }

		public Assets() {}

		public void Load(MainGame game)
		{
			CansTexture = game.Content.Load<Texture2D>("Cans");
			CansTextureInfo = game.Content.Load<Dictionary<String, Rectangle>>("Cans_Atlas");

			HudFont = game.Content.Load<SpriteFont>("HudFont");
			HudFontSmall = game.Content.Load<SpriteFont>("HudFontSmall");

			Back = game.Content.Load<Texture2D>("Machine");

			normalAnimation = new Animation[5]
			{
				new Animation(100, GetRegions("Blue")),
				new Animation(100, GetRegions("Red")),
				new Animation(100, GetRegions("Orange")),
				new Animation(100, GetRegions("Purple")),
				new Animation(100, GetRegions("Green"))
			};

			dieAnimation = new Animation[5]
			{
				new Animation(100, GetRegions("BlueCrush")),
				new Animation(100, GetRegions("RedCrush")),
				new Animation(100, GetRegions("OrangeCrush")),
				new Animation(100, GetRegions("PurpleCrush")),
				new Animation(100, GetRegions("GreenCrush"))
			};

			Pointer = game.Content.Load<Texture2D>("Pointer");
			GameOver = game.Content.Load<Texture2D>("GameOver");

			FadeSFX = game.Content.Load<Song>("SFX/FadingBlock1");
		}

		public Animation GetCanNormalAnimation(int index)
		{
			return normalAnimation[index];
		}

		public Animation GetCanDieAnimation(int index)
		{
			return dieAnimation[index];
		}

		private Rectangle[] GetRegions(string regionName)
		{
			var regions = from obj in CansTextureInfo where obj.Key.StartsWith(regionName) select obj.Value;

			return regions.ToArray<Rectangle>();
		}
	}
}
