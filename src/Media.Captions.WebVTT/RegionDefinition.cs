namespace Media.Captions.WebVTT
{
    /// <summary>
    /// A WebVTT Region Definition block.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-definition-block" for details.</remarks>
    public class RegionDefinition : BaseBlock
    {
        /// <summary>
        /// Unique ID of the region. The ID gives a name to the region so it can be referenced by the cues
        /// that belong to the region.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-identifier-setting for details.</remarks>
        public string Id { get; set; }

        /// <summary>
        /// The width setting provides a fixed width as a percentage of the video width for the region
        /// into which cues are rendered and based on which alignment is calculated.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-width-setting for details.</remarks>
        public double? WidthPercent { get; set; }

        /// <summary>
        /// The lines setting provides a fixed height as a number of lines for the region into which cues are rendered.
        /// As such, it defines the height of the roll-up region if it is a scroll region.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-lines-setting for details.</remarks>
        public int? Lines { get; set; }

        /// <summary>
        /// The region anchor setting provides a tuple of two percentages that specify the point within the region
        /// box that is fixed in location.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-anchor-setting for details.</remarks>
        public Anchor? RegionAnchor { get; set; }

        /// <summary>
        /// The viewport anchor setting provides a tuple of two percentages that specify the point within the video viewport
        /// that the region anchor point is anchored to.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-viewport-anchor-setting for details.</remarks>
        public Anchor? ViewPortAnchor { get; set; }

        /// <summary>
        /// The region scroll setting specifies whether cues rendered into the region are allowed to move out of their
        /// initial rendering place and roll up, i.e. move towards the top of the video viewport.
        /// </summary>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-region-lines-setting for details.</remarks>
        public bool? Scroll { get; set; }
    }
}
