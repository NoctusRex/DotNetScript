using DotNetScript.Base;
using DotNetScriptbase.Helper;
using System.Collections.Generic;

namespace DotNetScript.Commands
{
    internal class HelpCommand : Command
    {
        public override List<string> Identifiers => new List<string> { "help", "?"};

        internal override void ValidateArguments(string[] arguments) { }

        protected override void Execute(string[] arguments)
        {
            ConsoleHelper.NewLine();
            ConsoleHelper.WriteLine("help (?, information)    - Prints out all available commands.");
            ConsoleHelper.NewLine();
            ConsoleHelper.WriteLine("version (credits, about) - Prints out assembly information.");
            ConsoleHelper.NewLine();
            ConsoleHelper.WriteLine("run (execute, start)     - Executes a c#/vb class file. Starting from the first parameterless, not generic, not abstract, not static method of the first public, not abstract, not interface, not generic class found.");
            ConsoleHelper.WriteLine("                         -> 2nd argument: The path to the .cs/.vb file.");
            ConsoleHelper.NewLine();
            ConsoleHelper.WriteLine("compile (build)          - Compiles a source file to a .dll or .exe.");
            ConsoleHelper.WriteLine("                         -> 2nd argument: The path to the .cs/.vb file.");
            ConsoleHelper.WriteLine("                         -> 3rd argument: Build file as executable (boolean).");
            ConsoleHelper.WriteLine("                         -> 4th argument: Build file in memory (boolean). If false the file is created in the same directory as the source file.");
            ConsoleHelper.NewLine();
        }

    }
}
