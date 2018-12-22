namespace Media.Captions.WebVTT
{
    using System;

    /// <summary>
    /// A WebVTT Comment block.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-comment-block for more details.</remarks>
    public class Comment : BaseBlock
    {
        /// <summary>
        /// Creates new comment block with given content.
        /// </summary>
        /// <param name="content">Comment content.</param>
        /// <returns>Comment that was created.</returns>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-comment-block for details.</remarks>
        public static Comment Create(string content)
        {
            if (false == string.IsNullOrEmpty(content)
                && content.Contains(Constants.ArrowToken))
            {
                throw new ArgumentException(string.Format("Comment text cannot contain '{0}'.", Constants.ArrowToken));
            }

            return new Comment() { RawContent = content };
        }
    }
}
