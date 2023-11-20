using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WeatherAnalyzer.Util;

internal static class Util
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNull]
    public static T ApiResultNullCheck<T>(T value, [CallerArgumentExpression(nameof(value))] string expr = null!)
    {
        if (value is null)
        {
            throw new InvalidOperationException($"API error: '{expr}' was null");
        }

        return value;
    }
}
