using System;
using System.IO;

namespace BrainfuckInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program(args);
        }

        public Program(string[] args)
        {
            string filename = null;
            string input = "";
            if(args.Length >= 1)
            {
                filename = args[0];
                if (args.Length >= 2) input = args[1];
            }
            var content = "";

            if(filename == null)
            {
                Console.WriteLine("As there is no file given, you can write your code here.\nPress 'Enter' to execute your code.");
                content = Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Reading code from {filename}...");
                content = readFile(filename);
                Console.WriteLine(content);
            }
            Console.WriteLine(interpreteCode(content, input));
            Console.ReadLine();
        }

        private string readFile(string filename)
        {
            var fileinfo = new FileInfo(filename);
            try
            {
                return fileinfo.OpenText().ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        private string interpreteCode(string code, string input)
        {
            var position = 0;
            var memory = new short[0xFFFF];
            var pointer = 0;
            var inputPointer = 0;
            var output = "";

            var bracketsOpen = 0;
            var bracketsClosed = 0;

            while(position < code.Length)
            {
                switch (code[position])
                {
                    case '+':
                        if (memory[pointer] >= short.MaxValue) memory[pointer] = 0;
                        else memory[pointer]++;
                        break;
                    case '-':
                        if (memory[pointer] <= 0) memory[pointer] = short.MaxValue;
                        else memory[pointer]--;
                        break;
                    case '>':
                        if (pointer >= 0xFFFF) pointer = 0;
                        else pointer++;
                        break;
                    case '<':
                        if (pointer <= 0) pointer = 0xFFFF - 1;
                        else pointer--;
                        break;
                    case '.':
                        output += (char)memory[pointer];
                        break;
                    case ',':
                        memory[pointer] = (short)input[inputPointer];
                        inputPointer++;
                        break;
                    case '[':
                        var loopEnd = position + 1;
                        bracketsOpen = 1;
                        bracketsClosed = 0;

                        while(loopEnd < code.Length && bracketsOpen != bracketsClosed)
                        {
                            if (code[loopEnd] == '[') bracketsOpen++;
                            else if (code[loopEnd] == ']') bracketsClosed++;
                            loopEnd++;
                        }
                        if (loopEnd == code.Length)
                        {
                            Console.WriteLine($"No end of loop found for loop start at {position}!");
                            return "";
                        }
                        if (memory[pointer] == 0) position = loopEnd;
                        break;
                    case ']':
                        var loopStart = position - 1;
                        bracketsOpen = 0;
                        bracketsClosed = 1;

                        while(loopStart >= 0 && bracketsOpen != bracketsClosed)
                        {
                            if (code[loopStart] == '[') bracketsOpen++;
                            else if (code[loopStart] == ']') bracketsClosed++;
                            loopStart--;
                        }
                        if(loopStart < 0)
                        {
                            Console.WriteLine($"No start of loop found for loop end at {position}!");
                            return "";
                        }
                        if(memory[pointer] != 0) position = loopStart;
                        break;
                    default:
                        break;
                }
                position++;
            }

            return output;
        }
    }
}
