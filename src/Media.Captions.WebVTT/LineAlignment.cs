namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Line alignment setting.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-line-cue-setting for details.</remarks>
    public enum LineAlignment
    {
        /// <summary>
        /// Align to the start of the line.
        /// </summary>
        Start,

        /// <summary>
        /// Align to the center of the line.
        /// </summary>
        Center,

        /// <summary>
        /// Align to the end of the line.
        /// </summary>
        End
    }
}
