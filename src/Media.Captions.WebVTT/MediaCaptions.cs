namespace Media.Captions.WebVTT
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Media captions metadata.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#file-structure for more details.</remarks>
    public class MediaCaptions
    {
        /// <summary>
        /// Gets or sets region definitions.
        /// </summary>
        public RegionDefinition[] Regions { get; set; }

        /// <summary>
        /// Gets or sets style definitions.
        /// </summary>
        public Style[] Styles { get; set; }

        /// <summary>
        /// Gets or sets subtitles, captions, and metadata.
        /// </summary>
        public Cue[] Cues { get; set; }
    }
}
