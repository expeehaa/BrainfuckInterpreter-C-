using System;

namespace BrainfuckInterpreter.Exceptions {
	public class SyntaxException : Exception {
		public string Code { get; }
		
		public SyntaxException(string code) {
			Code = code;
		}

		public SyntaxException(string code, string message) : base(message) {
			Code = code;
		}
	}
}
