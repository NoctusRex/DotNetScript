using DotNetScript.Base;
using DotNetScript.Commands;
using DotNetScriptbase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetScriptbase
{
    class Program
    {
        private static List<Command> Commands { get; set; }

        static void Main(string[] arguments)
        {
            ConsoleHelper.Init();

            LoadCommands();

            try
            {
                TryExecuteCommand(arguments);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError(ex);
                ConsoleHelper.WriteWarning("Use 'help' for a list of commands.");
            }
        }

        private static void LoadCommands()
        {
            Commands = new List<Command>();
            AddCommand(new RunCommand());
            AddCommand(new HelpCommand());
            AddCommand(new VersionCommand());
            AddCommand(new CompileCommand());
        }

        private static void AddCommand(Command command)
        {
            if (Commands.Any(x => x.GetType() == command.GetType())) { ConsoleHelper.WriteWarning($"'{command.GetType().Name}' was already added."); return; }

            command.Identifiers.ForEach(i =>
            {
                if (Commands.Any(c => c.Identifiers.Contains(i))) throw new InvalidOperationException($"The identifier '{i}' of '{command.GetType().Name}' is already registered.");
            });

            Commands.Add(command);
        }

        private static void TryExecuteCommand(string[] arguments)
        {
            if (arguments.Length == 0) throw new ArgumentNullException("No argument was specified.");

            string mainCommand = arguments[0].ToLower();
            List<string> foundIdentifiers = new List<string>();

            // find matching commands
            foreach (Command command in Commands)
            {
                foreach (string identifier in command.Identifiers)
                {
                    if (!identifier.StartsWith(mainCommand)) continue;

                    foundIdentifiers.Add(identifier);
                }
            }

            // take action depending on how many commands where found
            switch (foundIdentifiers.Count)
            {
                case 0:
                    if(arguments.Length != 1) throw new InvalidOperationException($"Invalid argument {mainCommand}.");
                    Commands.First(x => x is RunCommand).TryExecute(arguments);
                    break;

                case 1:
                    Commands.First(x => x.Identifiers.Contains(foundIdentifiers.First())).TryExecute(arguments.Skip(1).ToArray());
                    break;

                default:
                    ConsoleHelper.WriteWarning($"ARgument {mainCommand} is not unique. Did you mean:");
                    foundIdentifiers.ForEach(x => { ConsoleHelper.WriteLine($" - {x}"); });
                    break;
            }

        }
    }
}
