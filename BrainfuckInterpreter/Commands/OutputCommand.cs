namespace BrainfuckInterpreter.Commands {
	[Command('.')]
	public class OutputCommand : ICommand {
		public void Handle(InterpretationContext context) {
			context.Output += (char)context.Memory[context.Pointer];
		}
	}
}
