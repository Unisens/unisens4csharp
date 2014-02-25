using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using org.unisens;

namespace UnisensUnitTest
{
    public class TestProperties
    {
        // movisens:
        //public static String UNISENS_LOCATION = @"C:\Users\jstumpp\Documents\SVN\Unisens\unisens4Java";

        // ShineTech:
        // public static String UNISENS_LOCATION = @"E:\Java2Net"; 

        // FZI:
        public static String UNISENS_LOCATION = @"C:\kirst\Verlinkt\Unisens_SVN\unisens\trunk";

        // Giau Pham:
        //public static String UNISENS_LOCATION = @"C:\Projekte\unisens\trunk";

        public static String EXAMPLE1 = @"unisens-tests\data\example1";
        public static String EXAMPLE2 = @"unisens-tests\data\example2";
        public static String EXAMPLE3 = @"unisens-tests\data\test_read_pos";
        public static String EXAMPLE_UNIT = @"unisens-tests\data\test_uint";

        public static String EXAMPLE_TEMP_SIGNAL_ENTRY = @"unisens-tests\data\example temp\SignalEntry";
        public static String EXAMPLE_TEMP_EVENT_ENTRY = @"unisens-tests\data\example temp\EventEntry";
        public static String EXAMPLE_TEMP_SPAN_ENTRY = @"unisens-tests\data\example temp\SpanEntry";
        public static String EXAMPLE_TEMP_VALUES_ENTRY = @"unisens-tests\data\example temp\ValuesEntry";

        public static string CopyExampleToTest(String example)
        {
            String sourcePath = UNISENS_LOCATION + @"\" + example;
            String testPath   = example + "_" + Guid.NewGuid().ToString();

            Console.WriteLine("Path: " + testPath);

            if (System.IO.Directory.Exists(sourcePath))
            {
                Directory.CreateDirectory(testPath);
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    String fileName = System.IO.Path.GetFileName(s);
                    String destFile = System.IO.Path.Combine(testPath, System.IO.Path.GetFileName(s));
                    System.IO.File.Copy(s, destFile, true);
                }
                return testPath;
            }
            return null;
        }

        public static void DeleteExample(String example)
        {
            String sourcePath = UNISENS_LOCATION + @"\" + example;
            if (System.IO.Directory.Exists(sourcePath))
            {
                Directory.CreateDirectory(example);
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    String fileName = System.IO.Path.GetFileName(s);
                    String destFile = System.IO.Path.Combine(example, System.IO.Path.GetFileName(s));
                    System.IO.File.Copy(s, destFile, true);
                }
            }
        }

        public static void assertArrayEquals(sbyte[] o1, sbyte[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(byte[] o1, byte[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(short[] o1, short[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(UInt16[] o1, UInt16[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(int[] o1, int[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(UInt32[] o1, UInt32[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(float[] o1, float[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(long[] o1, long[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertArrayEquals(object[] o1, object[] o2)
        {
            for (int i = 0; i < o1.Length; i++)
            {
                Assert.AreEqual(o1[i], o2[i]);
            }
        }

        public static void assertIntArraysEquals(int[][] o1, int[][] o2)
        {
            for (int i = 0; i < o1.GetLength(0); i++)
            {
                assertArrayEquals(o1[i], o2[i]);
            }
        }


        public static void assertValueList(Value[] expected, Value[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                assertValue(expected[i], actual[i]);
            }
        }

        public static void assertValue(Value expected, Value actual)
        {
            Assert.AreEqual(expected.GetSamplestamp(), actual.GetSamplestamp());
            var eData = expected.GetData();
            var aData = actual.GetData();
            if (eData is byte[])
            {
                assertArrayEquals(expected.GetData() as byte[], aData as byte[]);
            }
            else if (eData is short[])
            {
                assertArrayEquals(eData as short[], aData as short[]);
            }
            else if (eData is long[])
            {
                assertArrayEquals(eData as long[], aData as long[]);
            }
            else if (eData is float[])
            {
                assertArrayEquals(eData as float[], aData as float[]);
            }
            else if (eData is int[])
            {
                assertArrayEquals(eData as int[], aData as int[]);
            }
  
        }

        public static void assertValueList(ValueList expected, ValueList actual)
        {
            assertArrayEquals(expected.getSamplestamps(), actual.getSamplestamps());
            var eData = expected.getData();
            var aData = actual.getData();
            if (eData is byte[])
            {
                if(aData is byte[][])
                {
                    var data = (aData as byte[][]);
                    var dim = new byte[data.Length];
                    for (var i = 0; i < data.Length; i ++ )
                    {
                        dim[i] = data[i][0];
                    }
                    assertArrayEquals(eData as byte[], dim);
                }
                else
                    assertArrayEquals(eData as byte[], aData as byte[]);
            }
            else if (eData is short[])
            {
                if (aData is short[][])
                {
                    var data = (aData as short[][]);
                    var dim = new short[data.Length];
                    for (var i = 0; i < data.Length; i++)
                    {
                        dim[i] = data[i][0];
                    }
                    assertArrayEquals(eData as short[], dim);
                }
                else
                    assertArrayEquals(eData as short[], aData as short[]);
            }
            else if (eData is long[])
            {
                if (aData is long[][])
                {
                    var data = (aData as long[][]);
                    var dim = new long[data.Length];
                    for (var i = 0; i < data.Length; i++)
                    {
                        dim[i] = data[i][0];
                    }
                    assertArrayEquals(eData as long[], dim);
                }
                else
                    assertArrayEquals(eData as long[], aData as long[]);
            }
            else if (eData is float[])
            {
                if (aData is float[][])
                {
                    var data = (aData as float[][]);
                    var dim = new float[data.Length];
                    for (var i = 0; i < data.Length; i++)
                    {
                        dim[i] = data[i][0];
                    }
                    assertArrayEquals(eData as float[], dim);
                }
                else
                    assertArrayEquals(eData as float[], aData as float[]);
            }
            else if (eData is int[])
            {
                if (aData is int[][])
                {
                    var data = (aData as int[][]);
                    var dim = new int[data.Length];
                    for (var i = 0; i < data.Length; i++)
                    {
                        dim[i] = data[i][0];
                    }
                    assertArrayEquals(eData as int[], dim);
                }
                else
                    assertArrayEquals(eData as int[], aData as int[]);
            }
        }

        public static void assertEventList(List<Event> expected, List<Event> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for(var i = 0; i < expected.Count; i ++)
            {
                Assert.AreEqual(expected[i].getComment(), actual[i].getComment());
                Assert.AreEqual(expected[i].getType(), actual[i].getType());
                Assert.AreEqual(expected[i].getSamplestamp(), actual[i].getSamplestamp());
            }
        }

        public static void assertDictionaryEquals(Dictionary<string,string> expected, Dictionary<string,string> actual )
        {
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var attr in expected)
            {
                Assert.IsTrue(actual.ContainsKey(attr.Key));
                Assert.AreEqual(attr.Value, actual[attr.Key]);
            }
        }

    }
}
