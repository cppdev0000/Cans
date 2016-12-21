using Microsoft.Xna.Framework;
using System;

namespace WinCans
{
	public class Animation
	{
		private Rectangle[] keyFrames;
		private float frameDuration;
		private float animationDuration;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="frameDuration"></param>
		/// <param name="keyFrames"></param>
		public Animation(float frameDuration, Rectangle[] keyFrames)
		{
			this.frameDuration = frameDuration;
			this.animationDuration = keyFrames.Length * frameDuration;
			this.keyFrames = keyFrames;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public Rectangle GetKeyFrame(GameTime gameTime)
		{
			return keyFrames[GetKeyFrameIndex(gameTime)];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="frameNumber"></param>
		/// <returns></returns>
		public Rectangle GetFrame(int frameNumber)
		{
			return keyFrames[frameNumber];
		}

		/// <summary>
		/// Returns the current frame number
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public int GetKeyFrameIndex(GameTime gameTime)
		{
			if (keyFrames.Length == 1) return 0;

			int frameNumber = (int)(gameTime.TotalGameTime.Milliseconds / frameDuration);
			frameNumber = Math.Min(keyFrames.Length - 1, frameNumber);

			return frameNumber;
		}
	}
}
