using System;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Structures
{
	/// <summary>
	/// An implementation of a binary tree.
	/// </summary>
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
		/// The node which contains this leaf of the tree
		/// </summary>
		/// <value>The parent</value>
		public Tree<T> Parent { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Structures.Tree`1"/> class.
		/// This sets the value of the root node
		/// </summary>
		/// <param name="currentValue">The value of the parent node</param>
		public Tree (T currentValue)
		{
			Parent = null;
			CurrentNode = currentValue;
		}

		/// <summary>
		/// Initializes a new instance of a tree
		/// </summary>
		protected Tree (T currentValue, Tree<T> parent)
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
		protected Tree (T currentValue, Tree<T> parent, Tree<T> left, Tree<T> right) : this(currentValue, parent)
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
		/// Get the value from the branch in the given direction.
		/// 
		/// UP: The parent's value.
		/// RIGHT: The RightBranch's value
		/// LEFT: The LeftBranch's value
		/// DOWN: The current value
		/// </summary>
		/// <returns>The value</returns>
		/// <param name="direction">LEFT or RIGHT or UP or DOWN</param>
		public T GetValue(Direction direction){
			switch (direction)
			{
			case Direction.LEFT:
				return LeftBranch.CurrentNode;
			case Direction.RIGHT:
				return RightBranch.CurrentNode;
			case Direction.UP:
				return Parent.CurrentNode;
			case Direction.DOWN:
				return this.CurrentNode;
			}

			return default(T);
		}

		/// <summary>
		/// Which direction on this tree is that branch. Returns down
		/// as a default.
		/// </summary>
		/// <param name="branch">The branch to test</param>
		public Direction Which(Tree<T> branch)
		{
			if (LeftBranch != null && LeftBranch.Equals (branch))
				return Direction.LEFT;
			if (RightBranch != null && RightBranch.Equals (branch))
				return Direction.RIGHT;
			if (Parent != null && Parent.Equals (branch))
				return Direction.UP;
			
			return Direction.DOWN;
		}

		/// <summary>
		/// Tests how many elements this tree has
		/// </summary>
		/// <returns>The total number of members that this tree has</returns>
		public int TotalMembers()
		{
			if (LeftBranch == null && RightBranch == null)
				return 1;
			else if (RightBranch == null) // Implies left is not null
				return 1 + LeftBranch.TotalMembers ();
			else if (LeftBranch == null) // Implies right is not null
				return 1 + RightBranch.TotalMembers ();
			else
				return 1 + LeftBranch.TotalMembers () + RightBranch.TotalMembers ();
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
		/// Grow into both directions at once
		/// </summary>
		/// <param name="left">The left value</param>
		/// <param name="right">The right value</param>
		public void Grow(T left, T right)
		{
			LeftBranch = new Tree<T> (left, this);
			RightBranch = new Tree<T> (right, this);
		}

		/// <summary>
		/// Returns all of the trees found nested "level" levels into
		/// the binary tree. The root node is level 0.
		/// </summary>
		/// <param name="level">Level.</param>
		public IEnumerable<Tree<T>> Level(int level)
		{
			if (level == 0)
				return Transformations.Wrap (this);

			if (LeftBranch == null && RightBranch == null)
				return new List<Tree<T>> ();
			if(LeftBranch == null) // Implies right is not null
				return RightBranch.Level(level - 1);
			if(RightBranch == null) // Implies left is not null
				return LeftBranch.Level(level - 1);

			// Base Case: Both branches are full
			return Transformations.Concatenate(LeftBranch.Level(level - 1), RightBranch.Level(level - 1));
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
		/// Cuts the tree off at a certain level
		/// </summary>
		/// <returns>The tree up to that point</returns>
		/// <param name="level">The level to not include the children of</param>
		public void Snip(int level)
		{
			foreach (Tree<T> tree in Level(level))
				tree.Snip();
		}

		/// <summary>
		/// Snip only a specific direction off of this tree
		/// </summary>
		/// <param name="d">The direction to snip off</param>
		public void Snip(Direction d)
		{
			switch (d) {
			case Direction.LEFT:
				LeftBranch = null;
				break;
			case Direction.RIGHT:
				RightBranch = null;
				break;
			}
		}
		/// <summary>
		/// Map the specified function over this tree, returning the result.
		/// </summary>
		/// <param name="function">The resulting tree</param>
		public void Map(Func<T, T> function){

			// Base case - no children
			CurrentNode = function(CurrentNode);

			if (LeftBranch != null)
				LeftBranch.Map (function);
			if (RightBranch != null)
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

			// Base case - 2 children, neither of which are isometric parents.
			if (IsIsoParent () && !(LeftBranch.IsIsoParent ()) && !(RightBranch.IsIsoParent ())) {
				CurrentNode = coalescor (
					(leftFirst ? LeftBranch : RightBranch).CurrentNode,
					(leftFirst ? RightBranch : LeftBranch).CurrentNode);
				
				LeftBranch = null;
				RightBranch = null;
			} else {
				if (LeftBranch.IsIsoParent ())
					LeftBranch.Coalesce (coalescor, firstArgument);
				if (RightBranch.IsIsoParent ())
					RightBranch.Coalesce (coalescor, firstArgument);
			}
		}

		/// <summary>
		/// Coalesces the tree from the bottom up. This will
		/// take a branch with 2 outer leaves, and perform an operation on
		/// them with the parent node's value, replacing it. This runs in place.
		/// 
		/// If a Node does not have 2 children, that part of the tree is ignored.
		/// This is only one iteration of Coalesce.
		/// 
		/// The value of the current node is considered in the third parameter of the coalescor.
		/// </summary>
		/// <param name="coalescor">Coalescor - the function that takes 2 Nodes and returns a result</param>
		public void Coalesce(Func<T, T, T, T> coalescor, Direction firstArgument)
		{
			bool leftFirst = firstArgument.Equals (Direction.LEFT);

			// Base case - 2 children, neither of which are parents.
			if (IsIsoParent () && !(LeftBranch.IsIsoParent ()) && !(RightBranch.IsIsoParent ())) {
				CurrentNode = coalescor (
					(leftFirst ? LeftBranch : RightBranch).CurrentNode,
					(leftFirst ? RightBranch : LeftBranch).CurrentNode,
					CurrentNode);

				LeftBranch = null;
				RightBranch = null;
			}
			else { 
				if(LeftBranch.IsParent())
					LeftBranch.Coalesce (coalescor, firstArgument);
				if (RightBranch.IsParent())
				RightBranch.Coalesce (coalescor, firstArgument);
			}

		}
			
		/// <summary>
		/// Return just the outer leaves of this tree. That is, leaves
		/// that have no children.
		/// </summary>
		/// <returns>The outer leaves</returns>
		public IEnumerable<T> OuterLeaves()
		{
			if (LeftBranch == null && RightBranch == null)
				return Transformations.Wrap (CurrentNode);
			else if(LeftBranch == null) // Implies right is not null
				return RightBranch.OuterLeaves();
			else if(RightBranch == null) // Implies left is not null
				return LeftBranch.OuterLeaves();
			else // Implies both are not null 
				return Transformations.Concatenate(LeftBranch.OuterLeaves(), RightBranch.OuterLeaves());
		}

		/// <summary>
		/// "Prune" branches of this tree after the predicate returns false.
		/// </summary>
		/// <param name="pred">Pred.</param>
		public void Prune(Predicate<T> pred){
			if (pred (CurrentNode))
				Parent.Snip (Parent.Which (this));
			else { 
				if (LeftBranch != null)
					LeftBranch.Prune (pred);

				if (RightBranch != null)
					RightBranch.Prune (pred);
			}
		}
	}
}

