using System;
using Microsoft.Xna.Framework;

namespace WinCans
{
	public class GameBoard
	{
		private int[] Count = new int[4];
		private int selectedCount = 0;
		private int selectedPoints = 0;
		private Rectangle selectedBounds = new Rectangle();
		private CanData[,] board = null;

		public int Width { get; private set; }

		public int Height { get; private set; }

		public int SelectedPoints { get { return selectedPoints; } }

		public Rectangle SelectedBounds { get { return selectedBounds; } }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public GameBoard(int width, int height)
		{
			Width = width;
			Height = height;
			board = new CanData[Width,Height];
		}

		/// <summary>
		/// 
		/// </summary>
		public void NewBoard()
		{
			Random rand = new Random();
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					int r = rand.Next(4);
					board[x,y] = new CanData(x,y,r,CanData.CanState.NORMAL);
					Count[r]++;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		public CanData GetCanData(int col, int row)
		{
			if (col >= 0 && col < Width &&
					row >= 0 && row < Height)
			{
				return board[col,row];
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public void ClearSelected()
		{
			for (int col = 0; col < Width; col++)
			{
				for (int row = 0; row < Height; row++)
				{
					if (this.board[col,row] != null) {
						board[col,row].state = CanData.CanState.NORMAL;
					}
				}
			}

			selectedCount = 0;
			selectedPoints = 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		public bool Select(int col, int row)
		{
			bool collect = false;

			// See if the clicked position was valid
			if (board[col,row] == null)
			{
				ClearSelected();
				return false;
			}

			// Check to see if the click was on a ball that is already selected
			if (board[col,row].state == CanData.CanState.SELECTED)
			{
				collect = true;
			}
			else
			{
				ClearSelected();

				selectedBounds.Location = new Point(col, row);
				selectedBounds.Size = new Point(1, 1);
				RecursiveSelect(this.board[col,row].index, col, row);

				if (selectedCount == 1)
				{
					board[col,row].state = CanData.CanState.NORMAL;
				}
				else
				{
					int m = 2;
					this.selectedPoints = 2;
					for (int i = 0; i < (this.selectedCount - 2); i++)
					{
						this.selectedPoints += m;
						m += 2;
					}
				}
			}

			return collect;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="seed"></param>
		/// <param name="col"></param>
		/// <param name="row"></param>
		void RecursiveSelect(int seed, int col, int row)
		{
			CanData block;

			selectedCount++;
			board[col,row].state = CanData.CanState.SELECTED;

			// Update selection boundary
			selectedBounds = Rectangle.Union(selectedBounds, new Rectangle(col, row, 1, 1));

			if (col > 0)
			{
				block = this.board[col - 1,row];
				if (block != null && block.index == seed && block.state == CanData.CanState.NORMAL)
				{
					RecursiveSelect(seed, col - 1, row);
				}
			}

			if (col < Width - 1)
			{
				block = this.board[col + 1,row];
				if (block != null && block.index == seed && block.state == CanData.CanState.NORMAL)
				{
					RecursiveSelect(seed, col + 1, row);
				}
			}

			if (row > 0)
			{
				block = this.board[col,row - 1];
				if (block != null && block.index == seed && block.state == CanData.CanState.NORMAL)
				{
					RecursiveSelect(seed, col, row - 1);
				}
			}

			if (row < Height - 1)
			{
				block = this.board[col,row + 1];
				if (block != null && block.index == seed && block.state == CanData.CanState.NORMAL)
				{
					RecursiveSelect(seed, col, row + 1);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int Collect()
		{
			int score;
			int col = 0;
			int row;

			while (col < Width)
			{
				row = Height - 1;
				while (row >= 0)
				{
					CanData blk = board[col,row];
					if (blk == null)
					{
						break; // This is the top of this column
					}

					if (blk.state == CanData.CanState.SELECTED)
					{
						//save type and position to undo list
						//undoList.add(blk.getData());
						blk.state = CanData.CanState.DYING;
						CollapseColumn(col, row);
					}
					else
					{
						row--;
					}
				}
				if (board[col,Height - 1] == null)
				{
					CompressHorizontally(col);
					if (board[col,Height - 1] == null)
					{
						break;
					}
				}
				else
				{
					col++;
				}
			}

			score = this.selectedPoints;
			this.selectedCount = 0;
			this.selectedPoints = 0;

			return score;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Undo()
		{
			for (int col = 0; col < Width; col++)
			{
				for (int row = 0; row < Height; row++)
				{
					CanData blk = board[col,row];
					if (blk != null)
					{

					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="col"></param>
		/// <param name="row"></param>
		private void CollapseColumn(int col, int row)
		{
			while (row > 0) // Shift column down
			{
				CanData blk = board[col,row - 1];
				if (blk == null)
				{
					break;
				}

				if (blk.state != CanData.CanState.DYING)
				{
					blk.y++;
				}
				board[col,row] = blk;
				row--;
			}
			board[col,row] = null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="col"></param>
		private void CompressHorizontally(int col)
		{
			while (col < Width - 1)
			{
				for (int row = 0; row < Height; row++)
				{
					board[col,row] = board[col + 1,row];
					board[col + 1,row] = null;

					if (board[col,row] != null)
					{
						board[col, row].x--;
					}
				}
				col++;
			}
		}

		/// <summary>
		/// Check the board for any remaining valid moves. The game is over if none are found
		/// </summary>
		/// <returns>True if the game is over</returns>
		public bool CheckGameOver()
		{
			for (int col = 0; col < Width; col++)
			{
				for (int row = 0; row < Height - 1; row++)
				{
					CanData b1 = board[col,row];
					CanData b2 = board[col,row + 1];

					if ((b1 != null && b2 != null && b1.index == b2.index))
					{
						return false;
					}
				}
			}

			for (int row = 0; row < Height; row++)
			{
				for (int col = 0; col < Width - 1; col++)
				{
					CanData b1 = board[col,row];
					CanData b2 = board[col + 1,row];

					if ((b1 != null && b2 != null && b1.index == b2.index))
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}