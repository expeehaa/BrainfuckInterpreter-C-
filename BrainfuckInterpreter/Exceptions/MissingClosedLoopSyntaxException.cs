namespace BrainfuckInterpreter.Exceptions {
	public class MissingClosedLoopSyntaxException : SyntaxException {
		public MissingClosedLoopSyntaxException(string code) : base(code) { }
	}
}
