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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;
using System;

namespace UnisensUnitTest
{
    [TestClass]

    public class EventEntryImplTest : TestProperties
    {
        public static EventEntry eventEntry;
        public static Unisens unisens;
        public static UnisensFactory factory;
        public static List<Event> expected;

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
            String TestLocation = CopyExampleToTest(EXAMPLE2);
            factory = UnisensFactoryBuilder.createFactory();
            unisens = factory.createUnisens(TestLocation);
            eventEntry = (EventEntry)unisens.getEntry("events.csv");
            expected = new List<Event>();
            expected.Add(new Event(30, "A1", "liegen"));
            expected.Add(new Event(500, "A2", "sitzen"));
            expected.Add(new Event(1343, "A3", "stehen"));
            expected.Add(new Event(3200, "A4", "gehen"));
            expected.Add(new Event(5600, "A5", "treppe hoch"));
            expected.Add(new Event(5600, "A6", ""));
        }

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
        }

        [TestMethod]
        public void testReadInt()
        {
            assertEventList(expected.GetRange(0, 3), eventEntry.read(0, 3));
            assertEventList(expected.GetRange(3, 3), eventEntry.read(3));

        }

        [TestMethod]
        public void testReadLongInt()
        {
            assertEventList(expected.GetRange(0, 6), eventEntry.read(0, 6));
            assertEventList(expected.GetRange(3, 3), eventEntry.read(3, 6));
        }

        [TestMethod]
        public void testAppendEvent()
        {
            Event newEvent = new Event(6000, "A7", "liegen");
            expected.Add(newEvent);
            eventEntry.append(newEvent);
            assertEventList(expected, eventEntry.read(0, 7));
            expected.Remove(newEvent);
            eventEntry.empty();
            eventEntry.append(expected);
            assertEventList(expected, eventEntry.read(0, 6));
            unisens.closeAll();
            unisens = factory.createUnisens(EXAMPLE2);
        }

        [TestMethod]
        public void testAppendMultipleEvents()
        {
            Event newEvent1 = new Event(6000, "A7", "liegen");
            Event newEvent2 = new Event(7000, "A8", "stehen");
            Event newEvent3 = new Event(8000, "A9", "liegen");

            expected.Add(newEvent1);
            expected.Add(newEvent2);
            expected.Add(newEvent3);

            eventEntry.append(newEvent1);
            eventEntry.append(newEvent2);
            eventEntry.append(newEvent3);
            assertEventList(expected, eventEntry.read(0, 9));

            expected.Remove(newEvent1);
            expected.Remove(newEvent2);
            expected.Remove(newEvent3);

            eventEntry.empty();
            eventEntry.append(expected);
            assertEventList(expected, eventEntry.read(0, 6));

            unisens.closeAll();
            unisens = factory.createUnisens(EXAMPLE2);
        }


        [TestMethod]
        public void testClone()
        {
            EventEntry clonedEventEntry = (EventEntry)eventEntry.clone<EventEntry>();
            Assert.AreEqual(eventEntry.getTypeLength(), clonedEventEntry.getTypeLength());
            Assert.AreEqual(eventEntry.getCommentLength(), clonedEventEntry.getCommentLength());
            Assert.AreEqual(eventEntry.getFileFormat().getFileFormatName(), clonedEventEntry.getFileFormat().getFileFormatName());
            Assert.AreEqual(eventEntry.getFileFormat().getComment(), clonedEventEntry.getFileFormat().getComment());
            Assert.AreEqual(eventEntry.getId(), clonedEventEntry.getId());
            Assert.AreEqual(eventEntry.getSampleRate(), clonedEventEntry.getSampleRate(), 0);
            Assert.AreEqual(eventEntry.getSource(), clonedEventEntry.getSource());
            Assert.AreEqual(eventEntry.getUnisens(), clonedEventEntry.getUnisens());
        }
    }
}