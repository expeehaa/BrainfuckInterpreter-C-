using System;

namespace BrainfuckInterpreter.Commands {
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class CommandAttribute : Attribute {
		public char[] Names { get; }
		
		public CommandAttribute(params char[] names) {
			Names = names;
		}
	}
}
