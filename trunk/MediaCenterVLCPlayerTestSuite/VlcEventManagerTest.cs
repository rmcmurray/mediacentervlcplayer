using MediaCenterVLCPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaCenterVLCPlayerTestSuite
{
    
    
    /// <summary>
    ///This is a test class for VlcEventManagerTest and is intended
    ///to contain all VlcEventManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VlcEventManagerTest
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

/*
        /// <summary>
        ///A test for VlcEventManager Constructor
        ///</summary>
        [TestMethod()]
        public void VlcEventManagerConstructorTest()
        {
            IntPtr p_event_manager = new IntPtr(); // TODO: Initialize to an appropriate value
            VlcEventManager target = new VlcEventManager(p_event_manager);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for InitalizeEvents
        ///</summary>
        [TestMethod()]
        public void InitalizeEventsTest()
        {
            IntPtr p_event_manager = new IntPtr(); // TODO: Initialize to an appropriate value
            VlcEventManager target = new VlcEventManager(p_event_manager); // TODO: Initialize to an appropriate value
            target.InitalizeEvents();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsAttached
        ///</summary>
        [TestMethod()]
        public void IsAttachedTest()
        {
            IntPtr p_event_manager = new IntPtr(); // TODO: Initialize to an appropriate value
            VlcEventManager target = new VlcEventManager(p_event_manager); // TODO: Initialize to an appropriate value
            VlcEventType type = new VlcEventType(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsAttached(type);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MediaPlayerEnded
        ///</summary>
        [TestMethod()]
        public void MediaPlayerEndedTest()
        {
            IntPtr p_event_manager = new IntPtr(); // TODO: Initialize to an appropriate value
            VlcEventManager target = new VlcEventManager(p_event_manager); // TODO: Initialize to an appropriate value
            VlcEventType type = new VlcEventType(); // TODO: Initialize to an appropriate value
            VlcEventType typeExpected = new VlcEventType(); // TODO: Initialize to an appropriate value
            IntPtr userdata = new IntPtr(); // TODO: Initialize to an appropriate value
            target.MediaPlayerEnded(ref type, userdata);
            Assert.AreEqual(typeExpected, type);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
*/
    }
}
