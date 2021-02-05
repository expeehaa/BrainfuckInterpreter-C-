namespace BrainfuckInterpreter {
	public class InterpretationContext {
		public string  Code         { get; }
		public string  Input        { get; }
		public int     Position     { get; set; }
		public short[] Memory       { get; set; }
		public int     Pointer      { get; set; }
		public int     InputPointer { get; set; }
		public string  Output       { get; set; }

		public char CurrentCommand => Code[Position];

		public int BracketsOpen   { get; set; }
		public int BracketsClosed { get; set; }

		public InterpretationContext(string code, string input) {
			Code  = code;
			Input = input;

			Reset();
		}

		public void Reset() {
			Position     = 0;
			Memory       = new short[0xFFFF];
			Pointer      = 0;
			InputPointer = 0;
			Output       = "";

			BracketsOpen   = 0;
			BracketsClosed = 0;
		}
	}
}
