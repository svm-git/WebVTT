namespace Media.Captions.WebVTT
{
    using System;
    using System.Collections.Generic;

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
        /// ID of a region this cue is a part of.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-cue-setting for details.</remarks>
        public string Region { get; set; }

        /// <summary>
        /// A vertical text cue setting configures the cue to use vertical text layout rather than horizontal text layout.
        /// Vertical text layout is sometimes used in Japanese, for example.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-vertical-text-cue-setting for details.</remarks>
        public VerticalTextLayout? Vertical { get; set; }

        /// <summary>
        /// Settings configuring the offset of the cue box from the video viewport’s edge.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-line-cue-setting for details.</remarks>
        public LineSettings? Line { get; set; }

        /// <summary>
        /// Configures the indent position of the cue box in the direction orthogonal to the line cue setting.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-position-cue-setting for details.</remarks>
        public PositionSettings? Position { get; set; }

        /// <summary>
        /// Configures the size of the cue box in the same direction as the WebVTT position cue setting.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-size-cue-setting for details.</remarks>
        public double? SizePercent { get; set; }

        /// <summary>
        /// Configures slignment of the text within the cue.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-alignment-cue-setting for details.</remarks>
        public TextAlignment? Alignment { get; set; }

        /// <summary>
        /// Gets raw content of the cue settings.
        /// </summary>
        public string RawSettings { get; internal set; }

        /// <summary>
        /// Gets or sets cue content.
        /// </summary>
        public Span[] Content { get; set; }
    }
}
