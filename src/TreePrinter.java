/**
 * Created by Anthony on 8/11/2014.
 */
public class TreePrinter {
	public static char[][] prettyPrint(BinaryNode head, int nodePrintLength) {
		return prettyPrint(head,getHeight(head),nodePrintLength);
	}
	public static char[][] prettyPrint(BinaryNode head, int treeHeight, int nodePrintLength) {
		String copyRight = "Tree display provided by Anthony Corbin";
		int printHeight = treeHeight+1; // plus 1 for copyright
		int printWidth  = getWidth(treeHeight,nodePrintLength);
		printWidth  = Math.max(printWidth,copyRight.length()); // must be at least the length of the copyright
		char [][] screen = new char[printHeight][printWidth];
		//print tree
		printToArray(screen,head,treeHeight,new Coord(printWidth / 2,0),nodePrintLength);
		//print copyright
		insertStringToCharArray(screen,new Coord(0,printHeight-1),copyRight);
		return screen;
	}
	public static String getString(BinaryNode head, int nodePrintLength) {
		return getString(head, getHeight(head), nodePrintLength);
	}
	public static String getString(BinaryNode head, int treeHeight, int nodePrintLength) {
		if(nodePrintLength % 2 == 0) nodePrintLength++;
		char [][] screen = prettyPrint(head,treeHeight,nodePrintLength);
		StringBuilder ret = new StringBuilder();
		char [] tmep = new char[1];
		for(int i=0;i<screen.length;i++) {
			for(int j=0;j<screen[i].length;j++) {
				char toPrint = screen[i][j] == tmep[0] ? ' ' : screen[i][j];
				ret.append(toPrint);
			}
			ret.append("\n");
		}
		return ret.toString();
	}
	//returns the required width to print given tree
	private static int getWidth(int height, int nodePrintLength) {
		// also try out 3 for a at least 1 - in each tree level
		int spacing = 1;
		return (spacing + nodePrintLength)*(int)Math.pow(2,height-1) - 1; // trust my math
	}

	//prints tree to screen starting at pos
	private static void printToArray(char[][]screen, BinaryNode node,int height,Coord pos, int dataLength) {
		if(node == null) return;

		//print node data
		String data = node.getString();
		insertStringToCharArrayCentered(screen,pos,data);
		//should traverse
		if(height != 1) {
			//trust the math
			int lastWidth = getWidth(height-1,dataLength);
			int numOfDashes = (lastWidth-1)/2 - ((dataLength-1)/2 -1);
			int offset = (dataLength-1)/2;

			//keeps track of left/right movement
			int leftOffset  = -offset;
			int rightOffset =  offset;
			//printing the pretty
			for(int i=1;i<=numOfDashes;i++) {
				char printL = i == numOfDashes ? '/'  : '-'; // character to be printed
				char printR = i == numOfDashes ? '\\' : '-'; // character to be printed
				leftOffset  -= 1;
				rightOffset += 1;
				if(node.getLeft()  != null) insertStringToCharArray(screen,pos.add(leftOffset,0),printL);  // PRINT!!
				if(node.getRight() != null) insertStringToCharArray(screen,pos.add(rightOffset,0),printR); // PRINT!!
			}
			//recourse for each branch
			printToArray(screen,node.getLeft(), height-1,pos.add(leftOffset,1),dataLength);
			printToArray(screen,node.getRight(),height-1,pos.add(rightOffset,1),dataLength);
		}
	}
	//centers given string on pos
	private static void insertStringToCharArrayCentered(char[][]screen,Coord pos,String data) {
		pos = new Coord(pos.x - data.length() / 2,pos.y);
		insertStringToCharArray(screen,pos,data);
	}
	//print character at pos
	private static void insertStringToCharArray(char[][]screen,Coord pos,char data) {
		screen[(int)pos.getY()][(int)pos.getX()] = data;
	}
	//prints string starting at pos
	private static void insertStringToCharArray(char[][]screen,Coord pos,String data) {
		for(int i=0;i<data.length();i++) {
			screen[(int)pos.getY()][(int)pos.getX() + i] = data.charAt(i);
		}
	}

	//determines the height of a binary tree
	private static int getHeight(BinaryNode theGuy) {
		if(theGuy == null) return 0;
		return 1 + Math.max(getHeight(theGuy.getLeft()),getHeight(theGuy.getRight()));
	}
	//since I don't have glm::vec2
	private static class Coord {
		private double x;
		private double y;
		public Coord(double x, double y) {
			this.x = x;
			this.y = y;
		}
		public double getX() { return x; }
		public double getY() { return y; }
		public Coord add(int x, int y) { return add(new Coord(x,y)); }
		public Coord add(Coord that)   { return new Coord(this.x + that.x, this.y + that.y); }
		public Coord mult(float l)     { return new Coord((int)(l * this.x),(int)( l * this.y)); }
	}
}

