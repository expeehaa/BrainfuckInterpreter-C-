namespace BrainfuckInterpreter.Commands {
	[Command('.')]
	public class OutputCommand : ICommand {
		public void Handle(InterpretationContext context) {
			context.Output += (char)context.Memory[context.Pointer];
		}

		public void CheckSyntax(string code) {
			// No syntax problems possible.
		}
	}
}
