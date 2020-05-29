using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
	public class Brainfuck
	{
		public static string Run(string program, string input = "")
		{
			var inputIndex = 0;
			var output = new StringBuilder();
			Run(program,
				() => inputIndex >= input.Length ? -1 : input[inputIndex++],
				c => output.Append(c));
			return output.ToString();
		}

		public static void Run(string program, Func<int> read, Action<char> write, int memorySize = 30000)
		{
			var vm = new My_VirtualMachine(program, memorySize);
			My_BrainfuckBasicCommands.RegisterTo(vm, read, write);
			My_BrainfuckLoopCommands.RegisterTo(vm);
			vm.Run();
		}
	}
}