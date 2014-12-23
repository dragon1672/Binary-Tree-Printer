using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binary_Tree_Printer;

namespace CPrintTester
{
	class Program
	{
		private class Node : IPrintableBinaryNode
		{
			private readonly int _num;
			public Node Left { get; set; }
			public Node Right { get; set; }

			public Node(int num) { this._num = num; }

			public IPrintableBinaryNode GetLeft() { return Left; }
			public IPrintableBinaryNode GetRight() { return Right; }
			public String GetString() { return "" + _num; }
		}
		private class ExampleBalancedTree
		{
			public readonly Node Head = new Node(0);
			public ExampleBalancedTree()
			{
				Head.Left = new Node(1) {Left = new Node(3), Right = new Node(4)};
				Head.Right = new Node(2) {Left = new Node(5), Right = new Node(6)};
			}
		}
		private class ExampleUnBalancedTree
		{
			public readonly Node Head = new Node(0);
			public ExampleUnBalancedTree()
			{
				Head.Left = new Node(1) {Left = new Node(2) { Left = new Node(4), Right = null }, Right = new Node(3)};
			}
		}
		static void Main(string[] args)
		{
			Console.WriteLine("Balanced");
			BinaryTreePrinter.Print(new ExampleBalancedTree().Head,1);
			Console.WriteLine("Unbalanced");
			BinaryTreePrinter.Print(new ExampleUnBalancedTree().Head,1);
		}
	}
}
