using MediaCenterVLCPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace MediaCenterVLCPlayerTestSuite
{
    
    
    /// <summary>
    ///This is a test class for MCRemoteControlManagerTest and is intended
    ///to contain all MCRemoteControlManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MCRemoteControlManagerTest
    {


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
        ///A test for MCRemoteControlManager Constructor
        ///</summary>
        [TestMethod()]
        public void MCRemoteControlManagerConstructorTest()
        {
            MCRemoteControlManager target = new MCRemoteControlManager();
            Assert.IsInstanceOfType(target, typeof(MCRemoteControlManager));
        }

        /// <summary>
        ///A test for MilliSecondsFromSettings
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MediaCenterVLCPlayer.exe")]
        public void MilliSecondsFromSettingsTest()
        {
            MCRemoteControlManager_Accessor target = new MCRemoteControlManager_Accessor();
            int seconds = 10;
            int expected = 10000;
            int actual;
            actual = target.MilliSecondsFromSettings(seconds);
            Assert.AreEqual(expected, actual);
        }
    }
}
