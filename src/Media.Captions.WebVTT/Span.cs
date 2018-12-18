namespace Media.Captions.WebVTT
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a caption or subtitle cue component.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-caption-or-subtitle-cue-components for details.</remarks>
    public class Span
    {
        /// <summary>
        /// Gets or sets type of the span.
        /// </summary>
        public SpanType Type { get; set; }

        /// <summary>
        /// Gets or sets classes of the span.
        /// </summary>
        public string[] Classes { get; set; }

        /// <summary>
        /// Gets or sets children spans.
        /// </summary>
        public Span[] Children { get; set; }

        /// <summary>
        /// Gets or sets span annotation.
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// Gets or sets span text.
        /// </summary>
        public string Text { get; set; }
    }
}
