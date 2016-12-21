using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WinCans.Actions;


namespace WinCans.Actors
{
	public abstract class Actor
	{
		private ActionBase action;

		#region Properties

		/// <summary>
		/// Action currently attached to this actor. null if no action is attached.
		/// </summary>
		public ActionBase Action
		{
			get { return action; }
			set
			{
				action = value;
				if (action != null)
				{
					action.Target = this;
				}
			}
		}

		/// <summary>
		/// The color of this actor. This color is used to tint the visual representation.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Indicates if the actor is enabled.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// The position of the actor.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// The name of this actor. This is optional and only used for searching.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Whether this actor is visible.
		/// </summary>
		bool Visible { get; set; }

		/// <summary>
		/// The width of this actor
		/// </summary>
		public float Width { get; set; }

		/// <summary>
		/// The height of this actor
		/// </summary>
		public float Height { get; set; }

		/// <summary>
		/// The X offset that is applied to the Position before the actor is rendered.
		/// </summary>
		public float OriginX { get; set; }

		/// <summary>
		/// The Y offset that is applied to the Position before the actor is rendered.
		/// </summary>
		public float OriginY { get; set; }

		/// <summary>
		/// The rotation, if any, that is appied during render.
		/// </summary>
		public float Rotation { get; set; }

		/// <summary>
		/// Scale factor to apply to drawing this actor.
		/// </summary>
		public Vector2 Scale { get; set; }

		/// <summary>
		/// True if the actor has an action attached.
		/// </summary>
		public bool HasAction { get { return Action != null; } }

		/// <summary>
		/// The parent group of this actor, or null if not in a group.
		/// </summary>
		public Group Parent { get; set; }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public Actor()
		{
			Color = Color.White;
			Enabled = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Act(GameTime gameTime)
		{
			if (Action != null && Enabled)
			{
				if (Action.Act(gameTime))
				{
					Action.Target = null;
					Action = null;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="batch"></param>
		public abstract void Draw(GameTime gameTime, SpriteBatch batch, float alpha);

		/// <summary>
		/// Returns the deepest actor that contains the specified point and is touchable and visible, or null if no actor was hit.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public virtual Actor HitTest(float x, float y)
		{
			return (x >= Position.X && x < Position.X + Width && y >= Position.Y && y < Position.Y + Height) ? this : null;
		}

		/// <summary>
		/// Move this actor by a specified amount
		/// </summary>
		/// <param name="delta">Vector2 specifiying the move amount</param>
		public void MoveBy(Vector2 delta)
		{
			SetPosition(Position + delta);
		}

		/// <summary>
		/// Sets the position of the actor
		/// </summary>
		/// <param name="pos"></param>
		private void SetPosition(Vector2 pos)
		{
			Position = pos;
			PositionChanged();
		}

		/// <summary>
		/// Removes this actor from its parent, if it has a parent.
		/// </summary>
		public void Remove()
		{
			if (Parent != null)
			{
				Parent.RemoveActor(this);
				ActorRemoved();
			}
		}

		/// <summary>
		/// Triggered when the actors position changes.
		/// </summary>
		protected virtual void PositionChanged() {}

		/// <summary>
		/// Triggered when the actor is removed from its parent group.
		/// </summary>
		protected virtual void ActorRemoved() { }
	}
}
