using System;
using System.Reflection;
using Figgle;

namespace AsciiArtSvc;

public static class AsciiArt
{
    public static string Write(string text, string? fontName = null)
    {
        FiggleFont? font = null;

        if (!string.IsNullOrWhiteSpace(fontName))
        {
            font = typeof(FiggleFonts).GetProperty(fontName,
            BindingFlags.Static | BindingFlags.Public)
            ?.GetValue(null)
            as FiggleFont;
        }
        font ??= FiggleFonts.Standard;
        return font.Render(text);
    }
    public static Lazy<IEnumerable<(string Name, FiggleFont Font)>> AllFonts =
        new(() =>
            from p in typeof(FiggleFonts)
            .GetProperties(
                BindingFlags.Public | BindingFlags.Static)
            select
                (Name: p.Name,
                Font: (p.GetValue(null) as FiggleFont)
            ));
}
