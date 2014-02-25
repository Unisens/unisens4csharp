using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;
using System.IO;

namespace UnisensUnitTest
{
    public class SpanEntryCreateReadAppendTest : TestProperties
    {
        public static UnisensFactory factory;
        public static Unisens unisens;
        public static double sampleRate = 1000;
        public static DateTime timestampStart;

        public SpanEntry spanEntry;
        public static List<Span> spans = new List<Span>();

        [ClassInitialize()]
        public static void setUpBeforeClass()  {
		File unisensPath = new File(EXAMPLE_TEMP_SPAN_ENTRY);
		if(unisensPath.exists()){
			for(File file : unisensPath.listFiles())
				if(file.isFile())
					Assert.IsTrue(file.delete());
		}
		else
			Assert.IsTrue(unisensPath.mkdirs());
		factory = UnisensFactoryBuilder.createFactory();
		unisens = factory.createUnisens(EXAMPLE_TEMP_SPAN_ENTRY);
		timestampStart = new DateTime();
		unisens.setTimestampStart(timestampStart);
		unisens.setMeasurementId("Temp spans");
		spans.Add(new Span(30, 220,  "A1", "liegen"));
		spans.Add(new Span(500, 600, "A2", "sitzen"));
		spans.Add(new Span(1343 , 1600, "A3", "stehen"));
		spans.Add(new Span(3200, 3500, "A4", "gehen"));
		spans.Add(new Span(5600, 5900, "A5", "treppe hoch"));
		spans.Add(new Span(5600, 6200, "A6", ""));
	}

        [ClassCleanup()]
        public static void tearDownAfterClass()
        {
            unisens.closeAll();
            unisens.save();
        }

        [TestMethod]
        public void testSpanEntry_BIN()
        {
            spanEntry = unisens.createSpanEntry("se.bin", sampleRate);
            spanEntry.setTypeLength(2);
            spanEntry.setCommentLength(11);
            spanEntry.setFileFormat(new BinFileFormatImpl());
            spanEntry.append(spans);
            assertSpanListEquals(spans, spanEntry.read(spans.Count));
        }

        [TestMethod]
        public void testSpanEntry_CSV()
        {
            spanEntry = unisens.createSpanEntry("se.csv", sampleRate);
            ((CsvFileFormat)spanEntry.getFileFormat()).setSeparator("\t");
            spanEntry.append(spans);
            assertSpanListEquals(spans, spanEntry.read(spans.Count));
        }

        [TestMethod]
        public void testSpanEntry_XML()
        {
            spanEntry = unisens.createSpanEntry("se.xml", sampleRate);
            spanEntry.setFileFormat(new XmlFileFormatImpl());
            spanEntry.append(spans);
            assertSpanListEquals(spans, spanEntry.read(spans.Count));
        }

        [TestMethod]
        public void testSaveUnisens() {
		unisens.closeAll();
		unisens.save();
		unisens = factory.createUnisens(EXAMPLE_TEMP_SPAN_ENTRY);
		Assert.AreEqual(timestampStart.ToString(), unisens.getTimestampStart().ToString());
		spanEntry = (SpanEntry)unisens.getEntry("se.bin");
		Assert.AreEqual("BIN", spanEntry.getFileFormat().getFileFormatName());
		Assert.AreEqual(sampleRate, spanEntry.getSampleRate(), 0);
		Assert.IsTrue(spanEntry.getFileFormat() is BinFileFormat);
		Assert.AreEqual(Endianess.LITTLE, ((BinFileFormat)spanEntry.getFileFormat()).getEndianess());
	}

        public void assertSpanListEquals(List<Span> expected, List<Span> actual)
        {
            Assert.IsTrue(expected.Count == actual.Count);
            for (int i = 0; i < expected.Count; i++)
                Assert.IsTrue(expected.get(i).equals(actual.get(i)));
        }
    }
}