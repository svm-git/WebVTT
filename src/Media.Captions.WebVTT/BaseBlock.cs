namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Base media metadata block.
    /// </summary>
    public abstract class BaseBlock
    {
        /// <summary>
        /// Gets raw content of the cue block.
        /// </summary>
        /// <remarks>Raw content of a block can be either:
        /// <list type="bullet">
        /// <item>region definition and settings;</item>
        /// <item>style definition;</item>
        /// <item>caption or subtitle text;</item>
        /// <item>chapter title text;</item>
        /// <item>metadata.</item>
        /// </list>
        /// </remarks>
        public string RawContent { get; internal set; }
    }
}
