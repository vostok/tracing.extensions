using System;

namespace Vostok.Tracing.Extensions
{
    /// <summary>
    /// Convert primitives types to pretty string for storage at annotations
    /// </summary>
    internal static class Primitive_ToPrettyString_Extensions
    {
        public static string ToPrettyString(this Guid? guid) => guid?.ToString();
        public static string ToPrettyString(this Guid guid) => guid.ToString();
        public static string ToPrettyString(this int num) => num.ToString();
        public static string ToPrettyString(this int? num) => num?.ToString();
    }
}