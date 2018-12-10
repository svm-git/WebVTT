namespace Media.Captions.WebVTT
{
    using System;

    /// <summary>
    /// Media cue block.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-cue-block" for details.</remarks>
    public class Cue : BaseBlock
    {
        /// <summary>
        /// Gets or sets start of the cue block relative to the beginning of a media stream.
        /// </summary>
        public TimeSpan Start { get; set; }

        /// <summary>
        /// Gets or sets end of the cue block relative to the beginning of a media stream.
        /// </summary>
        public TimeSpan End { get; set; }

        /// <summary>
        /// Gets or sets id of the cue block.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets raw content of the cue settings.
        /// </summary>
        public string RawSettings { get; internal set; }
    }
}
