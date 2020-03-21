using DotNetScript.Base;
using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetScript.Commands
{
    internal class CompileCommand : Command
    {
        public override List<string> Identifiers => new List<string> { "compile", "build" };

        internal override void ValidateArguments(string[] arguments)
        {
            if (arguments.Length != 3) throw new InvalidOperationException("Not enough arguments specified");

            if (!File.Exists(arguments[0])) throw new FileNotFoundException($"The specified file '{arguments[0]}' does not exist.");
            if (!arguments[0].EndsWith(".vb") && !arguments[0].EndsWith(".cs")) throw new InvalidOperationException($"The file '{arguments[0]}' does not end with .cs or .vb.");

            if (!bool.TryParse(arguments[1], out _)) throw new InvalidOperationException($"Argument {arguments[1]} is not a boolean.");
            if (!bool.TryParse(arguments[2], out _)) throw new InvalidOperationException($"Argument {arguments[2]} is not a boolean.");
        }

        protected override void Execute(string[] arguments)
        {
            CompileFile(arguments[0], bool.Parse(arguments[1]), bool.Parse(arguments[2]));
        }

        internal CompilerResults CompileFile(string sourceFile, bool asExecutable, bool inMemory)
        {
            CodeDomProvider provider;

            if (sourceFile.EndsWith(".cs")) provider = new CSharpCodeProvider();
                else provider = new VBCodeProvider();

            CompilerResults results = provider.CompileAssemblyFromFile(new CompilerParameters()
            {
                GenerateExecutable = asExecutable, // compile as library (dll)
                GenerateInMemory = inMemory // as a physical file or not
            }, sourceFile);

            if (results.Errors.Count != 0) ThrowErros(sourceFile, results);
            if (inMemory) return results;

            File.Move(results.PathToAssembly, Path.Combine(Path.GetDirectoryName(sourceFile), Path.GetFileNameWithoutExtension(sourceFile) + (asExecutable ? ".exe" : ".dll")));

            return results;
        }

        private void ThrowErros(string file, CompilerResults results)
        {
            StringBuilder errors = new StringBuilder();
            errors.Append($"Errors building {file} into {results.PathToAssembly}");
            foreach (CompilerError error in results.Errors)
            {
                errors.Append($"  {error.ToString()}");
            }
            throw new InvalidProgramException(errors.ToString());
        }

    }
}
