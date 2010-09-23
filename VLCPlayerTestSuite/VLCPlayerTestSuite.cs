using System;
using MediaCenterVLCPlayer;
using NUnit.Framework;

namespace VLCPlayerTestSuite
{
    [TestFixture]
    public class VLCPlayerTestSuite
    {

        [Test]
        public void DoesItRun()
        {
            MediaCenterVLCPlayer.Form1 app = new MediaCenterVLCPlayer.Form1("");
        }
    }
}
