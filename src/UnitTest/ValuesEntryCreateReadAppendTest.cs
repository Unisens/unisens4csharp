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
using System.IO;
using org.unisens.ri;

namespace UnisensUnitTest
{
    [TestClass]

    public class ValuesEntryCreateReadAppendTest : TestProperties
    {
        public static UnisensFactory factory;
        public static Unisens unisens;
        public ValuesEntry valuesEntry;
        public CsvFileFormat valuesEntryFileFormat;
        public BinFileFormat valuesEntryBinFileFormat;
        public Value[] int16 = { new Value(100, new short[] { 1, 1 }), new Value(200, new short[] { 2, 2 }), new Value(300, new short[] { 3, 3 }), new Value(400, new short[] { 4, 4 }), new Value(500, new short[] { 5, 5 }) };
        public Value[] int32 = { new Value(100, new int[] { -1, -1 }), new Value(200, new int[] { 2, 2 }), new Value(300, new int[] { 3, 3 }), new Value(400, new int[] { 4, 4 }), new Value(500, new int[] { 5, 5 }) };
        public Value[] uint32 = { new Value(100, new UInt32[] { 1, 1 }), new Value(200, new UInt32[] { 2, 2 }), new Value(300, new UInt32[] { 3, 3 }), new Value(400, new UInt32[] { 4, 4 }), new Value(500, new UInt32[] { 5, 5 }) };
        public ValueList int32_1_dim = new ValueList(new long[] { 100, 200, 300, 400, 500 }, new int[] { 1, 2, 3, 4, 5 });
        public Value[] double64 = { new Value(100, new double[] { 1.1, 1.1 }), new Value(200, new double[] { 2.2, 2.2 }), new Value(300, new double[] { 3.3, 3.3 }), new Value(400, new double[] { 4.4, 4.4 }), new Value(500, new double[] { 5.5, 5.5 }) };

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
            Directory.CreateDirectory(EXAMPLE_TEMP_VALUES_ENTRY);
            factory = UnisensFactoryBuilder.createFactory();
            unisens = factory.createUnisens(EXAMPLE_TEMP_VALUES_ENTRY);
            unisens.setTimestampStart(new DateTime());
            unisens.setMeasurementId("Temp values");
        }

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
            unisens.save();
        }

        [TestMethod]
        public void testValuesEntry_INT16()
        {
            valuesEntry = unisens.createValuesEntry("ve_int16.csv", new String[] { "a", "b" }, DataType.INT16, 400);
            valuesEntry.setName("Values int 16");
            valuesEntry.append(int16);
            assertValueList(int16, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_INT32()
        {
            valuesEntry = unisens.createValuesEntry("ve_int32.csv", new String[] { "a", "b" }, DataType.INT32, 400);
            valuesEntry.append(int32);
            assertValueList(int32, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_INT32_1_DIM()
        {
            valuesEntry = unisens.createValuesEntry("ve_int32_1_dim.csv", new String[] { "a" }, DataType.INT32, 400);
            valuesEntry.appendValuesList(int32_1_dim);
            assertValueList(int32_1_dim,valuesEntry.readValuesList(5));
        }

        [TestMethod]
        public void testValuesEntry_DOUBLE()
        {
            valuesEntry = unisens.createValuesEntry("ve_double.csv", new String[] { "a", "b" }, DataType.DOUBLE, 400);
            valuesEntry.append(double64);
            assertValueList(double64, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_XML_INT16()
        {
            valuesEntry = unisens.createValuesEntry("ve_int16.xml", new String[] { "a", "b" }, DataType.INT16, 400);
            valuesEntry.setFileFormat(new XmlFileFormatImpl());
            valuesEntry.append(int16);
            assertValueList(int16, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_XML_INT32()
        {
            valuesEntry = unisens.createValuesEntry("ve_int32.xml", new String[] { "a", "b" }, DataType.INT32, 400);
            valuesEntry.setFileFormat(new XmlFileFormatImpl());
            valuesEntry.append(int32);
            assertValueList(int32, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_XML_DOUBLE()
        {
            valuesEntry = unisens.createValuesEntry("ve_double.xml", new String[] { "a", "b" }, DataType.DOUBLE, 400);
            valuesEntry.setFileFormat(new XmlFileFormatImpl());
            valuesEntry.append(double64);
            assertValueList(double64, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_BIN_INT32()
        {
            valuesEntry = unisens.createValuesEntry("ve_int32.bin", new String[] { "a", "b" }, DataType.INT32, 400);
            valuesEntryBinFileFormat = new BinFileFormatImpl();
            valuesEntryBinFileFormat.setEndianess(Endianess.LITTLE);
            valuesEntry.setFileFormat(valuesEntryBinFileFormat);
            valuesEntry.append(int32);
            assertValueList(int32, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_BIN_INT32_BE()
        {
            valuesEntry = unisens.createValuesEntry("ve_int32_be.bin", new String[] { "a", "b" }, DataType.INT32, 400);
            valuesEntryBinFileFormat = new BinFileFormatImpl();
            valuesEntryBinFileFormat.setEndianess(Endianess.BIG);
            valuesEntry.setFileFormat(valuesEntryBinFileFormat);
            valuesEntry.append(int32);
            assertValueList(int32, valuesEntry.read(5));
        }

        [TestMethod]
        public void testValuesEntry_BIN_UINT32()
        {
            valuesEntry = unisens.createValuesEntry("ve_uint32.bin", new String[] { "a", "b" }, DataType.UINT32, 400);
            valuesEntryBinFileFormat = new BinFileFormatImpl();
            valuesEntryBinFileFormat.setEndianess(Endianess.LITTLE);
            valuesEntry.setFileFormat(valuesEntryBinFileFormat);
            valuesEntry.append(uint32);
            assertValueList(uint32, valuesEntry.read(5));
        }
    }
}