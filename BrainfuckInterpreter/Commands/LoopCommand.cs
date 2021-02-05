using BrainfuckInterpreter.Exceptions;
using System;

namespace BrainfuckInterpreter.Commands {
	[Command('[', ']')]
	public class LoopCommand : ICommand {
		public void Handle(InterpretationContext context) {
			if(context.CurrentCommand == '[') {
				var loopEnd = context.Position + 1;
				var bracketsOpen = 1;
				var bracketsClosed = 0;

				while(loopEnd <= context.Code.Length && bracketsOpen != bracketsClosed) {
					if(context.Code[loopEnd] == '[') {
						bracketsOpen++;
					} else if(context.Code[loopEnd] == ']') {
						bracketsClosed++;
					}

					loopEnd++;
				}

				if(loopEnd > context.Code.Length) {
					throw new Exception($"No end of loop found for loop start at {context.Position}!");
				}
				if(context.Memory[context.Pointer] == 0) {
					context.Position = loopEnd;
				}
			} else {
				var loopStart = context.Position - 1;
				var bracketsOpen = 0;
				var bracketsClosed = 1;

				while(loopStart >= 0 && bracketsOpen != bracketsClosed) {
					if(context.Code[loopStart] == '[') {
						bracketsOpen++;
					} else if(context.Code[loopStart] == ']') {
						bracketsClosed++;
					}

					loopStart--;
				}

				if(loopStart < 0) {
					throw new Exception($"No start of loop found for loop end at {context.Position}!");
				}

				if(context.Memory[context.Pointer] != 0) {
					context.Position = loopStart;
				}
			}
		}

		public void CheckSyntax(string code) {
			var bracketsOpened = 0;
			var bracketsClosed = 0;

			for(int i = 0; i < code.Length; i++) {
				if(code[i] == '[') {
					bracketsOpened++;
				} else if(code[i] == ']') {
					bracketsClosed++;
				}

				if(bracketsOpened < bracketsClosed) {
					throw new EarlyClosedLoopSyntaxException(code, i);
				}
			}

			if(bracketsOpened != bracketsClosed) {
				throw new MissingClosedLoopSyntaxException(code);
			}
		}
	}
}
