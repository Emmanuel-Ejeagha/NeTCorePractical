using System.Reflection;
using HelloDotNet;

public static class AsciiArt
{
    public static void Write(Options opts)
    {
        FiggleFont? font = null;
        if (!string.IsNullOrWhiteSpace(opts.Font))
        {
            font = typeof(FiggleFonts)
                .GetProperty(opts.Font, BindingFlags.Static | BindingFlags.Public)
                ?.GetValue(null) as FiggleFont;
            if (font == null)
            {
                WriteLine($"Could not find font '{opts.Font}'");
            }
        }

        font ??= FiggleFonts.Standard;

        if (opts?.Text != null)
        {
            WriteLine(font.Render(opts.Text));
            WriteLine($"Brought to you by {typeof(AsciiArt).FullName}");
        }
        //     var font = FiggleFonts.Standard;

        //     if (!string.IsNullOrWhiteSpace(opts.Font) &&
        //         opts.Font.Equals("Big", StringComparison.OrdinalIgnoreCase))
        //     {
        //         font = FiggleFonts.Big;
        //     }

        //     Console.WriteLine(font.Render(opts.Text ?? "Hello"));
        //     Console.WriteLine("Brought to you by " + typeof(AsciiArt).FullName);
        // }

        // public static void Write(string text)
        // {
        //     Console.WriteLine(FiggleFonts.Standard.Render(text));
        //     Console.WriteLine("Brought to you by " + typeof(AsciiArt).FullName);
        // }
    }
}
