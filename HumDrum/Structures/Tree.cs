using System;
using System.Collections.Generic;

using HumDrum.Recursion;

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

		public Tree<T> Parent { get; set; }

		/// <summary>
		/// Initializes a new instance of a tree
		/// </summary>
		public Tree (T currentValue, Tree<T> parent)
		{
			CurrentNode = currentValue;

			LeftBranch = null;
			RightBranch = null;

			Parent = parent;
		}

		/// <summary>
		/// Get the value from the branch in the given direction
		/// </summary>
		/// <returns>The value</returns>
		/// <param name="direction">LEFT or RIGHT</param>
		public T GetValue(Direction direction){
			switch (direction)
			{
			case Direction.LEFT:
				return LeftBranch.CurrentNode;
			case Direction.RIGHT:
				return RightBranch.CurrentNode;
			case Direction.UP:
				return Parent.CurrentNode;
			}

			return default(T);
		}

		/// <summary>
		/// Grow the tree in the direction with a seed value
		/// </summary>
		/// <param name="value">The value to use</param>
		/// <param name="direction">Which direction to grow</param>
		public void Grow(T value, Direction direction){
			if (direction == Direction.RIGHT)
				RightBranch = new Tree<T> (value, this);
			else if (direction == Direction.LEFT)
				LeftBranch = new Tree<T> (value, this);
		}

		/// <summary>
		/// Flattens the entire tree into a list of its Nodes. Working on these directly
		/// affects the information in the tree, so be careful.
		/// </summary>
		public List<T> Flatten(){
			if (RightBranch == null && LeftBranch == null)
				return TailHelper.Wrap (CurrentNode);
			else
				return TailHelper.Concatenate( // Returns the current node and all the other nodes
					TailHelper.Wrap(CurrentNode),
					TailHelper.Concatenate (RightBranch.Flatten (), LeftBranch.Flatten ()));
		}

		/// <summary>
		/// Destroy all sub-trees from this node, not including this one.
		/// </summary>
		public void Snip(){
			LeftBranch  = null;
			RightBranch = null;
		}

		/// <summary>
		/// Map the specified function over this tree
		/// </summary>
		/// <param name="function">The function to map</param>
		public void Map(Action<T> function){
			Flatten ().ForEach (function);
		}

		/// <summary>
		/// "Prune" branches of this tree after the predicate returns false.
		/// </summary>
		/// <param name="pred">Pred.</param>
		public void Prune(Predicate<T> pred){
			if (!pred (CurrentNode))
				Parent.Snip ();
			else {
				LeftBranch.Prune (pred);
				RightBranch.Prune (pred);
			}
		}
	}
}

