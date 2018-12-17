namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Configures the indent position of the cue box in the direction orthogonal to the line cue setting.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-position-cue-setting for details.</remarks>
    public struct PositionSettings
    {
        /// <summary>
        /// The cue position as a percentage of the video viewport. 
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-position-cue-setting for details.</remarks>
        public double? PositionPercent;

        /// <summary>
        /// An alignment for the cue box in the dimension of the writing direction, describing what the position is anchored to.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-position-cue-setting for details.</remarks>
        public PositionAlignment? Alignment;
    }
}
