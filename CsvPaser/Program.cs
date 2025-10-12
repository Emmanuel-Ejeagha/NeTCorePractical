using System.Text;
using CsvParser;

var reader = new StreamReader(new FileStream("Marvel.csv", FileMode.Open));

var csvReader = new CsvReader(reader);

foreach (var line in csvReader.Lines)

Console.WriteLine(line.First(p => p.Key == "Title").Value);