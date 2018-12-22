using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Captions.WebVTT
{
    /// <summary>
    /// A WebVTT Style block.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-style-block for more details.</remarks>
    public class Style : BaseBlock
    {
        /// <summary>
        /// Creates new style block.
        /// </summary>
        /// <param name="content">Style content.</param>
        /// <returns>Style that was created.</returns>
        /// <remarks>See http://www.w3.org/TR/webvtt1/#webvtt-style-block for more details.</remarks>
        public static Style Create(string content)
        {
            if (false == string.IsNullOrEmpty(content)
                && content.Contains(Constants.ArrowToken))
            {
                throw new ArgumentException(string.Format("Comment text cannot contain '{0}'.", Constants.ArrowToken));
            }

            return new Style() { RawContent = content };
        }
    }
}
