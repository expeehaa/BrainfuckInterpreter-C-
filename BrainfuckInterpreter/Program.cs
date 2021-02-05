using BrainfuckInterpreter.Exceptions;
using System;
using System.IO;
using System.Linq;

namespace BrainfuckInterpreter {
	public class Program {
		public static void Main(string[] args) {
			var filename = args.FirstOrDefault();
			var input = string.Join(" ", args.Skip(1).ToArray());
			
			var interpreter = new Interpreter(GetCode(filename), input);
			
			try {
				Console.WriteLine(interpreter.Interpret());
			} catch(SyntaxException e) {
				Console.WriteLine($"The syntax of the given brainfuck code is incorrect.\n{e}");
			}
			Console.ReadLine();
		}

		private static string GetCode(string filename) {
			if(filename == null) {
				Console.WriteLine("No file given, enter code and press Enter to execute.");
				return Console.ReadLine();
			} else {
				return ReadFile(filename);
			}
		}

		private static string ReadFile(string filename) {
			var fileinfo = new FileInfo(filename);

			try {
				return fileinfo.OpenText().ReadToEnd();
			} catch(Exception) {
				Console.WriteLine($"Error opening file '{filename}'");
				throw;
			}
		}
	}
}
