
using System.Collections.Generic;
 
namespace func.brainfuck
{
    public class My_BrainfuckLoopCommands
    {
        public static Dictionary<int, int> OpenBr = new Dictionary<int, int>();
        public static Dictionary<int, int> ClosingBr = new Dictionary<int, int>();
        public static Stack<int> Stack = new Stack<int>();

        public static void BodyLoop(IVirtualMachine vm)
        {
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                var bracket = vm.Instructions[i];
                switch (bracket)
                {
                    case '[':
                        Stack.Push(i);
                        break;
                    case ']':
                        ClosingBr[i] = Stack.Peek();
                        OpenBr[Stack.Pop()] = i;
                        break;
                }
            }
        }

        public static void RegisterTo(IVirtualMachine vm)
        {
            BodyLoop(vm);

            vm.RegisterCommand('[', machine =>
            {
                if (machine.Memory[machine.MemoryPointer] == 0)
                {
                    machine.InstructionPointer = OpenBr[machine.InstructionPointer];
                }
            });
            vm.RegisterCommand(']', machine =>
            {
                if (machine.Memory[machine.MemoryPointer] != 0)
                {
                    machine.InstructionPointer = ClosingBr[machine.InstructionPointer];
                }
            });
        }
    }
}