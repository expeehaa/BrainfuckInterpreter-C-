using System;

namespace BrainfuckInterpreter {
	public class Interpreter {
		public InterpretationContext Context { get; }

		public Interpreter(string code, string input) {
			Context = new InterpretationContext(code, input);
		}

		public string Interpret(){
			Context.Reset();
			
			if(!string.IsNullOrWhiteSpace(Context.Code)) {
				while(Context.Position < Context.Code.Length) {
					switch(Context.Code[Context.Position]) {
						case '+':
							if(Context.Memory[Context.Pointer] >= short.MaxValue) Context.Memory[Context.Pointer] = 0;
							else Context.Memory[Context.Pointer]++;
							break;
						case '-':
							if(Context.Memory[Context.Pointer] <= 0) Context.Memory[Context.Pointer] = short.MaxValue;
							else Context.Memory[Context.Pointer]--;
							break;
						case '>':
							if(Context.Pointer >= 0xFFFF) Context.Pointer = 0;
							else Context.Pointer++;
							break;
						case '<':
							if(Context.Pointer <= 0) Context.Pointer = 0xFFFF - 1;
							else Context.Pointer--;
							break;
						case '.':
							Context.Output += (char)Context.Memory[Context.Pointer];
							break;
						case ',':
							Context.Memory[Context.Pointer] = (short)Context.Input[Context.InputPointer];
							Context.InputPointer++;
							break;
						case '[':
							var loopEnd = Context.Position + 1;
							Context.BracketsOpen = 1;
							Context.BracketsClosed = 0;

							while(loopEnd <= Context.Code.Length && Context.BracketsOpen != Context.BracketsClosed) {
								if(Context.Code[loopEnd] == '[') {
									Context.BracketsOpen++;
								} else if(Context.Code[loopEnd] == ']') {
									Context.BracketsClosed++;
								}

								loopEnd++;
							}

							if(loopEnd > Context.Code.Length) {
								Console.WriteLine($"No end of loop found for loop start at {Context.Position}!");
								return "";
							}
							if(Context.Memory[Context.Pointer] == 0) {
								Context.Position = loopEnd;
							}
							break;
						case ']':
							var loopStart = Context.Position - 1;
							Context.BracketsOpen = 0;
							Context.BracketsClosed = 1;

							while(loopStart >= 0 && Context.BracketsOpen != Context.BracketsClosed) {
								if(Context.Code[loopStart] == '[') {
									Context.BracketsOpen++;
								} else if(Context.Code[loopStart] == ']') {
									Context.BracketsClosed++;
								}

								loopStart--;
							}

							if(loopStart < 0) {
								Console.WriteLine($"No start of loop found for loop end at {Context.Position}!");
								return "";
							}

							if(Context.Memory[Context.Pointer] != 0) {
								Context.Position = loopStart;
							}

							break;
						default:
							break;
					}

					Context.Position++;
				}

				return Context.Output;
			} else {
				return "";
			}
		}
	}
}
