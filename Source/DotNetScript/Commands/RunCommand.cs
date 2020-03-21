using DotNetScript.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetScript.Commands
{
    internal class RunCommand : Command
    {
        public override List<string> Identifiers => new List<string> { "execute", "run", "start" };
        private CompileCommand CompileCommand => new CompileCommand();

        internal override void ValidateArguments(string[] arguments)
        {
            if (arguments.Length == 0) throw new InvalidOperationException("No path was specified.");
            if (arguments.Length > 1) throw new InvalidOperationException("Invalid arguments specified. This command requires only on argument.");
        }

        protected override void Execute(string[] arguments)
        {
            CompileCommand.ValidateArguments(new string[] { arguments[0], "false", "true" });

            InvokeFunction(CompileCommand.CompileFile(arguments[0], false, true).CompiledAssembly);
        }

        private void InvokeFunction(Assembly assembly)
        {
            Type @class = assembly.GetTypes().First(x => x.IsPublic && !x.IsAbstract && !x.IsInterface && !x.IsGenericType);

            @class.GetMethods().First(x => x.IsPublic && !x.IsStatic && !x.IsGenericMethod && !x.IsAbstract && !x.ContainsGenericParameters && x.GetParameters().Length == 0).Invoke(Activator.CreateInstance(@class), null);
        }

    }
}
