namespace BrainfuckInterpreter.Exceptions {
	public class EarlyClosedLoopSyntaxException : SyntaxException {
		public int EarlyLoopClosePosition { get; }
		
		public EarlyClosedLoopSyntaxException(string code, int earlyLoopClosePosition) : base(code) {
			EarlyLoopClosePosition = earlyLoopClosePosition;
		}
	}
}
