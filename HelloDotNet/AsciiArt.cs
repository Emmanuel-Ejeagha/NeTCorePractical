using HelloDotNet;

public static class AsciiArt
{
    public static void Write(Options opts)
    {
        var font = FiggleFonts.Standard;

        if (!string.IsNullOrWhiteSpace(opts.Font) &&
            opts.Font.Equals("Big", StringComparison.OrdinalIgnoreCase))
        {
            font = FiggleFonts.Big;
        }

        Console.WriteLine(font.Render(opts.Text ?? "Hello"));
        Console.WriteLine("Brought to you by " + typeof(AsciiArt).FullName);
    }

    public static void Write(string text)
    {
        Console.WriteLine(FiggleFonts.Standard.Render(text));
        Console.WriteLine("Brought to you by " + typeof(AsciiArt).FullName);
    }
}
