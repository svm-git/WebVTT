namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Region or viewpoer anchor structure.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-anchor-setting for more details.</remarks>
    public struct Anchor
    {
        /// <summary>
        /// Measures x-dimension percentage.
        /// </summary>
        public double XPercent;

        /// <summary>
        /// Measures y-dimension percentage.
        /// </summary>
        public double YPercent;
    }
}
