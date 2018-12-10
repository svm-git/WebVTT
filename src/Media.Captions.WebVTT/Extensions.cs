namespace Media.Captions.WebVTT
{
    using System.Text;

    /// <summary>
    /// Utility methods.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Utility method that ensures that text lines are property added to the string builder.
        /// </summary>
        /// <param name="builder">The string builder to use.</param>
        /// <param name="line">The line to append.</param>
        /// <returns>The string builder.</returns>
        public static StringBuilder SafeAppendLine(this StringBuilder builder, string line)
        {
            if (line != null)
            {
                if (builder.Length > 0)
                {
                    builder.AppendLine();
                }

                builder.Append(line);
            }

            return builder;
        }
    }
}
