namespace TripDatePlanner.Utilities.Extensions;

public static class LinqExtensions
{
    public static (TResult Min, TResult Max) MinMax<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult> selector)
        where TResult : struct, IComparable<TResult>
    {
        return MinMax(source, selector, selector);
    }
    
    public static (TResult Min, TResult Max) MinMax<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult> minSelector,
        Func<TSource, TResult> maxSelector)
        where TResult : struct, IComparable<TResult>
    {
        using IEnumerator<TSource> enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            return (default, default);

        TSource current = enumerator.Current;
        TResult minValue = minSelector(current);
        TResult maxvalue = maxSelector(current);
        
        TResult min = minValue;
        TResult max = maxvalue;

        while (enumerator.MoveNext())
        {
            current = enumerator.Current;
            minValue = minSelector(current);
            maxvalue = maxSelector(current);

            if (minValue.CompareTo(min) < 0)
                min = minValue;
            if (maxvalue.CompareTo(max) > 0)
                max = maxvalue;
        }

        return (min, max);
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T element in source)
        {
            action(element);
        }
    }

    public static IEnumerable<T> Pipe<T>(this IEnumerable<T> source, Action<T> action)
    {
        return source.Select(element =>
        {
            action(element); 
            return element;
        });
    }
}