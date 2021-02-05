using System;

namespace BrainfuckInterpreter.Commands {
	[Command('[', ']')]
	public class LoopCommand : ICommand {
		public void Handle(InterpretationContext context) {
			if(context.CurrentCommand == '['){
				var loopEnd = context.Position + 1;
				context.BracketsOpen = 1;
				context.BracketsClosed = 0;

				while(loopEnd <= context.Code.Length && context.BracketsOpen != context.BracketsClosed) {
					if(context.Code[loopEnd] == '[') {
						context.BracketsOpen++;
					} else if(context.Code[loopEnd] == ']') {
						context.BracketsClosed++;
					}

					loopEnd++;
				}

				if(loopEnd > context.Code.Length) {
					throw new Exception($"No end of loop found for loop start at {context.Position}!");
				}
				if(context.Memory[context.Pointer] == 0) {
					context.Position = loopEnd;
				}
			} else{
				var loopStart = context.Position - 1;
				context.BracketsOpen = 0;
				context.BracketsClosed = 1;

				while(loopStart >= 0 && context.BracketsOpen != context.BracketsClosed) {
					if(context.Code[loopStart] == '[') {
						context.BracketsOpen++;
					} else if(context.Code[loopStart] == ']') {
						context.BracketsClosed++;
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
	}
}
