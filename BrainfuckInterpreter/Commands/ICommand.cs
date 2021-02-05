namespace BrainfuckInterpreter.Commands {
	public interface ICommand {
		public void Handle(InterpretationContext context);
	}
}
