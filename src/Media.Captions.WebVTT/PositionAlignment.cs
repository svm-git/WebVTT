namespace Media.Captions.WebVTT
{
    /// <summary>
    /// An alignment for the cue box in the dimension of the writing direction, describing what the position is anchored to.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-position-line-left-alignment for details.</remarks>
    public enum PositionAlignment
    {
        /// <summary>
        /// The cue box’s left side (for horizontal cues) or top side (otherwise) is aligned at the position.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-position-line-left-alignment for details.</remarks>
        LineLeft,

        /// <summary>
        /// The cue box is centered at the position. 
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-line-center-alignment for details.</remarks>
        Center,

        /// <summary>
        /// The cue box’s bottom side (for horizontal cues), right side (for vertical growing right), or left side (for vertical growing left) is aligned at the line.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-line-end-alignment for details.</remarks>
        LineRight
    }
}
