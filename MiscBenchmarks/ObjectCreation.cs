using BenchmarkDotNet.Attributes;
using System.Linq.Expressions;
using System.Reflection;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ObjectCreation
{
    [Benchmark]
    public object New()
    {
        return new Foobar();
    }

    [Benchmark]
    public object Activator_CreateInstance()
    {
        return Activator.CreateInstance(typeof(Foobar));
    }

    [Benchmark]
    public object GetConstructor_Invoke()
    {
        var ctor = typeof(Foobar).GetConstructor(Array.Empty<Type>());
        return ctor.Invoke(parameters: null);
    }

    private static readonly ConstructorInfo cachedCtor = typeof(Foobar).GetConstructor(Array.Empty<Type>());
    [Benchmark]
    public object GetConstructor_Cached_Invoke()
    {
        return cachedCtor.Invoke(parameters: null);
    }

    [Benchmark]
    public object Expression_Compile()
    {
        var ctor = typeof(Foobar).GetConstructor(Array.Empty<Type>());
        Expression.New(ctor);

        // Make a NewExpression that calls the ctor with the args we just created
        NewExpression newExp = Expression.New(ctor);

        // Create a lambda with the New expression as body and our param object[] as arg
        LambdaExpression lambda = Expression.Lambda(newExp, null);

        // Compile it
        var compiled = lambda.Compile();
        return compiled.DynamicInvoke(null);
    }

    private static readonly Delegate compiledExpressionCached = new Func<Delegate>(() =>
    {
        var ctor = typeof(Foobar).GetConstructor(Array.Empty<Type>());
        Expression.New(ctor);

        // Make a NewExpression that calls the ctor with the args we just created
        NewExpression newExp = Expression.New(ctor);

        // Create a lambda with the New expression as body and our param object[] as arg
        LambdaExpression lambda = Expression.Lambda(newExp, null);

        // Compile it
        return lambda.Compile();
    }).Invoke();

    [Benchmark]
    public object Expression_Compile_Cached()
    {
        return compiledExpressionCached.DynamicInvoke(null);
    }

    private class Foobar
    {
    }
}
