namespace BrainfuckInterpreter.Commands {
	[Command('<', '>')]
	public class MovePointerCommand : ICommand {
		public void Handle(InterpretationContext context) {
			if(context.CurrentCommand == '>' && context.Pointer >= 0xFFFF) {
				context.Pointer = 0;
			} else if(context.CurrentCommand == '<' && context.Pointer <= 0) {
				context.Pointer = 0xFFFF - 1;
			} else {
				context.Pointer += context.CurrentCommand == '>' ? 1 : -1;
			}
		}

		public void CheckSyntax(string code) {
			// No syntax problems possible.
		}
	}
}
