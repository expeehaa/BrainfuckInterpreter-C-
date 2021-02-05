namespace BrainfuckInterpreter.Commands {
	[Command('+', '-')]
	public class AdditionCommand : ICommand {
		public void Handle(InterpretationContext context) {
			if(context.CurrentCommand == '+' && context.Memory[context.Pointer] > short.MaxValue) {
				context.Memory[context.Pointer] = 0;
			} else if(context.CurrentCommand == '-' && context.Memory[context.Pointer] < 0) {
				context.Memory[context.Pointer] = short.MaxValue;
			} else {
				context.Memory[context.Pointer] += context.CurrentCommand == '+' ? 1 : -1;
			}
		}
	}
}
