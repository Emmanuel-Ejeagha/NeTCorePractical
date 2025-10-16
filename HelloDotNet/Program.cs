// This project is using global using in .csproj file

using HelloDotNet;
using CommandLine;

if (args.Length == 0)
{
    WriteLine("Usage: HelloDotnet <text>");
    Environment.Exit(1);
}

Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(AsciiArt.Write)
    .WithNotParsed(_ =>
        WriteLine("Usage: HelloDotnet <text> --font Big"));
// Console.WriteLine(FiggleFonts.Standard.Render(args[0]));
// AsciiArt.Write(args[0]);
