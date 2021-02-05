using BrainfuckInterpreter.Commands;
using BrainfuckInterpreter.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainfuckInterpreter {
	public class Interpreter {
		public static Dictionary<char, ICommand> Commands { get; }
		
		static Interpreter(){
			var icommandType         = typeof(ICommand);
			var commandAttributeType = typeof(CommandAttribute);

			Commands = icommandType.Assembly.GetTypes()
				.Where(t => t.GetInterfaces().Contains(icommandType))
				.Select(t => (Type: t, CommandAttribute: t.GetCustomAttributes(commandAttributeType, false).FirstOrDefault(a => a is CommandAttribute) as CommandAttribute))
				.Where(s => s.CommandAttribute is not null)
				.Select(s => (Command: (ICommand)Activator.CreateInstance(s.Type), s.CommandAttribute.Names))
				.SelectMany(s => s.Names.Select(n => (s.Command, Name: n)))
				.ToDictionary(s => s.Name, s => s.Command);
		}
		
		public InterpretationContext Context { get; }

		public Interpreter(string code, string input) {
			Context = new InterpretationContext(code, input);
		}

		public void CheckSyntax(){
			foreach(var command in Commands.Values) {
				command.CheckSyntax(Context.Code);
			}
		}

		public bool HasCorrectSyntax(){
			try {
				CheckSyntax();
				return true;
			} catch(SyntaxException) {
				return false;
			}
		}

		public string Interpret(){
			Context.Reset();
			
			if(!string.IsNullOrWhiteSpace(Context.Code)) {
				CheckSyntax();

				while(Context.Position < Context.Code.Length) {
					Commands[Context.CurrentCommand].Handle(Context);
					
					Context.Position++;
				}

				return Context.Output;
			} else {
				return "";
			}
		}
	}
}
