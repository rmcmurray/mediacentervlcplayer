using MediaCenterVLCPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaCenterVLCPlayerTestSuite
{      
    /// <summary>
    ///This is a test class for VlcMediaPlayerTest and is intended
    ///to contain all VlcMediaPlayerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VlcMediaPlayerTest
    {
        public string BaseTestFilePath = @"..\..\..\MediaCenterVLCPlayerTestSuite\Test Files\";
        public string[] VlcInstanceArgs = new string[] {
                "-I", "dummy", "--ignore-config",
                @"--plugin-path=" + MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
            };

        private TestContext testContextInstance;
        private VlcInstance instance = null;
        private VlcMediaPlayer player = null;

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            instance = new VlcInstance(VlcInstanceArgs);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (player != null)
            {
                if (!player.IsStopped)
                    player.Stop();

                player.Dispose();
            }

            if (instance != null)
                instance.Dispose();
        }
        //
        #endregion


        /// <summary>
        ///A test for VlcMediaPlayer Constructor
        ///</summary>
        [TestMethod()]
        public void VlcMediaPlayerConstructorTest()
        {
            VlcInstance instance = new VlcInstance(VlcInstanceArgs);
            VlcMediaPlayer player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            Assert.IsInstanceOfType(player, typeof(VlcMediaPlayer));
            Assert.IsInstanceOfType(player.Media, typeof(VlcMedia));
        }

        /// <summary>
        ///A test for ChangeSubtitleTrack
        ///</summary>
        [TestMethod()]
        public void ChangeSubtitleTrackTest()
        {
            VlcInstance instance = new VlcInstance(VlcInstanceArgs);
            VlcMediaPlayer player = instance.CreatePlayer(BaseTestFilePath + "mewmew-vorbis-ssa.mkv");
            player.Play();

            int currentSubtitleTrackId = VLCLib.libvlc_video_get_spu(player.Handle);
            player.ChangeSubtitleTrack(3);
            Assert.AreNotEqual(currentSubtitleTrackId, VLCLib.libvlc_video_get_spu(player.Handle));
        }

        /// <summary>
        ///A test for Pause
        ///</summary>
        /*
        [TestMethod()]
        public void PauseTest()
        {
            VlcInstance instance = new VlcInstance(VlcInstanceArgs);
            VlcMediaPlayer player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            Assert.IsTrue(player.Media.Time > 0);
            player.Pause();
            long currentTime= player.Media.Time;
            Assert.AreEqual(currentTime, player.Media.Time);
        }
        */
        /// <summary>
        ///A test for Play
        ///</summary>
        /*
        [TestMethod()]
        public void PlayTest()
        {
            VlcInstance instance = new VlcInstance(VlcInstanceArgs);
            VlcMediaPlayer player = instance.CreatePlayer(BaseTestFilePath + "mewmew-vorbis-ssa.mkv");
            player.Play();

            long currentTime = VLCLib.libvlc_media_player_get_time(VlcMediaPlayer.Handle);
            System.Threading.Thread.Sleep(1 * 1000);
            Assert.IsTrue(currentTime < VLCLib.libvlc_media_player_get_time(VlcMediaPlayer.Handle));
        }
        */
        /// <summary>
        ///A test for Stop
        ///</summary>
        /*
        [TestMethod()]
        public void StopTest()
        {
            VlcInstance instance = new VlcInstance(VlcInstanceArgs);
            VlcMediaPlayer player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            System.Threading.Thread.Sleep(1 * 1000);

            Assert.IsTrue(player.Media.Position > 0.0);

            player.Stop();
            float currentPos = player.Media.Position;
            Assert.AreEqual(currentPos, 0.0);
        }
        */
        /// <summary>
        ///A test for IsPaused
        ///</summary>
        [TestMethod()]
        public void IsPausedTest()
        {
            player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            player.Pause();
            Assert.IsTrue(player.IsPaused);
        }

        /// <summary>
        ///A test for IsPlaying
        ///</summary>
        [TestMethod()]
        public void IsPlayingTest()
        {
            player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            Assert.IsTrue(player.IsPlaying);
        }

        /// <summary>
        ///A test for IsStopped
        ///</summary>
        [TestMethod()]
        public void IsStoppedTest()
        {
            player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            player.Stop();
            Assert.IsTrue(player.IsStopped);
        }

        /// <summary>
        ///A test for Volume
        ///</summary>
        [TestMethod()]
        public void VolumeTest()
        {
            player = instance.CreatePlayer(BaseTestFilePath + "n900_extremely_short.avi");
            player.Play();
            int currentVolume = player.Volume;
            player.Volume = currentVolume + 10;
            Assert.IsTrue(currentVolume < player.Volume);
        }
    }
}
