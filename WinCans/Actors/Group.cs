using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinCans.Actors
{
	/// <summary>
	/// A collection of actors and subgroups.
	/// </summary>
	public class Group : Actor
	{
		public List<Actor> Actors { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public Group()
		{
			Actors = new List<Actor>();
		}

		/// <summary>
		/// Instructs all actors in this group to perform their tasks
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Act(GameTime gameTime)
		{
			for (int i = 0;i < Actors.Count;i++)
			{
				Actors[i].Act(gameTime);
			}
		}

		/// <summary>
		/// Adds an actor as a child of this group.
		/// </summary>
		/// <param name="actor"></param>
		public void Add(Actor actor)
		{
			actor.Parent = this;

			Actors.Add(actor);
		}

		/// <summary>
		/// Removes all actors from this group
		/// </summary>
		public void Clear()
		{
			Actors.Clear();
		}

		/// <summary>
		/// Instructs all actors in this group to paint themselves
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public override void Draw(GameTime gameTime, SpriteBatch batch, float alpha)
		{
			Actors.ForEach(a => a.Draw(gameTime,batch,alpha));
		}

		/// <summary>
		/// Returns the first actor found with the specified name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns>The actor with the requested name, or null if there is no actor with that name. This implementation
		/// does not recurse into subgroups.</returns>
		public Actor FindActor(string name)
		{
			return Actors.Find(a => a.Name.Equals(name));
		}

		/// <summary>
		/// Returns the topmost actor that intersects with the specified point and is visible, null otherwise.
		/// </summary>
		/// <param name="x">Horizontal position</param>
		/// <param name="y">Vertical position</param>
		/// <returns>Actor that was hit or null if no hit</returns>
		public override Actor HitTest(float x, float y)
		{
			foreach (Actor actor in Actors)
			{
				Actor actorHit = actor.HitTest(x, y);
				if (actorHit != null)
				{
					return actorHit;
				}
			}

			return null;
		}

		/// <summary>
		/// Removes specified actor from the group.
		/// </summary>
		/// <param name="actor"></param>
		/// <returns>True if actor was removed</returns>
		public bool RemoveActor(Actor actor)
		{
			return Actors.Remove(actor);
		}
	}
}
