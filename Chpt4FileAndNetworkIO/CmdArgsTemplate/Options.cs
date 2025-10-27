using CommandLine;

namespace CmdArgsTemplate;

public record class Options
{
    [Value(0, Required = true, HelpText = "Some text")]
    public string? Text { get;  init;}
}
