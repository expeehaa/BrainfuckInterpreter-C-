using System;
using System.IO;
using System.Linq;

namespace BrainfuckInterpreter {
	public class Program {
		public static void Main(string[] args) {
			var filename = args.FirstOrDefault();
			var input = string.Join(" ", args.Skip(1).ToArray());
			string content;

			if(filename == null) {
				Console.WriteLine("No file given, enter code and press Enter to execute.");
				content = Console.ReadLine();
			} else {
				content = ReadFile(filename);
				Console.WriteLine(content);
			}

			var interpreter = new Interpreter(content, input);

			Console.WriteLine(interpreter.Interpret());
			Console.ReadLine();
		}

		private static string ReadFile(string filename) {
			var fileinfo = new FileInfo(filename);

			try {
				return fileinfo.OpenText().ReadToEnd();
			} catch(Exception e) {
				Console.WriteLine(e.Message);
				return "";
			}
		}
	}
}
