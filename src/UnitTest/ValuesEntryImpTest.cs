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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;

namespace UnisensUnitTest
{
    [TestClass]

    public class ValuesEntryImpTest : TestProperties
    {
        public static ValuesEntry valuesEntry;
        public static ValuesEntry tempValuesEntry;
        public static Unisens unisens;
        public static UnisensFactory factory;
        public static Value[] expectedArray;
        public static ValueList expectedValueList;

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
            valuesEntry = (ValuesEntry)unisens.getEntry("bloodpressure.csv");
            expectedArray = new Value[] { new Value(0, new short[] { 123, 82 }), new Value(600, new short[] { 124, 87 }), new Value(1200, new short[] { 130, 67 }), new Value(1800, new short[] { 118, 78 }), new Value(2400, new short[] { 142, 92 }) };
            expectedValueList = new ValueList();
            expectedValueList.setSamplestamps(new long[] { 0, 600, 1200, 1800, 2400 });
            expectedValueList.setData(new short[][] { new short[] { 123, 82 }, new short[] { 124, 87 }, new short[] { 130, 67 }, new short[] { 118, 78 }, new short[] { 142, 92 } });
            tempValuesEntry = unisens.createValuesEntry("temp_values.csv", new String[] { "a", "b" }, DataType.INT16, 400);
        }

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
        }


        [TestMethod]
        public void testReadInt()
        {
            valuesEntry.resetPos();
            Value[] actuals = valuesEntry.read(5);
            assertValueList(expectedArray, actuals);
        }

        [TestMethod]
        public void testResetPos()
        {
            valuesEntry.resetPos();
            Value[] expecteds = valuesEntry.read(5);
            valuesEntry.resetPos();
            assertValueList(expecteds, valuesEntry.read(5));
        }


        [TestMethod]
        public void testReadLongInt()
        {
            valuesEntry.resetPos();
            Value[] actuals = valuesEntry.read(0, 5);
            assertValueList(expectedArray, actuals);
            Value[] actuals01 = valuesEntry.read(0, 2);
            Value[] actuals234 = valuesEntry.read(2, 5);
            Value[] actuals01234 = new Value[] { actuals01[0], actuals01[1], actuals234[0], actuals234[1], actuals234[2] };
            assertValueList(expectedArray, actuals01234);
        }

        [TestMethod]
        public void testReadValuesListInt()
        {
            valuesEntry.resetPos();
            ValueList actualValueList = valuesEntry.readValuesList(5);
            assertValueList(expectedValueList,actualValueList);
        }


        [TestMethod]
        public void testReadValuesListLongInt()
        {
            valuesEntry.resetPos();
            ValueList actualValueList = valuesEntry.readValuesList(0, 5);
            assertValueList(expectedValueList, actualValueList);
        }

        [TestMethod]
        public void testAppendValue()
        {
            Value expected = new Value(100, new short[] { 5, 10 });
            tempValuesEntry.append(expected);
            Value[] actuals = tempValuesEntry.read(tempValuesEntry.getCount() - 1, 1);
            assertValue(expected, actuals[0]);
            unisens.deleteEntry(tempValuesEntry);
        }

        [TestMethod]
        public void testAppendMultipleValues()
        {
            Value expected1 = new Value(100, new short[] { 5, 10 });
            Value expected2 = new Value(200, new short[] { 15, 1 });
            Value expected3 = new Value(500, new short[] { 25, -25 });
            tempValuesEntry.append(expected1);
            tempValuesEntry.append(expected2);
            tempValuesEntry.append(expected3);
            Value[] actuals = tempValuesEntry.read(tempValuesEntry.getCount() - 3, 3);
            assertValue(expected1, actuals[0]);
            assertValue(expected2, actuals[1]);
            assertValue(expected3, actuals[2]);
            unisens.deleteEntry(tempValuesEntry);
        }

        [TestMethod]
        public void testAppendValueArray()
        {
            tempValuesEntry.append(expectedArray);
            Value[] actuals = tempValuesEntry.read(tempValuesEntry.getCount() - expectedArray.Length, expectedArray.Length);
            assertValueList(expectedArray, actuals);
            unisens.deleteEntry(tempValuesEntry);
        }

        [TestMethod]
        public void testAppendValuesList()
        {
            tempValuesEntry.appendValuesList(expectedValueList);
            ValueList actuals = tempValuesEntry.readValuesList(tempValuesEntry.getCount() - expectedValueList.getSamplestamps().Length, expectedValueList.getSamplestamps().Length);
            assertValueList(expectedValueList, actuals);
            unisens.deleteEntry(tempValuesEntry);
        }

        [TestMethod]
        public void testClone()
        {
            ValuesEntry clonedValuesEntry = (ValuesEntry)valuesEntry.clone<ValuesEntry>();
            Assert.AreEqual(valuesEntry.getAdcResolution(), clonedValuesEntry.getAdcResolution());
            Assert.AreEqual(valuesEntry.getAdcZero(), clonedValuesEntry.getAdcZero());
            Assert.AreEqual(valuesEntry.getBaseline(), clonedValuesEntry.getBaseline());
            Assert.AreEqual(valuesEntry.getComment(), clonedValuesEntry.getComment());
            Assert.AreEqual(valuesEntry.getContentClass(), clonedValuesEntry.getContentClass());
            Assert.AreEqual(valuesEntry.getDataType(), clonedValuesEntry.getDataType());
            Assert.AreEqual(valuesEntry.getFileFormat().getFileFormatName(), clonedValuesEntry.getFileFormat().getFileFormatName());
            Assert.AreEqual(valuesEntry.getFileFormat().getComment(), clonedValuesEntry.getFileFormat().getComment());
            Assert.AreEqual(valuesEntry.getId(), clonedValuesEntry.getId());
            Assert.AreEqual(valuesEntry.getLsbValue(), clonedValuesEntry.getLsbValue(), 0);
            Assert.AreEqual(valuesEntry.getSampleRate(), clonedValuesEntry.getSampleRate(), 0);
            Assert.AreEqual(valuesEntry.getSource(), clonedValuesEntry.getSource());
            Assert.AreEqual(valuesEntry.getUnisens(), clonedValuesEntry.getUnisens());
            Assert.AreEqual(valuesEntry.getUnit(), clonedValuesEntry.getUnit());
            Assert.AreEqual(valuesEntry.getChannelCount(), clonedValuesEntry.getChannelCount());
            assertArrayEquals(valuesEntry.getChannelNames(), clonedValuesEntry.getChannelNames());
        }
    }
}