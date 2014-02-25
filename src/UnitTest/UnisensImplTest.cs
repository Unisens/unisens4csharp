using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;
using System.IO;

namespace UnisensUnitTest
{
    /// <summary>
    /// Summary description for UnisensImplTest
    /// </summary>
    [TestClass]
    public class UnisensImplTest : TestProperties
    {
        public static Unisens unisens1;
        public static Unisens unisens2;
        public static UnisensFactory factory;

        public UnisensImplTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            String Test1Location = CopyExampleToTest(EXAMPLE1);
            String Test2Location = CopyExampleToTest(EXAMPLE2);
            factory = UnisensFactoryBuilder.createFactory();
            unisens1 = factory.createUnisens(Test1Location);
            unisens2 = factory.createUnisens(Test2Location);
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            unisens1.closeAll();
            unisens2.closeAll();
        }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /**
         * Test method for {@link org.unisens1.ri.unisens1Impl#getCustomAttributes()}.
         */
        [TestMethod]
        public void testGetCustomAttributes()
        {
            Dictionary<String, String> customAttributes = new Dictionary<String, String>();
            customAttributes.Add("customKey1", "customValue1");
            customAttributes.Add("customKey2", "customValue2");
            assertDictionaryEquals(customAttributes, unisens1.getCustomAttributes());
        }

        /**
         * Test method for {@link org.unisens1.ri.unisens1Impl#createSignalEntry(java.lang.String, java.lang.String[], org.unisens1.DataType, double)}.
         */
        [TestMethod]
        public void testCreateSignalEntry()
        {
            SignalEntry signalEntry = unisens1.createSignalEntry("signalEntry.bin", new String[] { "channel A", "channel B" }, DataType.INT32, 1000);
            Assert.IsNotNull(unisens1.getEntry("signalEntry.bin"));

            String[] testString = new String[] { "channel A", "channel B" };
            for (int i = 0; i < testString.Length; i++)
            {
                Assert.AreEqual(testString[i], signalEntry.getChannelNames()[i]);
            }

            Assert.AreEqual(DataType.INT32, signalEntry.getDataType());
            Assert.AreEqual(1000, signalEntry.getSampleRate());
            Assert.AreEqual(2, signalEntry.getChannelCount());
            Assert.IsNotNull(signalEntry.getUnisens());
        }

        [TestMethod]
        public void testCreateGroup()
        {
            Group group = unisens1.createGroup("group123");
            Assert.AreEqual(0, group.getEntries().Count);
            var newGroup = unisens1.getGroup("group123");
            Assert.AreEqual(newGroup.getId(), group.getId());
            Assert.AreEqual(newGroup.getComment(), group.getComment());
            Assert.AreEqual(newGroup.getEntries().Count, group.getEntries().Count);
        }


        [TestMethod]
        public void testCreateEventEntry()
        {
            EventEntry eventEntry = unisens1.createEventEntry("eventEntry.csv", 400);
            Assert.IsNotNull(unisens1.getEntry("eventEntry.csv"));
            Assert.AreEqual("eventEntry.csv", eventEntry.getId());
            Assert.AreEqual(400, eventEntry.getSampleRate());
            Assert.IsNotNull(eventEntry.getUnisens());
        }

        [TestMethod]
        public void testCreateValuesEntry()
        {
            ValuesEntry valuesEntry = unisens1.createValuesEntry("valuesEntry.csv", new String[] { "channel 1", "channel 2" }, DataType.DOUBLE, 400);
            Assert.IsNotNull(unisens1.getEntry("valuesEntry.csv"));

            String[] testString = new String[] { "channel 1", "channel 2" };
            for (int i = 0; i < testString.Length; i++)
            {
                Assert.AreEqual(testString[i], valuesEntry.getChannelNames()[i]);
            }

            Assert.AreEqual(DataType.DOUBLE, valuesEntry.getDataType());
            Assert.AreEqual(400, valuesEntry.getSampleRate());
            Assert.AreEqual(2, valuesEntry.getChannelCount());
            Assert.IsNotNull(valuesEntry.getUnisens());
        }

        [TestMethod]
        public void testCreateCustomEntry()
        {
            CustomEntry customEntry = unisens1.createCustomEntry("customEntry.txt");
            Assert.IsNotNull(unisens1.getEntry("customEntry.txt"));
            Assert.AreEqual("customEntry.txt", customEntry.getId());
            Assert.IsNotNull(customEntry.getUnisens());
            unisens1.deleteEntry(customEntry);
            Assert.IsNull(unisens1.getEntry("customEntry.txt"));
        }

        [TestMethod]
        public void testAddGroup()
        {
            Group group2 = unisens2.getGroups()[0];
            Group group1 = unisens1.addGroup(group2, true);

            Assert.IsNotNull(unisens1.getGroup(group2.getId()));
            Assert.AreEqual(group2.getComment(), group1.getComment());
            foreach (Entry entry in group2.getEntries())
                Assert.IsNotNull(unisens1.getEntry(entry.getId()));
        }


        [TestMethod]
        public void testAddEntry()
        {
            Entry entry = unisens2.createEventEntry("eventEntry2.csv", 400);
            unisens1.addEntry(entry, true);
            Assert.IsNotNull(unisens1.getEntry(entry.getId()));
            Assert.AreEqual(entry.getFileFormat().getFileFormatName(), unisens1.getEntry(entry.getId()).getFileFormat().getFileFormatName());
        }

        [TestMethod]
        public void testGetEntry()
        {
            Assert.IsNotNull(unisens1.getEntry("ecg.bin"));
            Assert.IsNull(unisens1.getEntry("12321"));
        }

        [TestMethod]
        public void testDeleteEntry()
        {
            List<Entry> entries = new List<Entry>(unisens2.getGroup("group2").getEntries());
            foreach (Entry entry in entries)
            {
                Assert.IsNotNull(unisens2.getEntry(entry.getId()));
                unisens2.deleteEntry(entry);
                Assert.IsNull(unisens2.getEntry(entry.getId()));
            }
            Assert.IsNotNull(unisens2.getEntry("events.csv"));
            unisens2.deleteEntry(unisens2.getEntry("events.csv"));
            Assert.IsNull(unisens2.getEntry("events.csv"));
            Assert.IsFalse(File.Exists(unisens2.getPath() + "/" + "events.csv"));
        }


        [TestMethod]
        public void testDeleteGroup()
        {
            var group2 = unisens2.getGroup("group2");
            Assert.IsNotNull(group2);
            unisens2.deleteGroup(group2);
            Assert.IsNull(unisens2.getGroup("group2"));
            unisens2.addGroup(group2, false);
        }


        [TestMethod]
        public void testSave()
        {
            //fail("Not yet implemented");
        }
    }
}
