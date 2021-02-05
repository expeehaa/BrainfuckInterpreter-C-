using System;
using System.IO;

namespace BrainfuckInterpreter {
	public class Program {
		public static void Main(string[] args) {
			string filename = null;
			var input = "";
			string content;

			if(args.Length >= 1) {
				filename = args[0];

				if(args.Length >= 2) {
					input = args[1];
				}
			}

			if(filename == null) {
				Console.WriteLine("As there is no file given, you can write your code here.\nPress 'Enter' to execute your code.");
				content = Console.ReadLine();
			} else {
				Console.WriteLine($"Reading code from {filename}...");
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
