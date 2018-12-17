namespace Media.Captions.WebVTT.Test
{
    using System;
    using System.IO;

    using Media.Captions.WebVTT;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WebVttParserTest
    {
        [TestMethod]
        public void ParseCaptions()
        {
            using (var reader = new StreamReader(new MemoryStream(Properties.Resources.SampleCaption, false)))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void ParseRegions()
        {
            using (var reader = new StreamReader(new MemoryStream(Properties.Resources.SampleRegions, false)))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void ParseMetadata()
        {
            using (var reader = new StreamReader(new MemoryStream(Properties.Resources.SampleMetadata, false)))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void ParseChapter()
        {
            using (var reader = new StreamReader(new MemoryStream(Properties.Resources.SampleChapters, false)))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfShortSignature_Throws()
        {
            string vtt = "WEB";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfIncorrectSignature_Throws()
        {
            string vtt = "WEBvtt";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfIncorrectSuffixSignature_Throws()
        {
            string vtt = "WEBVTT_";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void ParseEmptyFile()
        {
            string vtt = @"WEBVTT  
    
   
   " + "\t\t\t\r\n";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();

                Assert.IsNotNull(captions);
                Assert.IsNull(captions.Regions);
                Assert.IsNull(captions.Cues);
                Assert.IsNull(captions.Styles);
            }
        }

        [TestMethod]
        public void ParseHoursInCueTime()
        {
            string vtt = 
@"WEBVTT

00:35.123-->12345:59:59.999
Caption with hourly duration";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();

                Assert.IsNotNull(captions);
                Assert.IsNull(captions.Regions);
                Assert.IsNotNull(captions.Cues);
                Assert.IsNull(captions.Styles);

                Assert.AreEqual(1, captions.Cues.Length);
                Assert.AreEqual(TimeSpan.Parse("00:00:35.123"), captions.Cues[0].Start);
                Assert.AreEqual(TimeSpan.Parse("514.09:59:59.999"), captions.Cues[0].End);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfNoMinutesInTiming_Throw()
        {
            string vtt =
@"WEBVTT

35.123 --> 59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfNoFractionsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

00:35 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooManyDigitsInMinutesInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:000:35.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooManyDigitsInSecondsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:00:000.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfOnlyFractionsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfHoursTooLargeInTiming_Throw()
        {
            string vtt =
@"WEBVTT

12345678901234567890:00:00.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooManyFractionDigitsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

00:00.0000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooManyMinutesInTiming_Throw()
        {
            string vtt =
@"WEBVTT

60:00.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooManySecondsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

00:70.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfHasHours_And_TooManyMinutesInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:60:00.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfHasHours_And_TooManySecondsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:00:70.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooFewSecondsDigitsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

00:0.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfHasHours_And_TooFewSecondsDigitsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:00:0.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfTooFewMinutessDigitsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:00.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfHasHours_And_TooFewMinutessDigitsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:0:00.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfNonDigitsInTiming_Throw()
        {
            string vtt =
@"WEBVTT

0:__:00.000 --> 12345:59:59.999
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfEndLessThanStart_Throw()
        {
            string vtt =
@"WEBVTT

0:10:00.000 --> 0:00:00.000
Bad caption";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfRegionAfterCue_Throw()
        {
            string vtt =
@"WEBVTT

0:00:00.000 --> 0:10:00.000
Some caption

REGION
Some:data";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfStyleAfterCue_Throw()
        {
            string vtt =
@"WEBVTT

0:00:00.000 --> 0:10:00.000
Some caption

STYLE
Some data";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void ParseSingleLineCue()
        {
            string testSettings = "start:10%";
            string vtt =
@"WEBVTT

0:00:00.000 --> 0:10:00.000 " + testSettings;

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
                Assert.IsNotNull(captions);
                Assert.IsNotNull(captions.Cues);
                Assert.IsTrue(captions.Cues.Length == 1);
                Assert.IsNotNull(captions.Cues[0].RawSettings);
                Assert.AreEqual(testSettings, captions.Cues[0].RawSettings);
                Assert.IsNull(captions.Cues[0].RawContent);
            }
        }

        [TestMethod]
        public void ParseRegion()
        {
            string region = @"id:fred
width:40%
lines:3
regionanchor:0%,100%
viewportanchor:10%,90%
scroll:up";
            string vtt =
@"WEBVTT

REGION
" + region +
@"
";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
                Assert.IsNotNull(captions);
                Assert.IsNotNull(captions.Regions);
                Assert.IsTrue(captions.Regions.Length == 1);
                Assert.IsNotNull(captions.Regions[0].RawContent);
                Assert.AreEqual(region, captions.Regions[0].RawContent);

                Assert.AreEqual("fred", captions.Regions[0].Id, "IDs are different.");
                Assert.AreEqual(3, captions.Regions[0].Lines.Value, "Lines are different.");
                Assert.AreEqual(true, captions.Regions[0].Scroll.Value, "Scrolls are different.");
                Assert.AreEqual(40.0, captions.Regions[0].WidthPercent.Value, "Widths are different.");
                Assert.AreEqual(0.0, captions.Regions[0].RegionAnchor.Value.XPercent, "Region anchor Xs are different.");
                Assert.AreEqual(100.0, captions.Regions[0].RegionAnchor.Value.YPercent, "Region anchor Ys are different.");
                Assert.AreEqual(10.0, captions.Regions[0].ViewPortAnchor.Value.XPercent, "Viewport anchor Xs are different.");
                Assert.AreEqual(90.0, captions.Regions[0].ViewPortAnchor.Value.YPercent, "Viewport anchor Ys are different.");
            }
        }

        [TestMethod]
        public void ParseRegionWithInvalidSettings()
        {
            string region = @"id:fred
width:40
lines:_
regionanchor:0,100%
viewportanchor:10,90%
scroll:down";
            string vtt =
@"WEBVTT

REGION
" + region + @"
";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
                Assert.IsNotNull(captions);
                Assert.IsNotNull(captions.Regions);
                Assert.IsTrue(captions.Regions.Length == 1);
                Assert.IsNotNull(captions.Regions[0].RawContent);
                Assert.AreEqual(region, captions.Regions[0].RawContent);

                Assert.AreEqual("fred", captions.Regions[0].Id, "IDs are different.");
                Assert.IsNull(captions.Regions[0].Lines);
                Assert.AreEqual(false, captions.Regions[0].Scroll.Value, "Scrolls are different.");
                Assert.IsNull(captions.Regions[0].WidthPercent);
                Assert.IsNull(captions.Regions[0].RegionAnchor);
                Assert.IsNull(captions.Regions[0].ViewPortAnchor);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfNoColonInSettings_Throws()
        {
            string region = @"id-fred";
            string vtt =
@"WEBVTT

REGION
" + region + @"
";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void IfNoValueAfterColonInSettings_Throws()
        {
            string region = @"width:";
            string vtt =
@"WEBVTT

REGION
" + region + @"
";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void ParseStyle()
        {
            string style = @"::cue {
  background-image: linear-gradient(to bottom, dimgray, lightgray);
  color: papayawhip;
}";
            string vtt =
@"WEBVTT

STYLE
" + style +
@"
";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
                Assert.IsNotNull(captions);
                Assert.IsNotNull(captions.Styles);
                Assert.IsTrue(captions.Styles.Length == 1);
                Assert.IsNotNull(captions.Styles[0].RawContent);
                Assert.AreEqual(style, captions.Styles[0].RawContent);
            }
        }

        [TestMethod]
        public void ParseCaptionWithSettings()
        {
            string caption = @"align:right size:50% vertical:lr line:3%,center position:15%,line-right region:r";
            string vtt =
@"WEBVTT

00:30.000 --> 00:31.500 " + caption + @"
<v Roger Bingham>We are in New York City";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
                Assert.IsNotNull(captions);
                Assert.IsNotNull(captions.Cues);
                Assert.IsTrue(captions.Cues.Length == 1);
                Assert.IsNotNull(captions.Cues[0].RawSettings);
                Assert.AreEqual(caption, captions.Cues[0].RawSettings);

                Assert.AreEqual("r", captions.Cues[0].Region, "Regions are different.");
                Assert.AreEqual(TextAlignment.Right, captions.Cues[0].Alignment.Value, "Alignments are different.");
                Assert.AreEqual(50, captions.Cues[0].SizePercent.Value, "Sizes are different.");
                Assert.AreEqual(VerticalTextLayout.LeftToRight, captions.Cues[0].Vertical.Value, "Verticals are different.");
                Assert.AreEqual(LineAlignment.Center, captions.Cues[0].Line.Value.Alignment.Value, "Line.Alignments are different.");
                Assert.AreEqual(3.0, captions.Cues[0].Line.Value.Percent.Value, "Line.Percents are different.");
                Assert.AreEqual(PositionAlignment.LineRight, captions.Cues[0].Position.Value.Alignment.Value, "Position.Alignments are different.");
                Assert.AreEqual(15.0, captions.Cues[0].Position.Value.PositionPercent.Value, "Position.Percents are different.");
            }
        }

        [TestMethod]
        public void ParseCaptionIgnoreBadSettings()
        {
            string caption = @"align:bad size:50 vertical:no line:%,center position:15%_line-right random:r";
            string vtt =
@"WEBVTT

00:30.000 --> 00:31.500 " + caption + @"
<v Roger Bingham>We are in New York City";

            using (var reader = new StringReader(vtt))
            {
                var captions = WebVttParser.ReadMediaCaptionsAsync(reader).ConfigureAwait(false).GetAwaiter().GetResult();
                Assert.IsNotNull(captions);
                Assert.IsNotNull(captions.Cues);
                Assert.IsTrue(captions.Cues.Length == 1);
                Assert.IsNotNull(captions.Cues[0].RawSettings);
                Assert.AreEqual(caption, captions.Cues[0].RawSettings);

                Assert.IsNull(captions.Cues[0].Alignment);
                Assert.IsNull(captions.Cues[0].SizePercent);
                Assert.IsNull(captions.Cues[0].Vertical);
                Assert.IsNull(captions.Cues[0].Line);
                Assert.IsNull(captions.Cues[0].Position);
            }
        }
    }
}
