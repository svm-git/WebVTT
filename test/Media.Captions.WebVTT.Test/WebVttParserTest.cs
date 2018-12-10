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
Some data";

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
    }
}
