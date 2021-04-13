using System;

namespace BlazorComponent.Doc.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            var testArgs = @"demo2json D:\Project\OpenSource\BlazorComponent\src\/Doc/BlazorComponent.Doc/Demos D:\Project\OpenSource\BlazorComponent\src\/Doc/BlazorComponent.Doc/wwwroot/meta";
            args = testArgs.Split(' ');

            try
            {
                return new CliWorker().Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred. {e.ToString()}");
                return 1;
            }
        }
    }
}
