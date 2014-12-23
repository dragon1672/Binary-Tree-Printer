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
		private const char LEFT_DOWN = '/';
		private const char RIGHT_DOWN = '\\';
		private const char SIDE_WAYS = '-';

		private static char LEFT_DOWN_PLACEHOLDER = (char) 1;
		private static char RIGHT_DOWN_PLACEHOLDER = (char) 2;
		private static char SIDE_WAYS_PLACEHOLDER = (char) 3;
		private static char SPACE_PLACEHOLDER = (char) 4;

		private static Dictionary<char, char> replaceChars = new Dictionary<char, char>()
		{
			{LEFT_DOWN_PLACEHOLDER, LEFT_DOWN},
			{RIGHT_DOWN_PLACEHOLDER, RIGHT_DOWN},
			{SIDE_WAYS_PLACEHOLDER, SIDE_WAYS},
			{SPACE_PLACEHOLDER, ' '},
		};

		private static HashSet<char> trashable = new HashSet<char>()
		{
			SIDE_WAYS_PLACEHOLDER,
			SPACE_PLACEHOLDER
		};


		/// <summary>
		/// returns a 2D char array of the binary tree
		/// </summary>
		/// <param name="head">root to print from</param>
		/// <param name="nodePrintLength">max space allocated for printing node's data</param>
		/// <returns></returns>
		public static char[,] getCharArray(IPrintableBinaryNode head, int nodePrintLength)
		{
			return getCharArray(head, getHeight(head), nodePrintLength);
		}


		/// <summary>
		/// returns a 2D char array of the binary tree
		/// </summary>
		/// <param name="head">root to print from</param>
		/// <param name="treeHeight">depth to print tree</param>
		/// <param name="nodePrintLength">max space allocated for printing node's data</param>
		/// <returns></returns>
		public static char[,] getCharArray(IPrintableBinaryNode head, int treeHeight, int nodePrintLength)
		{
			//if print length is odd number everything can be centered properly (easier math)
			if (nodePrintLength%2 == 0) nodePrintLength++;

			int printHeight = treeHeight + 1; // plus 1 for copyright
			int printWidth = getWidth(treeHeight, nodePrintLength);

			//increase width if needed to fix copyright
			string copyRight = "Tree display provided by Anthony Corbin";
			printWidth = Math.Max(printWidth, copyRight.Length); // must be at least the length of the copyright

			//allocating required space
			char[,] screen = new char[printHeight, printWidth];
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					screen[i, j] = SPACE_PLACEHOLDER;
				}
			}

			//print tree starting in the middle of the screen
			printToArray(screen, head, treeHeight, new Coord(printWidth/2, 0), nodePrintLength);

			//print copyright
			insertStringToCharArray(screen, new Coord(0, printHeight - 1), copyRight);
			return replacePlaceHolder(trim(screen));
		}

		public static char[,] replacePlaceHolder(char[,] screen)
		{
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					if (replaceChars.ContainsKey(screen[i,j]))
					{
						screen[i,j] = replaceChars[screen[i,j]];
					}
				}
			}
			return screen;
		}

		public static char[,] trim(char[,] screen)
		{
			HashSet<int> colsToKill = new HashSet<int>();
			int height = screen.GetLength(0);
			int width = screen.GetLength(1);
			for (int x = 0; x < width; x++)
			{
				bool canTrash = true;
				for (int y = 0; y < height && canTrash; y++)
				{
					canTrash = trashable.Contains(screen[y,x]);
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



		//collapses a 2D character array into a string
		//nodePrintLength is the size given to each node to print
		public static string getString(IPrintableBinaryNode head, int nodePrintLength)
		{
			return getString(head, getHeight(head), nodePrintLength);
		}

		//returns printout of tring treeHeight levels deep
		//nodePrintLength is the size given to each node to print
		public static string getString(IPrintableBinaryNode head, int treeHeight, int nodePrintLength)
		{
			// get printout
			char[,] screen = getCharArray(head, treeHeight, nodePrintLength);

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



		public static void print(IPrintableBinaryNode head, int nodePrintLength)
		{
			print(head, getHeight(head), nodePrintLength);
		}

		public static void print(IPrintableBinaryNode head, int treeHeight, int nodePrintLength)
		{
			// get printout
			char[,] screen = getCharArray(head, treeHeight, nodePrintLength);

			//print
			char[] tmep = new char[1];
			for (int i = 0; i < screen.GetLength(0); i++)
			{
				for (int j = 0; j < screen.GetLength(1); j++)
				{
					char toPrint = screen[i,j] == SPACE_PLACEHOLDER ? ' ' : screen[i,j]; // replace uninitialized with ' '
					Console.Write(toPrint);
				}
				Console.Write("\n");
			}
		}



		//returns the required width to print given tree
		private static int getWidth(int height, int nodePrintLength)
		{
			// also try out 3 for a at least 1 - in each tree level
			int spacing = 1;
			return (spacing + nodePrintLength)*(int) Math.Pow(2, height - 1) - 1; // trust my math
		}

		//prints tree to char[][] starting at pos
		private static void printToArray(char[,] screen, IPrintableBinaryNode node, int height, Coord pos, int dataLength)
		{
			if (node == null) return;

			//print node data
			string data = node.getString();
			insertStringToCharArrayCentered(screen, pos, data);
			//should traverse
			if (height != 1)
			{
				//trust the math
				int lastWidth = getWidth(height - 1, dataLength);
				int numOfDashes = (lastWidth - 1)/2 - ((dataLength - 1)/2 - 1);

				int offset = (dataLength - 1)/2;

				//printing the pretty
				for (int i = 1; i <= numOfDashes; i++)
				{
					char printL = i == numOfDashes ? LEFT_DOWN_PLACEHOLDER : SIDE_WAYS_PLACEHOLDER; // character to be printed
					char printR = i == numOfDashes ? RIGHT_DOWN_PLACEHOLDER : SIDE_WAYS_PLACEHOLDER; // character to be printed
					offset += 1;
					if (node.getLeft() != null) insertStringToCharArray(screen, pos.add(-offset, 0), printL); // PRINT!!
					if (node.getRight() != null) insertStringToCharArray(screen, pos.add(offset, 0), printR); // PRINT!!
				}
				//recourse for each branch with position at the end of each branch +1 in the y
				printToArray(screen, node.getLeft(), height - 1, pos.add(-offset, 1), dataLength);
				printToArray(screen, node.getRight(), height - 1, pos.add(offset, 1), dataLength);
			}
		}

		//centers given string on pos
		private static void insertStringToCharArrayCentered(char[,] screen, Coord pos, string data)
		{
			pos = new Coord(pos.getX() - data.Length/2, pos.getY());
			insertStringToCharArray(screen, pos, data);
		}

		//print character at pos
		private static void insertStringToCharArray(char[,] screen, Coord pos, char data)
		{
			screen[(int) pos.getY(),(int) pos.getX()] = data;
		}

		//prints string starting at pos
		private static void insertStringToCharArray(char[,] screen, Coord pos, string data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				screen[(int) pos.getY(),(int) pos.getX() + i] = data[i];
			}
		}

		//determines the height of a binary tree
		private static int getHeight(IPrintableBinaryNode theGuy)
		{
			if (theGuy == null) return 0;
			return 1 + Math.Max(getHeight(theGuy.getLeft()), getHeight(theGuy.getRight()));
		}
	}

	//Immutable
	//Allows for some vector math
	internal class Coord
	{
		private double x;
		private double y;

		public Coord(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public double getX()
		{
			return x;
		}

		public double getY()
		{
			return y;
		}

		public Coord add(int x, int y)
		{
			return new Coord(this.x + x, this.y + y);
		}
	}
}