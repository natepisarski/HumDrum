using System;

namespace HumDrum.Traits
{
	public class Exceptions
	{
		/// <summary>
		/// Thrown when a type is attempting to be initialized as a
		/// HumDrum.Traits.Class but is not declared as a class
		/// </summary>
		public class TypeNotClassException : Exception
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+TypeNotClassException"/> class.
			/// This will leave the message as "Type did not appear to be a class"
			/// </summary>
			public TypeNotClassException()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+TypeNotClassException"/> class.
			/// This gives control over the message
			/// </summary>
			/// <param name="message">The text for the Exception</param>
			public TypeNotClassException(string message) : base(message)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+TypeNotClassException"/> class.
			/// This is used when there is an inner exception to throw
			/// </summary>
			/// <param name="message">The message to set</param>
			/// <param name="inner">The inner exception</param>
			public TypeNotClassException(string message, Exception inner) : base(message, inner)
			{

			}
		}

		/// <summary>
		/// Thrown when a type is attempting to be initialized as a
		/// HumDrum.Traits.Interface but appears to not be
		/// </summary>
		public class TypeNotInterfaceException : Exception
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+TypeNotInterfaceException"/> class.
			/// This will leave the message as "Type did not appear to be an interface"
			/// </summary>
			public TypeNotInterfaceException()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+TypeNotInterfaceException"/> class.
			/// This gives control over the message
			/// </summary>
			/// <param name="message">The text for the Exception</param>
			public TypeNotInterfaceException(string message) : base(message)
			{

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+TypeNotInterfaceException"/> class.
			/// This is used when there is an inner exception to throw
			/// </summary>
			/// <param name="message">The message to set</param>
			/// <param name="inner">The inner exception</param>
			public TypeNotInterfaceException(string message, Exception inner) : base(message, inner)
			{

			}
		}

		/// <summary>
		/// Represents the lack of implementation of a Method that an 
		/// Interface specifies
		/// </summary>
		public class MethodNotImplementedException : Exception
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+MethodNotImplementedException"/> class.
			/// This will use the default message warning that a method has not been properly implemented.
			/// </summary>
			public MethodNotImplementedException()
			{
				
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="HumDrum.Traits.Exceptions+MethodNotImplementedException"/> class.
			/// 
			/// </summary>
			/// <param name="msg">Message.</param>
			public MethodNotImplementedException(string msg) : base(msg)
			{
				
			}

			public MethodNotImplementedException(string message, Exception inner) : base (message, inner)
			{

			}
		}
	}
}

