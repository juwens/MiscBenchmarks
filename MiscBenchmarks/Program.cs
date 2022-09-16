using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Immutable;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<Foobar>();
    }
}

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Foobar
{
    /*
     
    |           Method |      Mean |    Error |   StdDev |    Median | Rank |   Gen0 | Allocated |
    |----------------- |----------:|---------:|---------:|----------:|-----:|-------:|----------:|
    |          ToArray |  20.84 ns | 0.279 ns | 0.233 ns |  20.93 ns |    1 | 0.0076 |      64 B |
    |           ToList |  26.73 ns | 0.590 ns | 1.219 ns |  26.23 ns |    2 | 0.0114 |      96 B |
    | ToImmutableArray |  34.82 ns | 0.424 ns | 0.397 ns |  34.86 ns |    3 | 0.0076 |      64 B |
    |  ToImmutableList | 204.00 ns | 1.663 ns | 1.299 ns | 204.13 ns |    4 | 0.0629 |     528 B |
    
     */

    static readonly int[] Enumerable = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; 

    [Benchmark]
    public List<int> ToList()
    {
        return Enumerable.ToList();
    }

    [Benchmark]
    public int[] ToArray()
    {
        return Enumerable.ToArray();
    }

    [Benchmark]
    public ImmutableArray<int> ToImmutableArray()
    {
        return Enumerable.ToImmutableArray();
    }

    [Benchmark]
    public ImmutableList<int> ToImmutableList()
    {
        return Enumerable.ToImmutableList();
    }
}