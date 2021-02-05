namespace BrainfuckInterpreter.Commands {
	[Command(',')]
	public class ReadInputCommand : ICommand {
		public void Handle(InterpretationContext context) {
			context.Memory[context.Pointer] = (short)context.Input[context.InputPointer];
			context.InputPointer++;
		}

		public void CheckSyntax(string code) {
			// No syntax problems possible.
		}
	}
}
