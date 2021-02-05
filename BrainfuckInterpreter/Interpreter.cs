using System;

namespace BrainfuckInterpreter {
	public class Interpreter {
		public string Code  { get; }
		public string Input { get; }

		public Interpreter(string code, string input) {
			Code  = code;
			Input = input;
		}

		public string Interpret(){
			if(!string.IsNullOrWhiteSpace(Code)) {
				var position = 0;
				var memory = new short[0xFFFF];
				var pointer = 0;
				var inputPointer = 0;
				var output = "";

				int bracketsOpen, bracketsClosed;
				while(position < Code.Length) {
					switch(Code[position]) {
						case '+':
							if(memory[pointer] >= short.MaxValue) memory[pointer] = 0;
							else memory[pointer]++;
							break;
						case '-':
							if(memory[pointer] <= 0) memory[pointer] = short.MaxValue;
							else memory[pointer]--;
							break;
						case '>':
							if(pointer >= 0xFFFF) pointer = 0;
							else pointer++;
							break;
						case '<':
							if(pointer <= 0) pointer = 0xFFFF - 1;
							else pointer--;
							break;
						case '.':
							output += (char)memory[pointer];
							break;
						case ',':
							memory[pointer] = (short)Input[inputPointer];
							inputPointer++;
							break;
						case '[':
							var loopEnd = position + 1;
							bracketsOpen = 1;
							bracketsClosed = 0;

							while(loopEnd <= Code.Length && bracketsOpen != bracketsClosed) {
								if(Code[loopEnd] == '[') {
									bracketsOpen++;
								} else if(Code[loopEnd] == ']') {
									bracketsClosed++;
								}

								loopEnd++;
							}

							if(loopEnd > Code.Length) {
								Console.WriteLine($"No end of loop found for loop start at {position}!");
								return "";
							}
							if(memory[pointer] == 0) {
								position = loopEnd;
							}
							break;
						case ']':
							var loopStart = position - 1;
							bracketsOpen = 0;
							bracketsClosed = 1;

							while(loopStart >= 0 && bracketsOpen != bracketsClosed) {
								if(Code[loopStart] == '[') {
									bracketsOpen++;
								} else if(Code[loopStart] == ']') {
									bracketsClosed++;
								}

								loopStart--;
							}

							if(loopStart < 0) {
								Console.WriteLine($"No start of loop found for loop end at {position}!");
								return "";
							}

							if(memory[pointer] != 0) {
								position = loopStart;
							}

							break;
						default:
							break;
					}

					position++;
				}

				return output;
			} else {
				return "";
			}
		}
	}
}
