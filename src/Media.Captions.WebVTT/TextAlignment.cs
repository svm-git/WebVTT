namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Alignment of the text within the cue.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-alignment-cue-setting for details.</remarks>
    public enum TextAlignment
    {
        /// <summary>
        /// Align to the start of the line, relative to base text direction.
        /// </summary>
        Start,

        /// <summary>
        /// Align to the center of the line.
        /// </summary>
        Center,

        /// <summary>
        /// Align to the end of the line, relative to base text direction.
        /// </summary>
        End,

        /// <summary>
        /// Align to the left of the line.
        /// </summary>
        Left,

        /// <summary>
        /// Align to the right of the line.
        /// </summary>
        Right
    }
}
