namespace BrainfuckInterpreter.Commands {
	[Command(',')]
	public class ReadInputCommand : ICommand {
		public void Handle(InterpretationContext context) {
			context.Memory[context.Pointer] = (short)context.Input[context.InputPointer];
			context.InputPointer++;
		}
	}
}
