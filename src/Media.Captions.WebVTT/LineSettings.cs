namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Cue line settings.
    /// </summary>
    /// <remarks></remarks>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-line-cue-setting for details.</remarks>
    public struct LineSettings
    {
        /// <summary>
        /// Offset relative to the video viewport.
        /// </summary>
        public double? Percent;

        /// <summary>
        /// Line number.
        /// </summary>
        public int? LineNumber;

        /// <summary>
        /// Text alignment for the cue.
        /// </summary>
        public LineAlignment? Alignment;
    }
}
