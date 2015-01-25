import org.junit.Test;

public class TreePrinterTest {

	@Test
	public void testGetString() throws Exception {
		System.out.println(TreePrinter.getString(new ExampleBalancedTree().head,1));
		System.out.println(TreePrinter.getString(new ExampleBalancedTree().head,5));

		System.out.println(TreePrinter.getString(new ExampleUnBalancedTree().head,1));
		System.out.println(TreePrinter.getString(new ExampleUnBalancedTree().head,5));

	}

	@Test
	public void testGetString1() throws Exception {
		System.out.println(TreePrinter.getString(new ExampleBalancedTree().head,2,1));
		System.out.println(TreePrinter.getString(new ExampleUnBalancedTree().head,2,1));
	}

	@Test
	public void TestGetString2() throws Exception {
		System.out.printf(TreePrinter.getString(new ReallyExampleBalancedTree().head,10));
	}

	private class Node implements BinaryNode {
		int num;
		Node left;
		Node right;

		Node(int num) { this.num = num; }

		public BinaryNode getLeft()  { return left;   }
		public BinaryNode getRight() { return right;  }
		public String getString()    { return ""+num; }

		@Override
		public String toString() {
			return TreePrinter.getString(this,10);
		}
	}
	private class ExampleBalancedTree {
		public Node head = new Node(0);
		public ExampleBalancedTree() {
			head.left  = new Node(1);
			head.right = new Node(2);
			head.left.left  = new Node(3);
			head.left.right = new Node(4);
			head.right.left  = new Node(5);
			head.right.right = new Node(6);
		}
	}
	private class ReallyExampleBalancedTree {
		public Node head = new Node(2);
		public ReallyExampleBalancedTree() {
			head.left  = new Node(7);
			head.right = new Node(1);
			head.left.left  = new Node(26);
			head.left.left.left  = new Node(90);
			head.left.left.right  = new Node(25);
			head.left.left.right.right  = new Node(19);
			head.left.left.right.right.right  = new Node(17);
			head.left.right  = new Node(3);
		}
	}
	private class ExampleUnBalancedTree {
		public Node head = new Node(0);
		public ExampleUnBalancedTree() {
			head.left  = new Node(1);
			head.left.left  = new Node(2);
			head.left.right  = new Node(3);
			head.left.left.left = new Node(4);
		}
	}
}