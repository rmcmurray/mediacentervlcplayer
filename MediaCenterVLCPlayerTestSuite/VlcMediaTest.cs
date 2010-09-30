using MediaCenterVLCPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace MediaCenterVLCPlayerTestSuite
{
    
    
    /// <summary>
    ///This is a test class for VlcMediaTest and is intended
    ///to contain all VlcMediaTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VlcMediaTest
    {
        public string BaseTestFilePath = @"..\..\..\MediaCenterVLCPlayerTestSuite\Test Files\";
        public string[] VlcInstanceArgs = new string[] {
                "-I", "dummy", "--ignore-config",
                @"--plugin-path=" + MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
            };

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for VlcMedia Constructor
        ///</summary>
        [TestMethod()]
        public void VlcMediaConstructorTest()
        {
            VlcInstance instance = null;
            VlcMediaPlayer player = null;
            instance = new VlcInstance(VlcInstanceArgs);
            player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            Assert.IsInstanceOfType(player, typeof(VlcMediaPlayer));
            player.Play();
            Assert.AreEqual(player.Media.Length, 5000);
            player.Stop();
            player.Dispose();
        }

        /// <summary>
        ///A test for AspectRatio
        ///</summary>
        [TestMethod()]
        public void AspectRatioTest()
        {
            VlcInstance instance = new VlcInstance(VlcInstanceArgs);
            VlcMediaPlayer player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            Assert.AreEqual("1.66", player.Media.AspectRatio);
            player.Stop();
            player.Dispose();
            instance.Dispose();
        }

        /// <summary>
        ///A test for AudioTracks
        ///</summary>
        [TestMethod()]
        public void AudioTracksTest()
        {
            VlcInstance instance = null;
            VlcMediaPlayer player = null;
            instance = new VlcInstance(VlcInstanceArgs);
            player = instance.CreatePlayer(BaseTestFilePath + "mewmew-vorbis-ssa.mkv");
            Assert.IsInstanceOfType(player, typeof(VlcMediaPlayer));
            player.Play();
            Assert.AreEqual(player.Media.Length, 58183);

            player.Stop();
            player.Dispose();

            instance.Dispose();
        }

        /// <summary>
        ///A test for Length
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            VlcInstance instance = null;
            VlcMediaPlayer player = null;
            instance = new VlcInstance(VlcInstanceArgs);
            player = instance.CreatePlayer(BaseTestFilePath + "mewmew-vorbis-ssa.mkv");
            Assert.IsInstanceOfType(player, typeof(VlcMediaPlayer));
            player.Play();
            Assert.AreEqual(player.Media.Length, 58183);

            player.Stop();
            player.Dispose();

            instance.Dispose();
        }

        /// <summary>
        ///A test for SubtitleTracks
        ///</summary>
        [TestMethod()]
        public void SubtitleTracksTest()
        {
            VlcInstance instance = null;
            VlcMediaPlayer player = null;
            instance = new VlcInstance(VlcInstanceArgs);
            player = instance.CreatePlayer(BaseTestFilePath + "mewmew-vorbis-ssa.mkv");
            Assert.IsInstanceOfType(player, typeof(VlcMediaPlayer));
            player.Play();
            Assert.AreEqual(player.Media.Length, 58183);
            ArrayList subtitles = player.Media.SubtitleTracks;
            Assert.AreEqual(subtitles.Count, 17);

            Assert.AreEqual("Disable", ((VLCLibrary.libvlc_track_description_t)subtitles[0]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(0));
            Assert.AreEqual("Track 1 - [English]", ((VLCLibrary.libvlc_track_description_t)subtitles[1]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(1));
            Assert.AreEqual("Track 2 - [Nederlands]", ((VLCLibrary.libvlc_track_description_t)subtitles[2]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(2));
            Assert.AreEqual("Track 3 - [English]", ((VLCLibrary.libvlc_track_description_t)subtitles[3]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(3));
            Assert.AreEqual("Track 4 - [Suomi]", ((VLCLibrary.libvlc_track_description_t)subtitles[4]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(4));
            Assert.AreEqual("Track 5 - [FranÃ§ais]", ((VLCLibrary.libvlc_track_description_t)subtitles[5]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(5));
            Assert.AreEqual("Track 6 - [Deutsch]", ((VLCLibrary.libvlc_track_description_t)subtitles[6]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(6));
            Assert.AreEqual("Track 7 - [×¢×‘×¨×™×ª]", ((VLCLibrary.libvlc_track_description_t)subtitles[7]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(7));
            Assert.AreEqual("Track 8 - [Magyar]", ((VLCLibrary.libvlc_track_description_t)subtitles[8]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(8));
            Assert.AreEqual("Track 9 - [Italiano]", ((VLCLibrary.libvlc_track_description_t)subtitles[9]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(9));
            Assert.AreEqual("Track 10 - [æ—¥æœ¬èªž]", ((VLCLibrary.libvlc_track_description_t)subtitles[10]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(10));
            Assert.AreEqual("Track 11 - [Norsk]", ((VLCLibrary.libvlc_track_description_t)subtitles[11]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(11));
            Assert.AreEqual("Track 12 - [Polski]", ((VLCLibrary.libvlc_track_description_t)subtitles[12]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(12));
            Assert.AreEqual("Track 13 - [PortuguÃªs]", ((VLCLibrary.libvlc_track_description_t)subtitles[13]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(13));
            Assert.AreEqual("Track 14 - [Ð ÑƒÑÑÐºÐ¸Ð¹]", ((VLCLibrary.libvlc_track_description_t)subtitles[14]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(14));
            Assert.AreEqual("Track 15 - [EspaÃ±ol]", ((VLCLibrary.libvlc_track_description_t)subtitles[15]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(15));
            Assert.AreEqual("Track 16 - [Svenska]", ((VLCLibrary.libvlc_track_description_t)subtitles[16]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(16));

            player.Stop();
            player.Dispose();

            instance.Dispose();
        }

        /// <summary>
        ///A test for TrackCount
        ///</summary>
        /*
        [TestMethod()]
        public void TrackCountTest()
        {
            IntPtr handle = new IntPtr(); // TODO: Initialize to an appropriate value
            VlcMedia target = new VlcMedia(handle); // TODO: Initialize to an appropriate value
            int actual;
            actual = target.TrackCount;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        */
    }
}
