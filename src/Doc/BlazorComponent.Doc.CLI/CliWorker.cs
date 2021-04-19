using BlazorComponent.Doc.CLI.Commands;
using BlazorComponent.Doc.CLI.Interfaces;
using BlazorComponent.Doc.CLI.Wrappers;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Doc.CLI
{
    public class CliWorker
    {
        public int Execute(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = ConfigWrapper.Config.Name,
                FullName = ConfigWrapper.Config.FullName,
                Description = ConfigWrapper.Config.Descrption,
            };

            app.HelpOption();
            app.VersionOptionFromAssemblyAttributes(typeof(Program).Assembly);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 2;
            });

            new List<IAppCommand>()
            {
                new GenerateDemoJsonCommand(),
                new GenerateDocsToHtmlCommand(),
                new GenerateIconsToJsonCommand(),
                new GenerateMenuJsonCommand(),
            }
            .ToList()
            .ForEach(cmd =>
            {
                app.Command(cmd.Name, cmd.Execute);
            });

            return app.Execute(args);
        }
    }
}
