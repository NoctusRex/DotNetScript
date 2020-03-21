using DotNetScript.Base;
using DotNetScriptbase.Helper;
using System.Collections.Generic;

namespace DotNetScript.Commands
{
    internal class VersionCommand : Command
    {
        public override List<string> Identifiers => new List<string> { "version", "credits", "about", "info" };

        internal override void ValidateArguments(string[] arguments) { }

        protected override void Execute(string[] arguments)
        {
            AssemblyInfo ai = new AssemblyInfo();
            ConsoleHelper.WriteLine($"Title:            {ai.Title}");
            ConsoleHelper.WriteLine($"Product:          {ai.Product}");
            ConsoleHelper.WriteLine($"Description:      {ai.Description}");
            ConsoleHelper.WriteLine($"Assembly Version: {ai.AssemblyVersion}");
            ConsoleHelper.WriteLine($"File Version:     {ai.FileVersion}");
            ConsoleHelper.WriteLine($"Company:          {ai.Company}");
            ConsoleHelper.WriteLine($"Trademark:        {ai.Trademark}");
            ConsoleHelper.WriteLine($"Copyright:        {ai.Copyright}");
            ConsoleHelper.WriteLine($"Neutral Language: {ai.NeutralLanguage}");
        }

    }
}
