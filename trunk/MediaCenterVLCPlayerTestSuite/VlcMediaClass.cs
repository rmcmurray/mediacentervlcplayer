using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaCenterVLCPlayer;

namespace MediaCenterVLCPlayerTestSuite
{
    [TestClass]
    public class VlcMediaClass
    {
        public string BaseTestFilePath = @"..\..\..\MediaCenterVLCPlayerTestSuite\Test Files\";
        public string[] VlcInstanceArgs = new string[] {
                "-I", "dummy", "--ignore-config",
                @"--plugin-path=" + MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
            };

        [TestMethod]
        public void BaseTest()
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

            player = instance.CreatePlayer(BaseTestFilePath + "trn_anaglyph_adj.mpg");
            Assert.IsInstanceOfType(player, typeof(VlcMediaPlayer));
            player.Play();
            Assert.AreEqual(player.Media.Length, 6113);
            player.Stop();
            player.Dispose();

            instance.Dispose();
        }

        [TestMethod]
        public void TestAudioTracks()
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

        [TestMethod]
        public void TestSubtitles()
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

            Assert.AreEqual("Disable", ((VLCLib.libvlc_track_description_t)subtitles[0]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(0));
            Assert.AreEqual("Track 1 - [English]", ((VLCLib.libvlc_track_description_t)subtitles[1]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(1));
            Assert.AreEqual("Track 2 - [Nederlands]", ((VLCLib.libvlc_track_description_t)subtitles[2]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(2));
            Assert.AreEqual("Track 3 - [English]", ((VLCLib.libvlc_track_description_t)subtitles[3]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(3));
            Assert.AreEqual("Track 4 - [Suomi]", ((VLCLib.libvlc_track_description_t)subtitles[4]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(4));
            Assert.AreEqual("Track 5 - [FranÃ§ais]", ((VLCLib.libvlc_track_description_t)subtitles[5]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(5));
            Assert.AreEqual("Track 6 - [Deutsch]", ((VLCLib.libvlc_track_description_t)subtitles[6]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(6));
            Assert.AreEqual("Track 7 - [×¢×‘×¨×™×ª]", ((VLCLib.libvlc_track_description_t)subtitles[7]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(7));
            Assert.AreEqual("Track 8 - [Magyar]", ((VLCLib.libvlc_track_description_t)subtitles[8]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(8));
            Assert.AreEqual("Track 9 - [Italiano]", ((VLCLib.libvlc_track_description_t)subtitles[9]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(9));
            Assert.AreEqual("Track 10 - [æ—¥æœ¬èªž]", ((VLCLib.libvlc_track_description_t)subtitles[10]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(10));
            Assert.AreEqual("Track 11 - [Norsk]", ((VLCLib.libvlc_track_description_t)subtitles[11]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(11));
            Assert.AreEqual("Track 12 - [Polski]", ((VLCLib.libvlc_track_description_t)subtitles[12]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(12));
            Assert.AreEqual("Track 13 - [PortuguÃªs]", ((VLCLib.libvlc_track_description_t)subtitles[13]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(13));
            Assert.AreEqual("Track 14 - [Ð ÑƒÑÑÐºÐ¸Ð¹]", ((VLCLib.libvlc_track_description_t)subtitles[14]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(14));
            Assert.AreEqual("Track 15 - [EspaÃ±ol]", ((VLCLib.libvlc_track_description_t)subtitles[15]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(15));
            Assert.AreEqual("Track 16 - [Svenska]", ((VLCLib.libvlc_track_description_t)subtitles[16]).psz_name);
            Assert.IsTrue(player.ChangeSubtitleTrack(16));

            player.Stop();
            player.Dispose();

            instance.Dispose();
        }
    }
}
