using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Tree_Printer
{
	/// <summary>
	/// 
	/// </summary>
	public static class BinaryTreePrinter
	{
		private const char LeftDown = '/';
		private const char RightDown = '\\';
		private const char SideWays = '-';

		private const char LeftDownPlaceholder = (char) 1;
		private const char RightDownPlaceholder = (char) 2;
		private const char SideWaysPlaceholder = (char) 3;
		private const char SpacePlaceholder = (char) 4;

		private static readonly Dictionary<char, char> ReplaceChars = new Dictionary<char, char>()
		{
			{LeftDownPlaceholder, LeftDown},
			{RightDownPlaceholder, RightDown},
			{SideWaysPlaceholder, SideWays},
			{SpacePlaceholder, ' '},
		};

		private static readonly HashSet<char> Trashable = new HashSet<char>()
		{
			SideWaysPlaceholder,
			SpacePlaceholder
		};


		/// <summary>
		/// returns a 2D char array of the binary tree
		/// </summary>
		/// <param name="head">root to print from</param>
		/// <param name="nodePrintLength">max space allocated for printing node's data</param>
		/// <returns></returns>
		public static char[,] GetCharArray(IPrintableBinaryNode head, int nodePrintLength)
		{
			return GetCharArray(head, GetHeight(head), nodePrintLength);
		}


		/// <summary>
		/// returns a 2D char array of the binary tree
		/// </summary>
		/// <param name="head">root to print from</param>
		/// <param name="treeHeight">depth to print tree</param>
		/// <param name="nodePrintLength">max space allocated for printing node's data</param>
		/// <returns></returns>
		public static char[,] GetCharArray(IPrintableBinaryNode head, int treeHeight, int nodePrintLength)
		{
			//if print length is odd number everything can be centered properly (easier math)
			if (nodePrintLength%2 == 0) nodePrintLength++;

			int printHeight = treeHeight + 1; // plus 1 for copyright
			int printWidth = GetWidth(treeHeight, nodePrintLength);

			//increase width if needed to fix copyright
			const string copyRight = "Tree display provided by Anthony Corbin";
			printWidth = Math.Max(printWidth, copyRight.Length); // must be at least the length of the copyright

			//allocating required space
			char[,] screen = new char[printHeight, printWidth];
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					screen[i, j] = SpacePlaceholder;
				}
			}

			//print tree starting in the middle of the screen
			PrintToArray(screen, head, treeHeight, new Coord(printWidth/2.0, 0), nodePrintLength);

			//print copyright
			InsertStringToCharArray(screen, new Coord(0, printHeight - 1), copyRight);
			return ReplacePlaceHolder(Trim(screen));
		}

		private static char[,] ReplacePlaceHolder(char[,] screen)
		{
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					if (ReplaceChars.ContainsKey(screen[i,j]))
					{
						screen[i,j] = ReplaceChars[screen[i,j]];
					}
				}
			}
			return screen;
		}

		private static char[,] Trim(char[,] screen)
		{
			HashSet<int> colsToKill = new HashSet<int>();
			int height = screen.GetLength(0);
			int width = screen.GetLength(1);
			for (int x = 0; x < width; x++)
			{
				bool canTrash = true;
				for (int y = 0; y < height && canTrash; y++)
				{
					canTrash = Trashable.Contains(screen[y,x]);
				}
				if (canTrash)
				{
					colsToKill.Add(x);
				}
			}
			if (colsToKill.Count == 0) return screen;

			char[,] ret = new char[height,width - colsToKill.Count];
			int retCol = 0;
			for (int x = 0; x < width; x++)
			{
				if (colsToKill.Contains(x)) continue;
				for (int y = 0; y < height; y++)
				{
					ret[y,retCol] = screen[y,x];
				}
				retCol++;
			}

			return ret;

		}



		/// <summary>
		/// returns a string of the given binary tree
		/// </summary>
		/// <param name="head">root node to print from</param>
		/// <param name="nodePrintLength">max space allocated for node data</param>
		/// <returns></returns>
		public static string GetString(IPrintableBinaryNode head, int nodePrintLength)
		{
			return GetString(head, GetHeight(head), nodePrintLength);
		}

		/// <summary>
		/// returns a string of the given binary tree
		/// </summary>
		/// <param name="head">root node to print from</param>
		/// <param name="treeHeight">depth to print tree</param>
		/// <param name="nodePrintLength">max space allocated for node data</param>
		/// <returns></returns>
		public static string GetString(IPrintableBinaryNode head, int treeHeight, int nodePrintLength)
		{
			// get printout
			char[,] screen = GetCharArray(head, treeHeight, nodePrintLength);

			//convert into string
			StringBuilder ret = new StringBuilder();
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					ret.Append(screen[i,j]);
				}
				ret.Append("\n");
			}
			return ret.ToString();
		}


		/// <summary>
		/// outputs GetString to console
		/// </summary>
		/// <param name="head">root node to print from</param>
		/// <param name="nodePrintLength">max space allocated for node data</param>
		/// <returns></returns>
		public static void Print(IPrintableBinaryNode head, int nodePrintLength)
		{
			Print(head, GetHeight(head), nodePrintLength);
		}

		/// <summary>
		/// outputs GetString to console
		/// </summary>
		/// <param name="head">root node to print from</param>
		/// <param name="treeHeight">depth to print tree</param>
		/// <param name="nodePrintLength">max space allocated for node data</param>
		public static void Print(IPrintableBinaryNode head, int treeHeight, int nodePrintLength)
		{
			// get printout
			char[,] screen = GetCharArray(head, treeHeight, nodePrintLength);

			//print
			char[] tmep = new char[1];
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					char toPrint = screen[i,j] == SpacePlaceholder ? ' ' : screen[i,j]; // replace uninitialized with ' '
					Console.Write(toPrint);
				}
				Console.Write("\n");
			}
		}



		//returns the required width to print given tree
		private static int GetWidth(int height, int nodePrintLength)
		{
			// also try out 3 for a at least 1 - in each tree level
			const int spacing = 1;
			return (spacing + nodePrintLength)*(int) Math.Pow(2, height - 1) - 1; // trust my math
		}

		//prints tree to char[][] starting at pos
		private static void PrintToArray(char[,] screen, IPrintableBinaryNode node, int height, Coord pos, int dataLength)
		{
			if (node == null) return;

			//print node data
			string data = node.GetString();
			InsertStringToCharArrayCentered(screen, pos, data);
			//should traverse
			if (height == 1) return;

			//trust the math
			int lastWidth = GetWidth(height - 1, dataLength);
			int numOfDashes = (lastWidth - 1)/2 - ((dataLength - 1)/2 - 1);

			int offset = (dataLength - 1)/2;

			//printing the pretty
			for (int i = 1; i <= numOfDashes; i++)
			{
				char printL = i == numOfDashes ? LeftDownPlaceholder : SideWaysPlaceholder; // character to be printed
				char printR = i == numOfDashes ? RightDownPlaceholder : SideWaysPlaceholder; // character to be printed
				offset += 1;
				if (node.GetLeft() != null) InsertStringToCharArray(screen, pos.Add(-offset, 0), printL); // PRINT!!
				if (node.GetRight() != null) InsertStringToCharArray(screen, pos.Add(offset, 0), printR); // PRINT!!
			}
			//recourse for each branch with position at the end of each branch +1 in the y
			PrintToArray(screen, node.GetLeft(), height - 1, pos.Add(-offset, 1), dataLength);
			PrintToArray(screen, node.GetRight(), height - 1, pos.Add(offset, 1), dataLength);
		}

		//centers given string on pos
		private static void InsertStringToCharArrayCentered(char[,] screen, Coord pos, string data)
		{
			pos = new Coord(pos.GetX() - data.Length/2.0, pos.GetY());
			InsertStringToCharArray(screen, pos, data);
		}

		//print character at pos
		private static void InsertStringToCharArray(char[,] screen, Coord pos, char data)
		{
			screen[(int) pos.GetY(),(int) pos.GetX()] = data;
		}

		//prints string starting at pos
		private static void InsertStringToCharArray(char[,] screen, Coord pos, string data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				screen[(int) pos.GetY(),(int) pos.GetX() + i] = data[i];
			}
		}

		//determines the height of a binary tree
		private static int GetHeight(IPrintableBinaryNode theGuy)
		{
			if (theGuy == null) return 0;
			return 1 + Math.Max(GetHeight(theGuy.GetLeft()), GetHeight(theGuy.GetRight()));
		}
	}

	//Immutable
	//Allows for some vector math
	internal class Coord
	{
		public double X { get; private set; }
		public double Y { get; private set; }

		public Coord(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public double GetX()
		{
			return X;
		}

		public double GetY()
		{
			return Y;
		}

		public Coord Add(int x, int y)
		{
			return new Coord(this.X + x, this.Y + y);
		}
	}
}