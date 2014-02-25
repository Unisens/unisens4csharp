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
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;

namespace UnisensUnitTest
{
    [TestClass]

    public class SignalEntryImplTest : TestProperties
    {
        public static Unisens unisens;
        public static SignalEntry signalEntry;
        public static UnisensFactory factory;
        public static Unisens unisens3;
        public static Unisens unisensUint;
        public static SignalEntry signalEntry3;
        public static SignalEntry signalEntryUint8_2x12;
        public static SignalEntry signalEntryUint16_2x12;
        public static SignalEntry signalEntryUint32_2x12;
        public static SignalEntry signalEntryUint8_1x12;
        public static SignalEntry signalEntryUint16_1x12;
        public static SignalEntry signalEntryUint32_1x12;
        public static SignalEntry signalEntryUint8_1x1;
        public static SignalEntry signalEntryUint16_1x1;
        public static SignalEntry signalEntryUint32_1x1;

        private TestContext testContextInstance;
        private static string Test1Location;

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
            factory = UnisensFactoryBuilder.createFactory();

            Test1Location = CopyExampleToTest(EXAMPLE1);           
            unisens = factory.createUnisens(Test1Location);
            signalEntry = (SignalEntry)unisens.getEntry("ecg.bin");

            String Test3Location = CopyExampleToTest(EXAMPLE3);
            unisens3 = factory.createUnisens(Test3Location);
            signalEntry3 = (SignalEntry)unisens3.getEntry("test.bin");

            String TestUnitLocation = CopyExampleToTest(EXAMPLE_UNIT);
            unisensUint = factory.createUnisens(TestUnitLocation);
            signalEntryUint8_2x12 = (SignalEntry)unisensUint.getEntry("test_2x12_UINT8.bin");
            signalEntryUint16_2x12 = (SignalEntry)unisensUint.getEntry("test_2x12_UINT16.bin");
            signalEntryUint32_2x12 = (SignalEntry)unisensUint.getEntry("test_2x12_UINT32.bin");
            signalEntryUint8_1x12 = (SignalEntry)unisensUint.getEntry("test_1x12_UINT8.bin");
            signalEntryUint16_1x12 = (SignalEntry)unisensUint.getEntry("test_1x12_UINT16.bin");
            signalEntryUint32_1x12 = (SignalEntry)unisensUint.getEntry("test_1x12_UINT32.bin");
            signalEntryUint8_1x1 = (SignalEntry)unisensUint.getEntry("test_1x1_UINT8.bin");
            signalEntryUint16_1x1 = (SignalEntry)unisensUint.getEntry("test_1x1_UINT16.bin");
            signalEntryUint32_1x1 = (SignalEntry)unisensUint.getEntry("test_1x1_UINT32.bin");
        }

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
        }

        [TestMethod]
        public void testReadUint8_1x1()
        {
            signalEntryUint8_1x1.resetPos();
            Object data = signalEntryUint8_1x1.read(1);
            Assert.IsTrue(data is byte[][]);
            byte[][] data1 = (byte[][])data;
            byte[][] expected = new byte[][] { new byte[] { 255 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint16_1x1()
        {
            signalEntryUint16_1x1.resetPos();
            Object data = signalEntryUint16_1x1.read(1);
            Assert.IsTrue(data is UInt16[][]);
            UInt16[][] data1 = (UInt16[][])data;

            UInt16[][] expected = new UInt16[][] { new UInt16[] { 65535 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint32_1x1()
        {
            signalEntryUint32_1x1.resetPos();
            Object data = signalEntryUint32_1x1.read(1);
            Assert.IsTrue(data is UInt32[][]);
            UInt32[][] data1 = (UInt32[][])data;

            UInt32[][] expected = new UInt32[][] { new UInt32[] { 4294967295 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint8_1x12()
        {
            signalEntryUint8_1x12.resetPos();
            Object data = signalEntryUint8_1x12.read(12);
            Assert.IsTrue(data is byte[][]);
            byte[][] data1 = (byte[][])data;

            byte[][] expected = new byte[][] { new byte[] { 255 }, new byte[] { 0 }, new byte[] { 127 }, new byte[] { 255 }, new byte[] { 255 }, new byte[] { 255 }, new byte[] { 255 }, new byte[] { 255 }, new byte[] { 0 }, new byte[] { 0 }, new byte[] { 0 }, new byte[] { 123 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint16_1x12()
        {
            signalEntryUint16_1x12.resetPos();
            Object data = signalEntryUint16_1x12.read(12);
            Assert.IsTrue(data is UInt16[][]);
            UInt16[][] data1 = (UInt16[][])data;

            UInt16[][] expected = new UInt16[][] { new UInt16[] { 65535 }, new UInt16[] { 0 }, new UInt16[] { 127 }, new UInt16[] { 32767 }, new UInt16[] { 65535 }, new UInt16[] { 255 }, new UInt16[] { 65535 }, new UInt16[] { 65535 }, new UInt16[] { 0 }, new UInt16[] { 0 }, new UInt16[] { 0 }, new UInt16[] { 123 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint32_1x12()
        {
            signalEntryUint32_1x12.resetPos();
            Object data = signalEntryUint32_1x12.read(12);
            Assert.IsTrue(data is UInt32[][]);
            UInt32[][] data1 = (UInt32[][])data;

            UInt32[][] expected = new UInt32[][] { new UInt32[] { 4294967295 }, new UInt32[] { 0 }, new UInt32[] { 127 }, new UInt32[] { 32767 }, new UInt32[] { 2147483647 }, new UInt32[] { 255 }, new UInt32[] { 65535 }, new UInt32[] { 4294967295 }, new UInt32[] { 0 }, new UInt32[] { 0 }, new UInt32[] { 0 }, new UInt32[] { 123 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint8_2x12()
        {
            signalEntryUint8_2x12.resetPos();
            Object data = signalEntryUint8_2x12.read(12);
            Assert.IsTrue(data is byte[][]);
            byte[][] data1 = (byte[][])data;

            byte[][] expected = new byte[][] { new byte[] { 255, 0 }, new byte[] { 0, 0 }, new byte[] { 127, 0 }, new byte[] { 255, 0 }, new byte[] { 255, 123 }, new byte[] { 255, 127 }, new byte[] { 255, 255 }, new byte[] { 255, 255 }, new byte[] { 0, 255 }, new byte[] { 0, 255 }, new byte[] { 0, 255 }, new byte[] { 123, 255 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint16_2x12()
        {
            signalEntryUint16_2x12.resetPos();
            Object data = signalEntryUint16_2x12.read(12);
            Assert.IsTrue(data is UInt16[][]);
            UInt16[][] data1 = (UInt16[][])data;

            UInt16[][] expected = new UInt16[][] { new UInt16[] { 65535, 0 }, new UInt16[] { 0, 0 }, new UInt16[] { 127, 0 }, new UInt16[] { 32767, 0 }, new UInt16[] { 65535, 123 }, new UInt16[] { 255, 127 }, new UInt16[] { 65535, 255 }, new UInt16[] { 65535, 32767 }, new UInt16[] { 0, 65535 }, new UInt16[] { 0, 65535 }, new UInt16[] { 0, 65535 }, new UInt16[] { 123, 65535 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testReadUint32_2x12()
        {
            signalEntryUint32_2x12.resetPos();
            Object data = signalEntryUint32_2x12.read(12);
            Assert.IsTrue(data is UInt32[][]);
            UInt32[][] data1 = (UInt32[][])data;

            UInt32[][] expected = new UInt32[][] { new UInt32[] { 4294967295, 0 }, new UInt32[] { 0, 0 }, new UInt32[] { 127, 0 }, new UInt32[] { 32767, 0 }, new UInt32[] { 2147483647, 123 }, new UInt32[] { 255, 127 }, new UInt32[] { 65535, 255 }, new UInt32[] { 4294967295, 32767 }, new UInt32[] { 0, 65535 }, new UInt32[] { 0, 2147483647 }, new UInt32[] { 0, 4294967295 }, new UInt32[] { 123, 4294967295 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testRead()
        {
            signalEntry.resetPos();
            Object data = signalEntry.read(10);
            Assert.IsTrue(data is int[][]);
            int[][] data1 = (int[][])data;
            int[][] expected = new int[][] { new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { -5, 5 }, new int[] { 0, -5 }, new int[] { 5, 26 }, new int[] { 42, 80 } };
            for (int i = 0; i < data1.Length; i++)
                assertArrayEquals(expected[i], data1[i]);
        }

        [TestMethod]
        public void testResetPos()
        {
            signalEntry.resetPos();
            int[][] expected = new int[][] { new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { -5, 5 }, new int[] { 0, -5 }, new int[] { 5, 26 }, new int[] { 42, 80 } };
            int[][] data = (int[][])signalEntry.read(10);
            for (int i = 0; i < data.Length; i++)
                assertArrayEquals(expected[i], data[i]);
        }
        [TestMethod]
        public void testReadPos()
        {
            signalEntry.resetPos();
            signalEntry.read(10);
            int[][] expected = (int[][])signalEntry.read(10);
            signalEntry.resetPos();
            int[][] actuals = (int[][])signalEntry.read(10, 10);

            for (int i = 0; i < expected.Length; i++)
                assertArrayEquals(expected[i], actuals[i]);
        }

        [TestMethod]
        public void testReadPos2()
        {
            signalEntry.resetPos();
            int[][] expected = (int[][])signalEntry.read(100000);
            signalEntry.resetPos();

            for (int i = 0; i < expected.Length; i += 5000)
            {
                int[][] data = (int[][])signalEntry.read(i, 1);
                assertArrayEquals(expected[i], data[0]);
            }
        }

        [TestMethod]
        public void testReadPos3()
        {
            var count = (int) signalEntry3.getCount()/100;
            var e = signalEntry3.read(count);
            short[][] expected = (short[][])e;
            signalEntry3.resetPos();
            for (int i = 0; i < expected.Length; i++)
            {
                short[][] data = (short[][])signalEntry3.read(i * 100, 1);
                assertArrayEquals(expected[i], data[0]);
            }
        }

        [TestMethod]
        public void testAppend()
        {
            SignalEntry signalEntry = unisens.createSignalEntry("temp", new String[] { "c1", "c2" }, DataType.INT32, 400);
            int[][] expecteds = new int[][] { new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { -5, 5 }, new int[] { 0, -5 }, new int[] { 5, 26 }, new int[] { 42, 80 } };
            signalEntry.append(expecteds);
            int[][] actuals = (int[][])signalEntry.read(expecteds.Length);
            assertIntArraysEquals(expecteds, actuals);
            unisens.deleteEntry(signalEntry);
        }

        [TestMethod]
        public void testAppendCsvDimm1()
        {
            SignalEntry signalEntry = unisens.createSignalEntry("temp1.csv", new String[] { "c1" }, DataType.INT32, 400);
            signalEntry.setFileFormat(signalEntry.createCsvFileFormat());
            int[] data = { 0, 0, 0, 0, 0, 0, -5, 0, 5, 42 };
            int[][] expecteds = new int[][] { new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { 0 }, new int[] { -5 }, new int[] { 0 }, new int[] { 5 }, new int[] { 42 } };
            signalEntry.append(data);
            int[][] actuals = (int[][])signalEntry.read(expecteds.Length);
            assertIntArraysEquals(expecteds, actuals);
            unisens.deleteEntry(signalEntry);
        }

        [TestMethod]
        public void testCustomAttributes()
        {
            Entry entry = unisens.getEntry("ecg.bin");
            Dictionary<String, String> customEntryAttrs = entry.getCustomAttributes();
            Assert.IsTrue(customEntryAttrs != null);
            Assert.AreEqual(2, customEntryAttrs.Count());
            Assert.IsTrue(customEntryAttrs["customEntryKey1"].Equals("customEntryValue1", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(customEntryAttrs["customEntryKey2"].Equals("customEntryValue2", StringComparison.CurrentCultureIgnoreCase));
        }

        [TestMethod]
        public void testLsbValue()
        {
            double lsbValue = 1.345;
            SignalEntry signalEntry = unisens.createSignalEntry("lsbtest.csv", new String[] { "c1" }, DataType.INT32, 400);
            signalEntry.setLsbValue(lsbValue);
            unisens.save();

            SignalEntry testEntry = (SignalEntry)unisens.getEntry("lsbtest.csv");
            Assert.AreEqual(testEntry.getLsbValue(), signalEntry.getLsbValue());
            Assert.AreEqual(lsbValue, signalEntry.getLsbValue());
        }

        [TestMethod]
        public void testLsbValueAgain()
        {
            double lsbValue = 1.345;
            System.IO.Directory.CreateDirectory(Test1Location + "_x");
            Unisens unisensTest = factory.createUnisens(Test1Location + "_x");
            SignalEntry signalEntry = unisensTest.createSignalEntry("lsbtest.bin", new String[] { "c1" }, DataType.INT32, 400);
            signalEntry.append(new Int32[] { 1, 2, 3, 4 });
            signalEntry.setLsbValue(lsbValue);
            unisensTest.save();
            unisensTest.closeAll();

            SignalEntry testEntry = (SignalEntry)unisensTest.getEntry("lsbtest.bin");
            Assert.AreEqual(testEntry.getLsbValue(), signalEntry.getLsbValue());
            Assert.AreEqual(lsbValue, signalEntry.getLsbValue());
        }

        [TestMethod]
        public void testSampleRate()
        {
            double sampleRate = 10.24;
            SignalEntry signalEntry = unisens.createSignalEntry("sampleratetest.csv", new String[] { "c1" }, DataType.INT32, sampleRate);
            unisens.save();

            SignalEntry testEntry = (SignalEntry)unisens.getEntry("sampleratetest.csv");
            Assert.AreEqual(testEntry.getSampleRate(), signalEntry.getSampleRate());
            Assert.AreEqual(sampleRate, signalEntry.getSampleRate());
        }

        [TestMethod]
        public void testSampleRateAgain()
        {
            double sampleRate = 10.24;
            System.IO.Directory.CreateDirectory(Test1Location + "_x");
            Unisens unisensTest = factory.createUnisens(Test1Location + "_x");

            SignalEntry signalEntry = unisensTest.createSignalEntry("sampleratetest.bin", new String[] { "c1" }, DataType.INT32, sampleRate);
            signalEntry.append(new Int32[] { 1, 2, 3, 4 });
            unisensTest.save();
            unisensTest.closeAll();

            SignalEntry testEntry = (SignalEntry)unisensTest.getEntry("sampleratetest.bin");
            Assert.AreEqual(sampleRate, testEntry.getSampleRate());
            Assert.AreEqual(sampleRate, signalEntry.getSampleRate());
        }

        [TestMethod]
        public void testClone()
        {
            SignalEntry clonedSignalEntry = (SignalEntry)signalEntry.clone<SignalEntry>();
            Assert.AreEqual(signalEntry.getAdcResolution(), clonedSignalEntry.getAdcResolution());
            Assert.AreEqual(signalEntry.getAdcZero(), clonedSignalEntry.getAdcZero());
            Assert.AreEqual(signalEntry.getBaseline(), clonedSignalEntry.getBaseline());
            Assert.AreEqual(signalEntry.getComment(), clonedSignalEntry.getComment());
            Assert.AreEqual(signalEntry.getContentClass(), clonedSignalEntry.getContentClass());
            Assert.AreEqual(signalEntry.getDataType(), clonedSignalEntry.getDataType());
            Assert.AreEqual(signalEntry.getFileFormat().getFileFormatName(), clonedSignalEntry.getFileFormat().getFileFormatName());
            Assert.AreEqual(signalEntry.getFileFormat().getComment(), clonedSignalEntry.getFileFormat().getComment());
            Assert.AreEqual(signalEntry.getId(), clonedSignalEntry.getId());
            Assert.AreEqual(signalEntry.getLsbValue(), clonedSignalEntry.getLsbValue(), 0);
            Assert.AreEqual(signalEntry.getSampleRate(), clonedSignalEntry.getSampleRate(), 0);
            Assert.AreEqual(signalEntry.getSource(), clonedSignalEntry.getSource());
            Assert.AreEqual(signalEntry.getUnisens(), clonedSignalEntry.getUnisens());
            Assert.AreEqual(signalEntry.getUnit(), clonedSignalEntry.getUnit());
            Assert.AreEqual(signalEntry.getChannelCount(), clonedSignalEntry.getChannelCount());
            assertArrayEquals(signalEntry.getChannelNames(), clonedSignalEntry.getChannelNames());
        }
    }
}