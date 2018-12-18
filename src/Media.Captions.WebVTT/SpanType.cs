namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Determines the type of a caption or subtitle cue components.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-caption-or-subtitle-cue-components for details.</remarks>
    public enum SpanType
    {
        /// <summary>
        /// A WebVTT cue class span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-class-span for details.</remarks>
        Class,

        /// <summary>
        /// A WebVTT cue italics span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-italics-span for details.</remarks>
        Italics,

        /// <summary>
        /// A WebVTT cue bold span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-bold-span for details.</remarks>
        Bold,

        /// <summary>
        /// A WebVTT cue underline span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-underline-span for details.</remarks>
        Underline,

        /// <summary>
        /// A WebVTT cue ruby span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-ruby-span for details.</remarks>
        Ruby,

        /// <summary>
        /// A WebVTT cue ruby span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-ruby-span for details.</remarks>
        RubyText,

        /// <summary>
        /// A WebVTT cue voice span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-voice-span for details.</remarks>
        Voice,

        /// <summary>
        /// A WebVTT cue language span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-language-span for details.</remarks>
        Language,

        /// <summary>
        /// A WebVTT cue timestamp.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-timestamp for details.</remarks>
        TimeStamp,

        /// <summary>
        /// A WebVTT cue text span.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-text-span for details.</remarks>
        Text
    }
}
