using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinCans.Actors;
using WinCans.Actions;
using WinCans.Easing;

namespace WinCans
{
	public class CanActor : Actor
	{
		private int lastX;
		private int lastY;
		private CanData.CanState lastState;

		public CanData Data { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data"></param>
		public CanActor(CanData data)
		{
			this.Data = data;
			lastX = this.Data.x;
			lastY = this.Data.y;
			lastState = this.Data.state;

			Width = 53; // TODO: Convert to constant
			Height = 60; // TODO: Convert to constant
			OriginX = Width / 2;
			OriginY = Height / 2;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Act(GameTime gameTime)
		{
			// Detect if the block has been moved
			if (Data.x != lastX || Data.y != lastY)
			{
				Action = new SequenceAction(
					new DelayAction(150),
					new MoveByAction(new Vector2(Width * (Data.x - lastX), Height * (Data.y - lastY)), 750, new BounceOut(4))
				);

				lastX = Data.x;
				lastY = Data.y;
			}

			// Detect if this block is dead
			if (Data.state != lastState && Data.state == CanData.CanState.DYING)
			{
				Action = new SequenceAction(
					new ColorAction(Color.Transparent,500),
					new RemoveActorAction(
				));
				lastState = Data.state;
			}

			base.Act(gameTime);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch batch, float alpha)
		{
			switch (Data.state)
			{
				case CanData.CanState.NORMAL:
					batch.Draw(Assets.Instance.CansTexture, Position, Assets.Instance.GetCanNormalAnimation(Data.index).GetFrame(0), Color * alpha);
					break;
				case CanData.CanState.SELECTED:
					batch.Draw(Assets.Instance.CansTexture, Position,
						Assets.Instance.GetCanNormalAnimation(Data.index).GetKeyFrame(gameTime), Color);
					break;
				case CanData.CanState.DYING:
					batch.Draw(Assets.Instance.CansTexture, Position,
						Assets.Instance.GetCanDieAnimation(Data.index).GetKeyFrame(gameTime), Color);
					break;
			}
		}

		/// <summary>
		/// Ensures that the data field is cleared when this can is removed in order to allow for GC
		/// </summary>
		protected override void ActorRemoved()
		{
			Data = null;
		}
	}
}
