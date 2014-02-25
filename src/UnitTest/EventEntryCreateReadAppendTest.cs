/*
Unisens Tests - tests for a universal sensor data format
Copyright (C) 2008 FZI Research Center for Information Technology, Germany
                   Institute for Information Processing Technology (ITIV),
				   KIT, Germany

This file is part of the Unisens Tests. For more information, see
<http://www.unisens.org>

The Unisens Tests is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Unisens Tests is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with the Unisens Tests. If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;
using System.IO;
using org.unisens.ri;

namespace UnisensUnitTest
{
    [TestClass]

    public class EventEntryCreateReadAppendTest : TestProperties
    {
        public static UnisensFactory factory;
        public static Unisens unisens;
        public static double sampleRate = 1000;
        public static DateTime timestampStart;

        public EventEntry eventEntry;
        public static List<Event> events = new List<Event>();

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

        [ClassInitialize()]
        public static void setUpBeforeClass(TestContext testContext)
        {
            Directory.CreateDirectory(EXAMPLE_TEMP_EVENT_ENTRY);
            factory = UnisensFactoryBuilder.createFactory();
            unisens = factory.createUnisens(EXAMPLE_TEMP_EVENT_ENTRY);
            timestampStart = new DateTime();
            unisens.setTimestampStart(timestampStart);
            unisens.setMeasurementId("Temp events");
            events.Add(new Event(30, "A1", "liegen"));
            events.Add(new Event(500, "A2", "sitzen"));
            events.Add(new Event(1343, "A3", "stehen"));
            events.Add(new Event(3200, "A4", "gehen"));
            events.Add(new Event(5600, "A5", "treppe hoch"));
            events.Add(new Event(5600, "A6", ""));
            events.Add(new Event(int.MaxValue - 1, "A7", ""));
            events.Add(new Event(0, "A8", ""));
        }

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
            unisens.save();
        }

        [TestMethod]
        public void testEventEntry_BIN()
        {
            eventEntry = unisens.createEventEntry("ee.bin", sampleRate);
            eventEntry.setTypeLength(2);
            eventEntry.setCommentLength(11);
            eventEntry.setFileFormat(new BinFileFormatImpl());
            eventEntry.append(events);
            assertEventListEquals(events, eventEntry.read(events.Count));
        }

        [TestMethod]
        public void testEventEntry_CSV()
        {
            eventEntry = unisens.createEventEntry("ee.csv", sampleRate);
            ((CsvFileFormat)eventEntry.getFileFormat()).setSeparator("\t");
            eventEntry.append(events);
            assertEventListEquals(events, eventEntry.read(events.Count));
        }

        [TestMethod]
        public void testEventEntry_XML()
        {
            eventEntry = unisens.createEventEntry("ee.xml", sampleRate);
            eventEntry.setFileFormat(new XmlFileFormatImpl());
            eventEntry.append(events);
            assertEventListEquals(events, eventEntry.read(events.Count));
        }

        [TestMethod]
        public void testSaveUnisens()
        {
            eventEntry = unisens.createEventEntry("test.bin", sampleRate);
            eventEntry.setTypeLength(2);
            eventEntry.setCommentLength(11);
            eventEntry.setFileFormat(new BinFileFormatImpl());
            eventEntry.append(events);
            unisens.closeAll();
            unisens.save();
            unisens = factory.createUnisens(EXAMPLE_TEMP_EVENT_ENTRY);
            Assert.AreEqual(timestampStart.ToString(), unisens.getTimestampStart().ToString());
            eventEntry = (EventEntry)unisens.getEntry("test.bin");
            Assert.AreEqual("BIN", eventEntry.getFileFormat().getFileFormatName());
            Assert.AreEqual(sampleRate, eventEntry.getSampleRate(), 0);
            Assert.IsTrue(eventEntry.getFileFormat() is BinFileFormat);
            Assert.AreEqual(Endianess.LITTLE, ((BinFileFormat)eventEntry.getFileFormat()).getEndianess());
        }

        public void assertEventListEquals(List<Event> expected, List<Event> actual)
        {
            Assert.IsTrue(expected.Count == actual.Count);
            for (int i = 0; i < expected.Count; i++)
                Assert.IsTrue(expected[i].Equals(actual[i]));
        }
    }
}