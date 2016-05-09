using System;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Structures
{
	[Obsolete]
	public class Tree<out T>
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
		/// The node which contains this leaf of the tree
		/// </summary>
		/// <value>The parent</value>
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
		/// Initializes a new instance of the <see cref="HumDrum.Structures.Tree`1"/> class.
		/// This allows you to set what the left and right branches are in the constructor.
		/// </summary>
		/// <param name="currentValue">The value of this "leaf"</param>
		/// <param name="parent">The parent branch</param>
		/// <param name="left">The left branch</param>
		/// <param name="right">The right branch</param>
		public Tree (T currentValue, Tree<T> parent, Tree<T> left, Tree<T> right) : this(currentValue, parent)
		{
			LeftBranch = left;
			RightBranch = right;
		}

		/// <summary>
		/// Determines whether this instance is the root parent of this tree
		/// </summary>
		/// <returns><c>true</c> if this instance is parent; otherwise, <c>false</c>.</returns>
		public bool IsRootParent()
		{
			return (Parent == null);
		}

		/// <summary>
		/// Determines whether this instance is parent.
		/// </summary>
		/// <returns><c>true</c> if this instance is parent; otherwise, <c>false</c>.</returns>
		public bool IsParent()
		{
			return (LeftBranch != null) || (RightBranch != null);
		}

		/// <summary>
		/// Tests to see if both the left and right branch are Empty.
		/// </summary>
		/// <returns><c>true</c>, if iso parent was ised, <c>false</c> otherwise.</returns>
		public bool IsIsoParent()
		{
			return (LeftBranch != null) && (RightBranch != null);
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
		/// Flattens the entire tree into a list of its Nodes.
		/// </summary>
		public IEnumerable<T> Flatten(){
			if (RightBranch == null && LeftBranch == null)
				return Transformations.Wrap<T> (CurrentNode);
			else
				return Transformations.Concatenate( // Returns the current node and all the other nodes
					Transformations.Wrap(CurrentNode),
					Transformations.Concatenate (RightBranch.Flatten (), LeftBranch.Flatten ()));
		}

		/// <summary>
		/// Destroy all sub-trees from this node, not including this one.
		/// </summary>
		public void Snip(){
			LeftBranch  = null;
			RightBranch = null;
		}

		/// <summary>
		/// Map the specified function over this tree, returning the result.
		/// </summary>
		/// <param name="function">The resulting tree</param>
		public void Map(Func<T> function){

			// Base case - no children
			if(!(LeftBranch.IsParent()) && !(RightBranch.IsParent()))
				CurrentNode = function(CurrentNode);

			if (LeftBranch.IsParent ())
				LeftBranch.Map (function);
			if (RightBranch.IsIsoParent ())
				RightBranch.Map (function);
			
		}

		/// <summary>
		/// Coalesces the tree from the bottom up. This will
		/// take a branch with 2 outer leaves, and perform an operation on
		/// them, making them the new CurrentNode. Performs in place
		/// 
		/// If a Node does not have 2 children, that part of the tree is ignored.
		/// This is only one iteration of Coalesce.
		/// </summary>
		/// <param name="coalescor">Coalescor - the function that takes 2 Nodes and returns a result</param>
		public void Coalesce(Func<T, T, T> coalescor, Direction firstArgument)
		{
			bool leftFirst = firstArgument.Equals (Direction.LEFT);

			// Base case - 2 children, neither of which are parents.
			if (IsIsoParent () && !(LeftBranch.IsIsoParent ()) && !(RightBranch.IsIsoParent ())) {
				CurrentNode = coalescor (
					(leftFirst ? LeftBranch : RightBranch).CurrentNode,
					(leftFirst ? RightBranch : LeftBranch).CurrentNode);
				
				LeftBranch = null;
				RightBranch = null;
			}

			if (LeftBranch.IsIsoParent ())
				LeftBranch.Coalesce (coalescor, firstArgument);
			if (RightBranch.IsIsoParent ())
				RightBranch.Coalesce (coalescor, firstArgument);
			
		}

		/// <summary>
		/// Coalesces the tree from the bottom up. This will
		/// take a branch with 2 outer leaves, and perform an operation on
		/// them with the parent node's value, replacing it. This runs in place.
		/// 
		/// If a Node does not have 2 children, that part of the tree is ignored.
		/// This is only one iteration of Coalesce.
		/// </summary>
		/// <param name="coalescor">Coalescor - the function that takes 2 Nodes and returns a result</param>
		public void Coalesce(Func<T, T, T, T> coalescor, Direction firstArgument)
		{
			bool leftFirst = firstArgument.Equals (Direction.LEFT);

			// Base case - 2 children, neither of which are parents.
			if (IsIsoParent () && !(LeftBranch.IsIsoParent ()) && !(RightBranch.IsIsoParent ())) {
				CurrentNode = coalescor (
					(leftFirst ? LeftBranch : RightBranch).CurrentNode,
					(leftFirst ? RightBranch : LeftBranch).CurrentNode);

				LeftBranch = null;
				RightBranch = null;
			}

			if (LeftBranch.IsParent() ())
				LeftBranch.Coalesce (coalescor, firstArgument);
			if (RightBranch.IsParent())
				RightBranch.Coalesce (coalescor, firstArgument);

		}
			
		/// <summary>
		/// Outers the leaves.
		/// </summary>
		/// <returns>The leaves.</returns>
		public IEnumerable<T> OuterLeaves()
		{
			
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

