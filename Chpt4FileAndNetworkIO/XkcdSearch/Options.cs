using CommandLine;

namespace XkcdSearch;

public record Options
{
    [Value(0, Required = true, HelpText = "Title")]
    public string? Title { get; init; }

    [Option('c', "category", HelpText = "Joke category")]
    public string Category { get; init; } = "programming";
}
