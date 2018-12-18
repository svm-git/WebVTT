namespace Media.Captions.WebVTT
{
    /// <summary>
    /// Utility class for various string literals defined in WebVTT format.
    /// </summary>
    /// <remarks>See http://www.w3.org/TR/webvtt1/#syntax for details.</remarks>
    internal static class Constants
    {
        public const string WebVttHeaderToken = "WEBVTT";
        public const string RegionToken = "REGION";
        public const string StyleToken = "STYLE";
        public const string CommentToken = "NOTE";
        public const string ArrowToken = "-->";

        public const string RegionIdName = "id";
        public const string WidthName = "width";
        public const string LinesName = "lines";
        public const string RegionAnchorName = "regionanchor";
        public const string ViewPortAnchorName = "viewportanchor";
        public const string ScrollName = "scroll";
        
        public const string VerticalName = "vertical";
        public const string LineName = "line";
        public const string PositionName = "position";
        public const string SizeName = "size";
        public const string AlignName = "align";
        public const string RegionName = "region";

        public const string ScrollUpValue = "up";
        public const string StartValue = "start";
        public const string CenterValue = "center";
        public const string EndValue = "end";
        public const string LeftValue = "left";
        public const string RightValue = "right";
        public const string LineLeftValue = "line-left";
        public const string LineRightValue = "line-right";
        public const string RightToLeftValue = "rl";
        public const string LeftToRightValue = "lr";

        public const string ClassSpanName = "c";
        public const string ItalicsSpanName = "i";
        public const string BoldSpanName = "b";
        public const string UnderlineSpanName = "u";
        public const string RubySpanName = "ruby";
        public const string RubyTextSpanName = "rt";
        public const string VoiceSpanName = "v";
        public const string LanguageSpanName = "lang";
    }
}
