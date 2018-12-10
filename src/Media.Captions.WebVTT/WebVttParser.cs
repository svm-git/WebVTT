namespace Media.Captions.WebVTT
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Parses a text in WebVTT format.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/ for more details.</remarks>
    public static class WebVttParser
    {
        private const string WebVttHeaderToken = "WEBVTT";
        private const string RegionToken = "REGION";
        private const string StyleToken = "STYLE";
        private const string CommentToken = "NOTE";
        private const string ArrowToken = "-->";

        /// <summary>
        /// Reads WebVTT media captions from a given <see cref="TextReader"/>.
        /// </summary>
        /// <param name="reader">Text reader to read the captions.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        public static async Task<MediaCaptions> ReadMediaCaptionsAsync(
            TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            // Process stream header line
            var line = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line) 
                || line.Length < 6
                || false == line.StartsWith(WebVttParser.WebVttHeaderToken)
                || line.Length >= 7 && line[6] != '\t' && line[6] != ' ')
            {
                throw new InvalidDataException("The stream does not start with the correct WebVTT file signature.");
            }

            var result = new MediaCaptions();

            // Process (skip over) optional headers from the stream
            while (false == string.IsNullOrEmpty(line))
            {
                line = await reader.ReadLineAsync();
            }

            // Process media caption blocks.
            List<RegionDefinition> regions = new List<RegionDefinition>();
            List<Style> styles = new List<Style>();
            List<Cue> cues = new List<Cue>();
            
            BaseBlock block;

            do
            {
                block = await WebVttParser.ReadBlockAsync(reader);

                RegionDefinition region = block as RegionDefinition;
                Style style = block as Style;
                Cue cue = block as Cue;

                if (cues.Count == 0)
                {
                    if (region != null)
                    {
                        regions.Add(region);
                    }
                    else if (style != null)
                    {
                        styles.Add(style);
                    }
                }
                else if (region != null || style != null)
                {
                    throw new InvalidDataException("Region or style blocks cannot be mixed with cue blocks.");
                }

                if (cue != null)
                {
                    cues.Add(cue);
                }
            }
            while (block != null);

            if (regions.Count > 0)
            {
                result.Regions = regions.ToArray();
            }

            if (styles.Count > 0)
            {
                result.Styles = styles.ToArray();
            }

            if (cues.Count > 0)
            {
                result.Cues = cues.ToArray();
            }

            return result;
        }

        /// <summary>
        /// Reads WebVTT block from the reader.
        /// </summary>
        /// <param name="reader">Text reader to read the region block.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        private static async Task<BaseBlock> ReadBlockAsync(
            TextReader reader)
        {
            var line = await reader.ReadLineAsync();

            if (string.IsNullOrEmpty(line))
            {
                return null;
            }

            if (line.StartsWith(WebVttParser.RegionToken))
            {
                return await WebVttParser.ReadRegionAsync(line, reader);
            }

            if (line.StartsWith(WebVttParser.StyleToken))
            {
                return await WebVttParser.ReadStyleAsync(line, reader);
            }

            if (line.StartsWith(WebVttParser.CommentToken))
            {
                return await WebVttParser.ReadCommentAsync(line, reader);
            }

            return await ReadCueAsync(line, reader);
        }

        /// <summary>
        /// Reads WebVTT Region definition block from the stream.
        /// </summary>
        /// <param name="firstLine">First line of the block read from the reader.</param>
        /// <param name="reader">Text reader to read the region block.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        private static async Task<RegionDefinition> ReadRegionAsync(
            string firstLine,
            TextReader reader)
        {
            string line = firstLine;
            if (line.Length > WebVttParser.RegionToken.Length
                && false == string.IsNullOrWhiteSpace(line.Substring(WebVttParser.RegionToken.Length)))
            {
                throw new InvalidDataException(string.Format("Invalid characters found after region definition header: {0}", line));
            }

            var content = new StringBuilder(100);

            while (false == string.IsNullOrEmpty(line))
            {
                line = await reader.ReadLineAsync();
                if (false == string.IsNullOrEmpty(line))
                {
                    if (line.Contains(WebVttParser.ArrowToken))
                    {
                        throw new InvalidDataException(string.Format("Region definition must not contain '{0}'.", WebVttParser.ArrowToken));
                    }

                    content.SafeAppendLine(line);
                }
            }

            return new RegionDefinition()
            {
                RawContent = content.Length > 0 ? content.ToString() : null
            };
        }

        /// <summary>
        /// Reads WebVTT Style block from the reader.
        /// </summary>
        /// <param name="firstLine">First line of the block read from the stream.</param>
        /// <param name="reader">Text reader to read the style block.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        private static async Task<Style> ReadStyleAsync(
            string firstLine,
            TextReader reader)
        {
            string line = firstLine;
            if (line.Length > WebVttParser.StyleToken.Length
                && false == string.IsNullOrWhiteSpace(line.Substring(WebVttParser.StyleToken.Length)))
            {
                throw new InvalidDataException(string.Format("Invalid characters found after style header: {0}", line));
            }

            var content = new StringBuilder(100);

            while (false == string.IsNullOrEmpty(line))
            {
                line = await reader.ReadLineAsync();
                if (false == string.IsNullOrEmpty(line))
                {
                    if (line.Contains(WebVttParser.ArrowToken))
                    {
                        throw new InvalidDataException(string.Format("Style definition must not contain '{0}'.", WebVttParser.ArrowToken));
                    }

                    content.SafeAppendLine(line);
                }
            }

            return new Style() 
            {
                RawContent = content.Length > 0 ? content.ToString() : null
            };
        }

        /// <summary>
        /// Reads WebVTT Comment block from the reader.
        /// </summary>
        /// <param name="firstLine">First line of the block read from the stream.</param>
        /// <param name="reader">Text reader to read the style block.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        private static async Task<Comment> ReadCommentAsync(
            string firstLine,
            TextReader reader)
        {
            string line = firstLine;
            var content = new StringBuilder(100);

            if (line.Length > WebVttParser.CommentToken.Length)
            {
                var inlineComment = line.Substring(WebVttParser.CommentToken.Length).TrimStart('\t', ' ');
                if (false == string.IsNullOrWhiteSpace(inlineComment))
                {
                    content.Append(inlineComment);
                }
            }

            while (false == string.IsNullOrEmpty(line))
            {
                line = await reader.ReadLineAsync();
                if (false == string.IsNullOrEmpty(line))
                {
                    content.SafeAppendLine(line);
                }
            }

            return new Comment()
            {
                RawContent = content.Length > 0 ? content.ToString() : null
            };
        }

        /// <summary>
        /// Reads WebVTT Cue block from the reader.
        /// </summary>
        /// <param name="firstLine">First line of the block read from the reader.</param>
        /// <param name="reader">Text reader to read the style block.</param>
        /// <returns>A task that represents the asynchronous read operation.</returns>
        private static async Task<Cue> ReadCueAsync(
            string firstLine,
            TextReader reader)
        {
            string line = firstLine;
            var content = new StringBuilder(100);

            Cue result = new Cue();

            if (false == line.Contains(WebVttParser.ArrowToken))
            {
                result.Id = line;
                line = await reader.ReadLineAsync();
            }

            int position = 0;
            result.Start = ParseTimeSpan(line, ref position);

            while (position < line.Length && char.IsWhiteSpace(line, position))
            {
                position++;
            }

            if (0 != string.CompareOrdinal(line, position, WebVttParser.ArrowToken, 0, WebVttParser.ArrowToken.Length))
            {
                throw new InvalidDataException(string.Format("Invalid characters found in cue timings '{0}' at position {1}.", line, position));
            }

            position += WebVttParser.ArrowToken.Length;

            while (position < line.Length && char.IsWhiteSpace(line, position))
            {
                position++;
            }

            result.End = ParseTimeSpan(line, ref position);

            if (result.End < result.Start)
            {
                throw new InvalidDataException(string.Format("Cue start time is greater than end time in '{0}'.", line));
            }

            if (position < line.Length)
            {
                var settings = line.Substring(position).TrimStart('\t', ' ').TrimEnd('\t', ' ');
                if (false == string.IsNullOrWhiteSpace(settings))
                {
                    result.RawSettings = settings;
                }
            }

            while (false == string.IsNullOrEmpty(line))
            {
                line = await reader.ReadLineAsync();
                if (false == string.IsNullOrEmpty(line))
                {
                    if (line.Contains(WebVttParser.ArrowToken))
                    {
                        throw new InvalidDataException(string.Format("Cue must not contain '{0}'.", WebVttParser.ArrowToken));
                    }

                    content.SafeAppendLine(line);
                }
            }

            if (content.Length > 0)
            {
                result.RawContent = content.ToString();
            }

            return result;
        }

        /// <summary>
        /// Parses time span value from cue timing string.
        /// </summary>
        /// <param name="line">String to process.</param>
        /// <param name="position">Current position in the line.</param>
        /// <returns>TimeSpan read from the string.</returns>
        private static TimeSpan ParseTimeSpan(string line, ref int position)
        {
            int initialPosition = position;

            int[] values = new int[4];
            int[] valueDigits = new int[4];

            int valueIndex = 0;
            
            while (position < line.Length)
            {
                char c = line[position]; 
                if (char.IsDigit(c))
                {
                    values[valueIndex] = values[valueIndex] * 10 + (c - '0');
                    valueDigits[valueIndex]++;

                    if (valueIndex >= 2 && valueDigits[valueIndex] > 3
                        || valueIndex > 0 && valueIndex < 2 && valueDigits[valueIndex] > 2)
                    {
                        throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
                    }
                }
                else if (c == '.')
                {
                    if (valueIndex < 1
                        || valueDigits[valueIndex] != 2)
                    {
                        throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
                    }

                    valueIndex++;
                }
                else if (c == ':')
                {
                    if (valueIndex > 0
                        && valueDigits[valueIndex] != 2)
                    {
                        throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
                    }

                    valueIndex++;
                }
                else if (char.IsWhiteSpace(c) || c == '-')
                {
                    if (valueDigits[valueIndex] != 3)
                    {
                        throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
                    }

                    break;
                }
                else
                {
                    throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
                }

                if (valueIndex >= values.Length)
                {
                    throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
                }

                position++;
            }

            if (valueIndex < 2)
            {
                throw new InvalidDataException(string.Format("Invalid character in cue timing value at position {0} in '{1}'.", position, line));
            }

            try
            {
                if (valueIndex == 2)
                {
                    if (values[0] > 59 || values[1] > 59
                        || valueDigits[0] != 2 || valueDigits[1] != 2 || valueDigits[2] != 3)
                    {
                        throw new InvalidDataException(string.Format("Invalid cue timing value '{0}'.", line));
                    }

                    return new TimeSpan(0, 0, values[0], values[1], values[2]);
                }

                if (values[1] > 59 || values[2] > 59
                    || valueDigits[1] != 2 || valueDigits[2] != 2 || valueDigits[3] != 3)
                {
                    throw new InvalidDataException(string.Format("Invalid cue timing value '{0}'.", line));
                }

                return new TimeSpan(values[0] / 24, values[0] % 24, values[1], values[2], values[3]);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidDataException(string.Format("Cue timing value '{0}' is too large.", line));
            }
        }
    }
}
