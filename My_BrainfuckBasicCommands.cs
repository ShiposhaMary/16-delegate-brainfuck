using System;
using System.Text.RegularExpressions;

namespace func.brainfuck
{
    public class My_BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write((char) b.Memory[b.MemoryPointer]); });

            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte) read(); });

            vm.RegisterCommand('+', b =>
            {
                unchecked
                {
                    b.Memory[b.MemoryPointer]++;
                }
            });

            vm.RegisterCommand('-', b =>
            {
                unchecked
                {
                    b.Memory[b.MemoryPointer]--;
                }
            });

            vm.RegisterCommand('>', b =>
            {
                if (++b.MemoryPointer >= b.Memory.Length) b.MemoryPointer %= b.Memory.Length;
            });

            vm.RegisterCommand('<', b =>
            {
                if (--b.MemoryPointer < 0) b.MemoryPointer = b.Memory.Length - 1;
            });

            RegisterNotSymbols(vm);
        }

        private static void RegisterNotSymbols(IVirtualMachine vm)
        {
            for (var i = 0; i < byte.MaxValue; i++)
            {
                if (!(((char)i) >= '0' && ((char)i) <= '9' || ((char)i) >= 'a' && ((char)i) <= 'z' || ((char)i) >= 'A' && ((char)i) <= 'Z')) continue;
                var j = i;
                vm.RegisterCommand((char) i, b => { b.Memory[b.MemoryPointer] = (byte) j; });
            }
        }
    }
}