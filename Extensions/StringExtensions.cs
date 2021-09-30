using System;
using System.Text;

namespace Watson.SmartHomeHub.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Perform an append with the addition value only if the specified predicate passes.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="value">The string to append.</param>
        /// <param name="condition">The predicate that determines whether the addition will be appended.</param>
        /// <returns>Returns the string builder.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, string value, Predicate<string> condition)
        {
            return condition.Invoke(value) ? builder.Append(value) : builder;
        }

        /// <summary>
        /// Perform an append with the addition value only if the specified value is not null or empty.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>Returns the string builder.</returns>
        public static StringBuilder AppendIfNotNullOrEmpty(this StringBuilder builder, string value)
        {
            return !string.IsNullOrEmpty(value) ? builder.Append(value) : builder;
        }
    }
}