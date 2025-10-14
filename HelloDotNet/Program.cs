// This project is using global using in .csproj file

using HelloDotNet;

if (args.Length == 0)
{
    Console.WriteLine("Usage: HelloDotnet <text>");
    Environment.Exit(1);
}

// Console.WriteLine(FiggleFonts.Standard.Render(args[0]));
AsciiArt.Write(args[0]);

WriteLine("Hello World");
