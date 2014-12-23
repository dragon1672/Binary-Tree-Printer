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
	public interface IPrintableBinaryNode
	{
		/// <summary>
		/// Returns Node to the left of current node
		/// </summary>
		/// <returns>Node left of current</returns>
		IPrintableBinaryNode GetLeft();
		/// <summary>
		/// Returns Node to the right of current node
		/// </summary>
		/// <returns>Node right of current</returns>
		IPrintableBinaryNode GetRight();
		/// <summary>
		/// the data of the node represented as a string
		/// </summary>
		/// <returns>the data of the node</returns>
		string GetString();
	}
}
