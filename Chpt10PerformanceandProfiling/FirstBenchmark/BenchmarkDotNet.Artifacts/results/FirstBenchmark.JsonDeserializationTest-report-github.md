```

BenchmarkDotNet v0.15.6, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
Intel Core i5-4300U CPU 1.90GHz (Max: 2.59GHz) (Haswell), 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.112
  [Host]     : .NET 9.0.11 (9.0.11, 9.0.1125.51716), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 9.0.11 (9.0.11, 9.0.1125.51716), X64 RyuJIT x86-64-v3


```
| Method     | Mean     | Error    | StdDev   |
|----------- |---------:|---------:|---------:|
| Newtonsoft | 606.9 μs | 11.99 μs | 23.10 μs |
| SystemText | 224.6 μs |  4.46 μs |  3.72 μs |
