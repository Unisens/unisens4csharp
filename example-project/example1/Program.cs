using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using org.unisens;
using Microsoft.VisualBasic;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {

        private string path;
        private string path1;

        static void Main(string[] args)
        {

            string path = @"C:\tmp1\" + System.DateTime.Now.ToString("yyyy-MM-dd_Hmms");
            string path1 = @"C:\Users\jstumpp\Documents\Messdaten\1. Test EKGMove";
            //Console.WriteLine(path);
            //Console.ReadKey();
            FileSystem.MkDir(path);
            FileSystem.ChDir(path1);
            new Program(path, path1);
        }


        // Testdaten (Matlab):
        // DATA = [2^32, 0, 2^7-1, 2^15-1, 2^31-1, 2^8-1, 2^16-1, 2^32-1, -2^7, -2^15, -2^31, 123.456]';

        /// <summary>
        /// Unisens library test
        /// </summary>
        /// <param name="p">Path for test unisens dataset</param>
        public Program(string p, string p1)
        {
            this.path = p;
            this.path1 = p1;

            string[] fileFormatList = new string[] { "csv", "xml", "bin_LITTLE", "bin_BIG" };
            DataType[] dataTypeList = new DataType[] { DataType.INT8, DataType.UINT8, DataType.INT16, DataType.UINT16, DataType.INT32, DataType.UINT32, DataType.FLOAT, DataType.DOUBLE };


            //Write data
            foreach (string f in fileFormatList)
            {
                foreach (DataType dataType in dataTypeList)
                {
                    signalTest(f, dataType);
                }
            }

            //Read data
            UnisensFactory uf1 = UnisensFactoryBuilder.createFactory();
            Unisens u1 = uf1.createUnisens(path1);
            Entry entry;
            entry = u1.getEntry("acc.bin");
            SignalEntry se1 = (SignalEntry)entry;
            short[][] A1 = (short[][])se1.read(0, 3);
            for (int i = 0; i < A1.Length; i++)
            {
                for (int j = 0; j < A1[i].Length; j++)
                    MessageBox.Show(A1[i].GetValue(j).ToString());
            }
        }

        private void signalTest(string fileFormat, DataType dataType)
        {

            string[] Channelnames = { "CH1", "CH2" };
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path);
            string fileName = "signal_" + dataType.ToString() + "_" + fileFormat + "." + fileFormat.Substring(0, 3);
            string fileName1 = "value_" + dataType.ToString() + "_" + fileFormat + "." + fileFormat.Substring(0, 3);
            ValuesEntry ve = u.createValuesEntry(fileName1, new String[] { "A", "B" }, dataType, 250);
            SignalEntry se = u.createSignalEntry(fileName, new String[] { "A", "B" }, dataType, 250);

            switch (fileFormat)
            {
                case "bin_LITTLE":
                    // BIN
                    BinFileFormat bffsili = se.createBinFileFormat();
                    bffsili.setEndianess(Endianess.LITTLE);
                    se.setFileFormat(bffsili);
                    BinFileFormat bffva = ve.createBinFileFormat();
                    bffva.setEndianess(Endianess.LITTLE);
                    ve.setFileFormat(bffva);
                    break;

                case "bin_BIG":
                    // BIN
                    BinFileFormat bffsibi = se.createBinFileFormat();
                    bffsibi.setEndianess(Endianess.BIG);
                    se.setFileFormat(bffsibi);
                    BinFileFormat bffvabi = ve.createBinFileFormat();
                    bffvabi.setEndianess(Endianess.BIG);
                    ve.setFileFormat(bffvabi);
                    break;

                case "xml":
                    // XML
                    XmlFileFormat xffsi = se.createXmlFileFormat();
                    se.setFileFormat(xffsi);
                    XmlFileFormat xffva = ve.createXmlFileFormat();
                    ve.setFileFormat(xffva);
                    break;

                case "csv":
                    // CSV
                    CsvFileFormat cffsi = se.createCsvFileFormat();
                    cffsi.setSeparator("\t");
                    cffsi.setDecimalSeparator(",");
                    se.setFileFormat(cffsi);
                    CsvFileFormat cffva = ve.createCsvFileFormat();
                    cffva.setComment("csv , 2 channel ");
                    cffva.setSeparator(";");
                    cffva.setDecimalSeparator(".");
                    ve.setFileFormat(cffva);
                    break;
            }

            var samplestamp = new long[3] { 1320, 22968, 30232 };
            switch (dataType)
            {

                case DataType.INT8:
                    var A = new sbyte[][] { new sbyte[] { -128, 127 }, new sbyte[] { 2, 5 }, new sbyte[] { 3, 6 } };
                    se.append(A);
                    ValueList valueList = new ValueList(samplestamp, A);
                    ve.appendValuesList(valueList);
                    break;
                case DataType.UINT8:
                    var B = new byte[][] { new byte[] { 255, 4 }, new byte[] { 2, 5 }, new byte[] { 3, 6 } };
                    se.append(B);
                    ValueList valueList1 = new ValueList(samplestamp, B);
                    ve.appendValuesList(valueList1);
                    break;
                case DataType.INT16:
                    var C = new short[][] { new short[] { -32768, 32767 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                    se.append(C);
                    ValueList valueList2 = new ValueList(samplestamp, C);
                    ve.appendValuesList(valueList2);
                    break;
                case DataType.UINT16:
                    var D = new UInt16[][] { new UInt16[] { 65535, 4 }, new UInt16[] { 2, 5 }, new UInt16[] { 3, 6 } };
                    se.append(D);
                    ValueList valueList3 = new ValueList(samplestamp, D);
                    ve.appendValuesList(valueList3);
                    break;
                case DataType.INT32:
                    var E = new int[][] { new int[] { -2147483648, 2147483647 }, new int[] { 2, 5 }, new int[] { 3, 6 } };
                    se.append(E);
                    ValueList valueList4 = new ValueList(samplestamp, E);
                    ve.appendValuesList(valueList4);
                    break;
                case DataType.UINT32:
                    var F = new UInt32[][] { new UInt32[] { 4294967295, 4 }, new UInt32[] { 2, 5 }, new UInt32[] { 3, 6 } };
                    se.append(F);   
                    ValueList valueList5 = new ValueList(samplestamp, F);
                    ve.appendValuesList(valueList5);
                    break;
                case DataType.FLOAT:
                    var G = new float[][] { new float[] { 123.4567F, 4 }, new float[] { 2, 5 }, new float[] { 3, 6 } };
                    se.append(G);
                    ValueList valueList6 = new ValueList(samplestamp, G);
                    ve.appendValuesList(valueList6);
                    break;
                case DataType.DOUBLE:
                    var H = new double[][] { new double[] { 123.4567D, 4 }, new double[] { 2, 5 }, new double[] { 3, 6 } };
                    se.append(H);
                    ValueList valueList7 = new ValueList(samplestamp, H);
                    ve.appendValuesList(valueList7);
                    break;
            }
            u.save();
            //Console.WriteLine("hallo");
            //Console.WriteLine(uf.ToString());
            //Console.ReadKey();
        }

    }
}