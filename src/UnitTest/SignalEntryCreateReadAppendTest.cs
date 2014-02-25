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

    public class SignalEntryCreateReadAppendTest : TestProperties
    {
        public static UnisensFactory factory;
        public static Unisens unisens;
        public static double sampleRate = 1000;
        public static DateTime timestampStart;
        public SignalEntry signalEntry;
        public BinFileFormat signalEntryFileFormat;
        public static sbyte[][] int8 = new sbyte[][] { new sbyte[] { SByte.MinValue, SByte.MaxValue }, new sbyte[] { 127, 127 }, new sbyte[] { 0, 0 }, new sbyte[] { 1, 1 }, new sbyte[] { 2, 2 } };
        public static sbyte[][] int8_n_1 = new sbyte[][] { new sbyte[] { 1, 2, 3 } };
        public static sbyte[][] int8_1_1 = new sbyte[][] { new sbyte[] { 1 } };
        public static byte[][] uint8 = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 1 }, new byte[] { 2, 2 }, new byte[] { 3, 3 }, new byte[] { 4, 4 } };
        public readonly short[][] int16 = new short[][] { new short[] { short.MinValue, short.MaxValue }, new short[] { -1, -1 }, new short[] { 0, 0 }, new short[] { 1, 1 }, new short[] { 2, 2 } };
        public static UInt16[][] uint16 = new UInt16[][] { new UInt16[] { 0, 0 }, new UInt16[] { 1, 1 }, new UInt16[] { 2, 2 }, new UInt16[] { 3, 3 }, new UInt16[] { 4, 4 } };
        public static int[][] int32 = new int[][] { new int[] { int.MinValue, int.MaxValue }, new int[] { -1, -1 }, new int[] { 0, 0 }, new int[] { 1, 1 }, new int[] { 2, 2 } };
        public static UInt32[][] uint32 = new UInt32[][] { new UInt32[] { 0, 0 }, new UInt32[] { 1, 1 }, new UInt32[] { 2, 2 }, new UInt32[] { 3, 3 }, new UInt32[] { 4, 4 } };
        public static UInt32[][] uint32_1_1 = new UInt32[][] { new UInt32[] { 1 } };
        //public static float[][] float32 = new float[][] { new float[] { float.MinValue, float.MaxValue }, new float[] { -1, -1 }, new float[] { 0, 0 }, new float[] { 1, 1 }, new float[] { 2, 2 } };
        public static float[][] float32 = new float[][] { new float[] {1,1}, new float[] { -1, -1 }, new float[] { 0, 0 }, new float[] { 1, 1 }, new float[] { 2, 2 } };
        //public static double[][] double64 = new double[][] { new double[] { double.MinValue, double.MaxValue }, new double[] { -1, -1 }, new double[] { 0.5, 0.6 }, new double[] { 1, 1 }, new double[] { 2, 2 } };
        public static double[][] double64 = new double[][] { new double[] { 0.5, 0.6 }, new double[] { 1, 1 }, new double[] { 2, 2 } };

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
            Directory.CreateDirectory(EXAMPLE_TEMP_SIGNAL_ENTRY);
            factory = UnisensFactoryBuilder.createFactory();
            unisens = factory.createUnisens(EXAMPLE_TEMP_SIGNAL_ENTRY);
            unisens.setTimestampStart(new DateTime());
            timestampStart = unisens.getTimestampStart();
            unisens.setMeasurementId("Temp signals");
        }

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
            unisens.save();
        }

        [TestMethod]
        public void testSignalEntry_INT8_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int8_le.bin", new String[] { "a", "b" }, DataType.INT8, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(int8);

            assertArrayEqual(int8, (sbyte[][])signalEntry.read(int8.Length));
        }

        [TestMethod]
        public void testSignalEntry_INT8_LE_N_CHANNELS_1_SAMPLE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int8_le_n_1.bin", new String[] { "a", "b", "c" }, DataType.INT8, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(int8_n_1);

            assertArrayEqual(int8_n_1, (sbyte[][])signalEntry.read(int8.Length));
        }

        [TestMethod]
        public void testSignalEntry_INT8_LE_1_CHANNEL_1_SAMPLE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int8_le_1_1.bin", new String[] { "a" }, DataType.INT8, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(int8_1_1);

            assertArrayEqual(int8_1_1, (sbyte[][])signalEntry.read(int8.Length));
        }

        [TestMethod]
        public void testSignalEntry_INT8_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int8_be.bin", new String[] { "a", "b" }, DataType.INT8, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(int8);

            assertArrayEqual(int8, (sbyte[][])signalEntry.read(int8.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT8_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint8_le.bin", new String[] { "a", "b" }, DataType.UINT8, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(uint8);
            assertArrayEqual(uint8, (byte[][])signalEntry.read(uint8.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT8_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint8_be.bin", new String[] { "a", "b" }, DataType.UINT8, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(uint8);
            assertArrayEqual(uint8, (byte[][])signalEntry.read(uint8.Length));
        }


        [TestMethod]
        public void testSignalEntry_INT16_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int16_le.bin", new String[] { "a", "b" }, DataType.INT16, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(int16);
            assertArrayEqual(int16, (short[][])signalEntry.read(int16.Length));
        }

        [TestMethod]
        public void testSignalEntry_INT16_BE()
        {
            short[][] tmp = int16;
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int16_be.bin", new String[] { "a", "b" }, DataType.INT16, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.setFileFormat(signalEntryFileFormat);
            signalEntry.append(tmp);
            assertArrayEqual(int16, (short[][])signalEntry.read(int16.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT16_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint16_le.bin", new String[] { "a", "b" }, DataType.UINT16, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(uint16);
            assertArrayEqual(uint16, (UInt16[][])signalEntry.read(uint16.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT16_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint16_be.bin", new String[] { "a", "b" }, DataType.UINT16, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(uint16);
            assertArrayEqual(uint16, (UInt16[][])signalEntry.read(uint16.Length));
        }

        [TestMethod]
        public void testSignalEntry_INT32_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int32_le.bin", new String[] { "a", "b" }, DataType.INT32, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(int32);
            assertArrayEqual(int32, (int[][])signalEntry.read(int32.Length));
        }

        [TestMethod]
        public void testSignalEntry_INT32_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int32_be.bin", new String[] { "a", "b" }, DataType.INT32, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(int32);
            assertArrayEqual(int32, (int[][])signalEntry.read(int32.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT32_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint32_le.bin", new String[] { "a", "b" }, DataType.UINT32, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(uint32);
            assertArrayEqual(uint32, (UInt32[][])signalEntry.read(uint32.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT32_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint32_be.bin", new String[] { "a", "b" }, DataType.UINT32, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(uint32);
            assertArrayEqual(uint32, (UInt32[][])signalEntry.read(uint32.Length));
        }

        [TestMethod]
        public void testSignalEntry_UINT32_1x1_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint32_1x1_le.bin", new String[] { "a" }, DataType.UINT32, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(uint32_1_1);
            assertArrayEqual(uint32_1_1, (UInt32[][])signalEntry.read(uint32_1_1.Length));
        }

        [TestMethod]
        public void testSignalEntry_FLOAT_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_float_le.bin", new String[] { "a", "b" }, DataType.FLOAT, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(float32);
            assertArrayEqual(float32, (float[][])signalEntry.read(float32.Length));
        }

        [TestMethod]
        public void testSignalEntry_FLOAT_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_float_be.bin", new String[] { "a", "b" }, DataType.FLOAT, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(float32);
            assertArrayEqual(float32, (float[][])signalEntry.read(float32.Length));
        }

        [TestMethod]
        public void testSignalEntry_DOUBLE_LE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_double_le.bin", new String[] { "a", "b" }, DataType.DOUBLE, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.LITTLE);
            signalEntry.append(double64);
            assertArrayEqual(double64, (double[][])signalEntry.read(double64.Length));
        }

        [TestMethod]
        public void testSignalEntry_DOUBLE_BE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_double_be.bin", new String[] { "a", "b" }, DataType.DOUBLE, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(double64);
            assertArrayEqual(double64, (double[][])signalEntry.read(double64.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_TAB()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_tab.csv", new String[] { "a", "b" }, DataType.INT16, sampleRate);
            CsvFileFormat csvFileFormat = new CsvFileFormatImpl();
            csvFileFormat.setSeparator("\t");
            signalEntry.setFileFormat(csvFileFormat);
            signalEntry.append(int16);
            assertArrayEqual(int16, (short[][])signalEntry.read(int16.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_SHORT()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_short.csv", new String[] { "a", "b" }, DataType.INT16, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(int16);
            assertArrayEqual(int16, (short[][])signalEntry.read(int16.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_INT()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int.csv", new String[] { "a", "b" }, DataType.INT32, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(int32);
            assertArrayEqual(int32, (int[][])signalEntry.read(int32.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_INT8()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int8.csv", new String[] { "a", "b" }, DataType.INT8, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(int8);
            this.assertArrayEqual( int8, (sbyte[][])signalEntry.read(int8.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_UINT8()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint8.csv", new String[] { "a", "b" }, DataType.UINT8, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(uint8);
            this.assertArrayEqual(uint8, (byte[][])signalEntry.read(uint8.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_UINT16()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint16.csv", new String[] { "a", "b" }, DataType.UINT16, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(uint16);
            this.assertArrayEqual(uint16, (UInt16[][])signalEntry.read(uint16.Length));
        }        

        [TestMethod]
        public void testSignalEntryCsv_UINT32()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint32.csv", new String[] { "a", "b" }, DataType.UINT32, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(uint32);
            this.assertArrayEqual(uint32, (UInt32[][])signalEntry.read(uint32.Length));
        }

        [TestMethod]
        public void testSignalEntryCsv_DOUBLE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_double.csv", new String[] { "a", "b" }, DataType.DOUBLE, sampleRate);
            signalEntry.setFileFormat(new CsvFileFormatImpl());
            signalEntry.append(double64);
            this.assertArrayEqual(double64, (double[][])signalEntry.read(double64.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_SHORT()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_short.xml", new String[] { "a", "b" }, DataType.INT16, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(int16);
            assertArrayEqual(int16, (short[][])signalEntry.read(int16.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_INT8()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int8.xml", new String[] { "a", "b" }, DataType.INT8, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(int8);
            assertArrayEqual(int8, (sbyte[][])signalEntry.read(int8.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_UINT8()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint8.xml", new String[] { "a", "b" }, DataType.UINT8, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(uint8);
            assertArrayEqual(uint8, (byte[][])signalEntry.read(uint8.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_UINT16()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_ushort.xml", new String[] { "a", "b" }, DataType.UINT16, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(uint16);
            assertArrayEqual(uint16, (UInt16[][])signalEntry.read(uint16.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_INT()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_int.xml", new String[] { "a", "b" }, DataType.INT32, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(int32);
            assertArrayEqual(int32, (int[][])signalEntry.read(int32.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_UINT32()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_uint32.xml", new String[] { "a", "b" }, DataType.UINT32, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(uint32);
            assertArrayEqual(uint32, (UInt32[][])signalEntry.read(uint32.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_FLOAT()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_float.xml", new String[] { "a", "b" }, DataType.FLOAT, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(float32);
            this.assertArrayEqual(float32, (float[][])signalEntry.read(float32.Length));
        }

        [TestMethod]
        public void testSignalEntryXml_DOUBLE()
        {
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_double.xml", new String[] { "a", "b" }, DataType.DOUBLE, sampleRate);
            signalEntry.setFileFormat(new XmlFileFormatImpl());
            signalEntry.append(double64);
            this.assertArrayEqual(double64, (double[][])signalEntry.read(double64.Length));
        }

        [TestMethod]
        public void testZSaveUnisens()
        {
            unisens.closeAll();

            unisens = factory.createUnisens(EXAMPLE_TEMP_SIGNAL_ENTRY);
            Assert.AreEqual(timestampStart.ToString(), unisens.getTimestampStart().ToString());
            signalEntry = (SignalEntry)unisens.createSignalEntry("se_double_be.bin", new String[] { "a", "b" }, DataType.DOUBLE, sampleRate);
            signalEntryFileFormat = (BinFileFormat)signalEntry.getFileFormat();
            signalEntryFileFormat.setEndianess(Endianess.BIG);
            signalEntry.append(double64);
            unisens.save();

            signalEntry = (SignalEntry)unisens.getEntry("se_double_be.bin");
            Assert.AreEqual("BIN", signalEntry.getFileFormat().getFileFormatName());
            Assert.AreEqual(DataType.DOUBLE, signalEntry.getDataType());
            Assert.AreEqual(sampleRate, signalEntry.getSampleRate(), 0);
            Assert.IsTrue(signalEntry.getFileFormat() is BinFileFormat);
            Assert.AreEqual(Endianess.BIG, ((BinFileFormat)signalEntry.getFileFormat()).getEndianess());

            unisens.deleteEntry(signalEntry);
            unisens.save();
        }


        private void assertArrayEqual(byte[][] expected, byte[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(sbyte[][] expected, sbyte[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(short[][] expected, short[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(UInt16[][] expected, UInt16[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(int[][] expected, int[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(UInt32[][] expected, UInt32[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(long[][] expected, long[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                assertArrayEquals(expected[i], actual[i]);
            }
        }

        private void assertArrayEqual(float[][] expected, float[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0].Length, actual[0].Length);
            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected[0].Length; j++)
                {
                    Assert.AreEqual(expected[i][j], actual[i][j], 0.001);
                }
            }
        }

        private void assertArrayEqual(double[][] expected, double[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0].Length, actual[0].Length);
            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected[0].Length; j++)
                {
                    Assert.AreEqual(expected[i][j], actual[i][j], 0.001);
                }
            }
        }
    }
}