using System;
using System.Collections.Generic;
using HumDrum.Collections;

namespace HumDrum.Structures
{
	public class Tree<T>
	{
		/// <summary>
		/// This current node
		/// </summary>
		public T CurrentNode { get; set; }

		/// <summary>
		/// The left branch
		/// </summary>
		/// <value>The left branch.</value>
		public Tree<T> LeftBranch { get; set; }

		/// <summary>
		/// The right branch of this binary tree
		/// </summary>
		/// <value>The right branch.</value>
		public Tree<T> RightBranch {get; set;}

		/// <summary>
		/// Initializes a new instance of a tree
		/// </summary>
		public Tree (T currentValue)
		{
			CurrentNode = currentValue;

			LeftBranch = null;
			RightBranch = null;
		}

		/// <summary>
		/// Get the value from the branch in the given direction
		/// </summary>
		/// <returns>The value</returns>
		/// <param name="direction">LEFT or RIGHT</param>
		public T GetValue(Direction direction){
			return direction == Direction.RIGHT ? RightBranch.CurrentNode : LeftBranch.CurrentNode;
		}

		/// <summary>
		/// Grow the tree in the direction with a seed value
		/// </summary>
		/// <param name="value">The value to use</param>
		/// <param name="direction">Which direction to grow</param>
		public void Grow(T value, Direction direction){
			if (direction == Direction.RIGHT)
				RightBranch = new Tree<T> (value);
			else if (direction == Direction.LEFT)
				LeftBranch = new Tree<T> (value);
		}

		/// <summary>
		/// Flattens the entire tree into a list of its Nodes. Working on these directly
		/// affects the information in the tree, so be careful.
		/// </summary>
		public List<T> Flatten(){
			if (RightBranch == null && LeftBranch == null)
				return new List<T>(Recursion.Wrap (CurrentNode));
			else
				return Recursion.Concatenate( // Returns the current node and all the other nodes
					Recursion.Wrap(CurrentNode),
					Recursion.Concatenate (RightBranch.Flatten (), LeftBranch.Flatten ()));
		}

		public List<T> Map(Func<T> function){
			//TODO: Add T,W tree with 2 current nodes.
			// you can implement a MathTree by having an Operation (Power, Multiply, Divide, Add, Subtract) and a number. OR
			// have Operations wrapped in a MathOperation base class. Have Operator and Number (double) be the two inherited classes
			// and do stuff that way.
		}
	}
}