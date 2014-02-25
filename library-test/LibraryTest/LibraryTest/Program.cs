using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using org.unisens;
using Microsoft.VisualBasic;
using System.IO;


namespace LibraryTest
{
    class Program
    {
        private string path;
        private string path1;

        static void Main(string[] args)
        {

            string path = @"C:\tmp\" + System.DateTime.Now.ToString("yyyy-MM-dd_Hmms");
            //string path = @"C:\Projekte\unisens4dotnet\trunk\library-test\TestData\test_01";

            //Console.WriteLine(path);
            //Console.ReadKey();
            FileSystem.MkDir(path);
            FileSystem.ChDir(path);
            new Program(path,path);
        }

	    // Testdaten (Matlab):
        // DATA = [2^32, 0, 2^7-1, 2^15-1, 2^31-1, 2^8-1, 2^16-1, 2^32-1, -2^7, -2^15, -2^31, 123.456]';

        /// <summary>
        /// Unisens library test
        /// </summary>
        /// <param name="p">Path for test unisens dataset</param>
	    public Program(string p,string p1)
	    {
            this.path = p;
            this.path1 = p1;

//            string[] fileFormatList = new string[] { "csv", "bin", "xml" };
            DataType[] dataTypeList = new DataType[] { DataType.INT8, DataType.UINT8, DataType.INT16, DataType.UINT16, DataType.INT32, DataType.UINT32, DataType.FLOAT, DataType.DOUBLE };
//            string[] fileFormatList = new string[] { "csv" };
            //DataType[] dataTypeList = new DataType[] { DataType.FLOAT, DataType.DOUBLE };
            string[] fileFormatList = new string[] { "csv", "bin" };
            //DataType[] dataTypeList = new DataType[] { DataType.INT8 };


            foreach (string f in fileFormatList)
            {
                foreach (DataType dataType in dataTypeList)
                {
                    signalTest(f, dataType);
                }
            }

            //signalBin();
            //signalCsv();
            //signalXml();

            //valuesBin();
            //valuesCsv();
            //valuesXml();

            //eventBin();
            //eventCsv();
            //eventXml();
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path1);
            List<Entry> listen = u.getEntries();
            foreach (Entry en in listen)
            {
                String name = en.getId();
                ReadFile(name);
            }    
	    }

        // WRITE
        private void signalTest(string fileFormat, DataType dataType)
        {
            
            string[] Channelnames = { "CH1", "CH2" };
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path);
            string fileName = "signal_" + dataType.ToString() + "." + fileFormat;

            SignalEntry se = u.createSignalEntry(fileName, new String[] { "A", "B" }, dataType, 250);

            switch (fileFormat)
            {
                case "bin":
                    // BIN
                    BinFileFormat bff = se.createBinFileFormat();
                    bff.setEndianess(Endianess.LITTLE);
                    se.setFileFormat(bff);
                    break;
                    
                case "xml":
                    // XML
                    XmlFileFormat xff = se.createXmlFileFormat();
                    se.setFileFormat(xff);
                    break;

                case "csv":
                    // CSV
                    CsvFileFormat cff = se.createCsvFileFormat();
                    cff.setSeparator("\t");
                    cff.setDecimalSeparator(".");
                    se.setFileFormat(cff);
                    break;
            } 

            switch (dataType)
            {                 
                case DataType.INT8:
                    var A = new sbyte[][] { new sbyte[] { 127, -128 }, new sbyte[] { 2, 5 }, new sbyte[] { 3, 6 } };
                    //var A = new byte[][] { new byte[] { 127, 255 }, new byte[] { 1, 0 }, new byte[] { 3, 6 } };
                    se.append(A);
                    break;
                case DataType.UINT8:
                    //var B = new short[][] { new short[] { 255, 0 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                    var B = new byte[][] { new byte[] { 127, 255 }, new byte[] { 1, 0 }, new byte[] { 3, 6 } };
                    se.append(B);
                    break;
                case DataType.INT16:
                    var C = new short[][] { new short[] { 32767, -32768 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                    se.append(C);
                    break;
                case DataType.UINT16:
                    var D = new UInt16[][] { new UInt16[] { 65535, 0 }, new UInt16[] { 2, 5 }, new UInt16[] { 3, 6 } };
                    se.append(D);
                    break;
                case DataType.INT32:
                    var E = new int[][] { new int[] { 2147483647, -2147483648 }, new int[] { 2, 5 }, new int[] { 3, 6 } };
                    se.append(E);
                    break;
                case DataType.UINT32:
                    var F = new UInt32[][] { new UInt32[] { 4294967295, 0 }, new UInt32[] { 2, 5 }, new UInt32[] { 3, 6 } };
                    se.append(F);
                    break;
                case DataType.FLOAT:
                    var G = new float[][] { new float[] { 123.4567F, 4 }, new float[] { 2, 5 }, new float[] { 3, 6 } };
                    se.append(G);
                    break;
                case DataType.DOUBLE:
                    var H = new double[][] { new double[] { 123.4567D, 4 }, new double[] { 2, 5 }, new double[] { 3, 6 } };
                    se.append(H);
                    break;
            }
            
            // Attributes
            se.setAdcResolution(16);
            se.setBaseline(0);
            se.setAdcZero(0);
            se.setComment("FhG - Elektroden ");
            se.setContentClass("ECG");
            se.setSourceId("ecg_m500_250 .bin ");
            se.setSource("FhG-Elektroden , ADS8345 ");
            se.setUnit("uV");
            se.setLsbValue(1.8273);
            se.setSampleRate(250);
            se.setChannelNames(Channelnames);

            // Save and close
            u.save();
            u.closeAll();
        }

        private void valueTest(string fileFormat, DataType dataType)
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path);
            string fileName = "value_" + dataType.ToString() + "." + fileFormat;
            ValuesEntry ve = u.createValuesEntry(fileName, new String[] { "A", "B" }, dataType, 250);

            switch (fileFormat)
            {
                case "bin":
                    // BIN
                    BinFileFormat bff = ve.createBinFileFormat();
                    bff.setEndianess(Endianess.LITTLE);
                    ve.setFileFormat(bff);
                    break;

                case "xml":
                    // XML
                    XmlFileFormat xff = ve.createXmlFileFormat();
                    ve.setFileFormat(xff);
                    break;

                case "csv":
                    // CSV
                    CsvFileFormat cff = ve.createCsvFileFormat();
                    cff.setComment("csv , 2 channel ");
                    cff.setSeparator(";");
                    cff.setDecimalSeparator(".");
                    ve.setFileFormat(cff);
                    break;
            }

            var samplestamp = new long[3] { 1320, 22968, 30232 };

            switch (dataType)
            {

                case DataType.INT8:
                    var A = new sbyte[][] { new sbyte[] { 127, 4 }, new sbyte[] { 2, 5 }, new sbyte[] { 3, 6 } };
                    ValueList valueList = new ValueList(samplestamp, A);
                    ve.appendValuesList(valueList);
                    break;
                case DataType.UINT8:
                    var B = new short[][] { new short[] { 255, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                    ValueList valueList1 = new ValueList(samplestamp, B);
                    ve.appendValuesList(valueList1);
                    break;
                case DataType.INT16:
                    var C = new short[][] { new short[] { 32767, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                    ValueList valueList2 = new ValueList(samplestamp, C);
                    ve.appendValuesList(valueList2);
                    break;
                case DataType.UINT16:
                    var D = new int[][] { new int[] { 65535, 4 }, new int[] { 2, 5 }, new int[] { 3, 6 } };
                    ValueList valueList3 = new ValueList(samplestamp, D);
                    ve.appendValuesList(valueList3);
                    break;
                case DataType.INT32:
                    var E = new int[][] { new int[] { 2147483647, 4 }, new int[] { 2, 5 }, new int[] { 3, 6 } };
                    ValueList valueList4 = new ValueList(samplestamp, E);
                    ve.appendValuesList(valueList4);
                    break;
                //case DataType.UINT32:
                //    var F = new long[][] { new long[] { 4294967295, 4 }, new long[] { 2, 5 }, new long[] { 3, 6 } };
                //    ValueList valueList5 = new ValueList(samplestamp, F);
                //    ve.appendValuesList(valueList5);
                //    break;
                case DataType.FLOAT:
                    var G = new float[][] { new float[] { 123.4567F, 4 }, new float[] { 2, 5 }, new float[] { 3, 6 } };
                    ValueList valueList6 = new ValueList(samplestamp, G);
                    ve.appendValuesList(valueList6);
                    break;
                case DataType.DOUBLE:
                    var H = new double[][] { new double[] { 123.4567D, 4 }, new double[] { 2, 5 }, new double[] { 3, 6 } };
                    ValueList valueList7 = new ValueList(samplestamp, H);
                    ve.appendValuesList(valueList7);
                    break;
            }
        }

        //READ
        public static void ReadFile(String name)
        {
            //byte[] buffer;
            FileStream fileStream = new FileStream(name, FileMode.Open);
            long length = fileStream.Length;
            byte[] b = new byte[length];
            UTF8Encoding temp = new UTF8Encoding(true);
            Console.WriteLine("Die Daten von " + name);
            try
            {
                while (fileStream.Read(b, 0, b.Length) > 0)
                {                   
                    Console.WriteLine(temp.GetString(b));
                    Console.WriteLine("\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

            
        //private void signalBin()
        //{
        //    // WRITE
        //    string[] Channelnames = { "CH1", "CH2" };
        //    UnisensFactory uf = UnisensFactoryBuilder.createFactory();
        //    Unisens u = uf.createUnisens(path);
        //    SignalEntry se = u.createSignalEntry("signal.bin", new String[]{"A", "B"}, DataType.INT8, 250);
        //    //short[][] A = new short[][] { { 1, 2, 3 }, { 4, 5, 6 } };
        //    BinFileFormat bff = se.createBinFileFormat();

        //    var A = new sbyte[][] { new sbyte[] { 1, 4 }, new sbyte[] { 2, 5 }, new sbyte[] { 3, 6 } };
        //    se.append(A);
        //    //se.setAdcProperties(2048, 12, 0, 0.03);
        //    //se.setComment("This is the first test...");
        //    //se.setContentClass("TEST");
        //    //se.setSource("TestSource");
        //    //se.setSourceId("23:34:12:34:23:12");
        //    //se.setUnit("mV");
        //    se.setAdcResolution(16);
        //    se.setBaseline(0);
        //    se.setAdcZero(0);
        //    se.setComment("FhG - Elektroden ");
        //    se.setContentClass("ECG");
        //    se.setDataType(DataType.INT16);
        //    se.setSourceId("ecg_m500_250 .bin ");
        //    se.setSource("FhG-Elektroden , ADS8345 ");
        //    se.setUnit("uV");
        //    se.setLsbValue(1);
        //    se.setSampleRate(250);
        //    se.setChannelNames(Channelnames);
        //    bff.setEndianess(Endianess.LITTLE);
            
        //    u.save();
        //    u.closeAll();

            
        //    // READ
        //    u = uf.createUnisens(path);
        //    se = (SignalEntry)u.getEntry("signal.bin");

        //    if (se.getUnit() != "mV")
        //    {
        //        // ERROR
        //    }

        //    u.closeAll();
        //}
	
        //private void signalXml()
        //{
        //    string[] Channelnames = { "CH1", "CH2" };
        //    UnisensFactory uf = UnisensFactoryBuilder.createFactory();
        //    Unisens u = uf.createUnisens(path);
        //    SignalEntry se = u.createSignalEntry("signal.xml", new String[] { "A", "B" }, DataType.INT16, 250);
        //    u.setMeasurementId("sampleData");
        //    u.setTimestampStart(DateTime.Now);
        //    u.setComment("Example data set , derived from Holtervergleich_0001 .");
            
        //    var A = new short[][] { new short[] { 1, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
        //    se.setFileFormat(se.createXmlFileFormat());
        //    se.append(A);
        //    se.setAdcResolution(16);
        //    se.setBaseline(0);
        //    se.setAdcZero(0);
        //    se.setComment("FhG - Elektroden ");
        //    se.setContentClass("ECG");
        //    se.setDataType(DataType.INT16);
        //    se.setSourceId("ecg_m500_250 .xml ");
        //    se.setSource("FhG-Elektroden , ADS8345 ");
        //    se.setUnit("uV");
        //    se.setLsbValue(1);
        //    se.setSampleRate(250);
        //    se.setChannelNames(Channelnames);
        //    u.save();
        //    u.closeAll();

        //    //se = (SignalEntry)u.getEntry("test_signal_1x12_UINT8.xml");
        //    //se.read(12);
            
        //}
	
        //private void signalCsv()
        //{
        //    string[] Channelnames = { "CH1", "CH2" };
        //    UnisensFactory uf = UnisensFactoryBuilder.createFactory();
        //    Unisens u = uf.createUnisens(path);
        //    SignalEntry se = u.createSignalEntry("signal.csv", new String[] { "A", "B" }, DataType.INT16, 250);
        //    u.setMeasurementId("sampleData");
        //    u.setTimestampStart(DateTime.Now);
        //    u.setComment("Example data set , derived from Holtervergleich_0001 .");

        //    var A = new short[][] { new short[] { 1, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
        //    CsvFileFormat cff = se.createCsvFileFormat();
        //    cff.setSeparator(";");
        //    cff.setDecimalSeparator(".");
        //    se.setFileFormat(cff);
        //    se.append(A);
        //    se.setAdcResolution(16);
        //    se.setBaseline(0);
        //    se.setAdcZero(0);
        //    se.setComment("FhG - Elektroden ");
        //    se.setContentClass("ECG");
        //    se.setDataType(DataType.INT16);
        //    se.setSourceId("ecg_m500_250 .csv ");
        //    se.setSource("FhG-Elektroden , ADS8345 ");
        //    se.setUnit("uV");
        //    se.setLsbValue(1);
        //    se.setSampleRate(250);
        //    se.createBinFileFormat();
        //    se.setChannelNames(Channelnames);

        //    u.save();
        //    u.closeAll();
        //}
	
        //private void valuesCsv()
        //{
        //    UnisensFactory uf = UnisensFactoryBuilder.createFactory();
        //    Unisens u = uf.createUnisens(path);
        //    ValuesEntry ve = u.createValuesEntry("values.csv", new String[] { "A", "B" }, DataType.INT16, 250);
        //    u.setMeasurementId("sampleData");
        //    u.setTimestampStart(DateTime.Now);
        //    u.setComment("Example data set , derived from Holtervergleich_0001 .");

        //    CsvFileFormat cff = ve.createCsvFileFormat();
        //    cff.setComment("csv , 2 channel ");
        //    cff.setSeparator(";");
        //    cff.setDecimalSeparator(".");
        //    ve.setFileFormat(cff);
        //    var A = new short[][] { new short[] { 1, 4 }, new short[] { 2, 5 }, new short[] { 3, 32767 } };
        //    ValueList valueList = new ValueList(new long[3] { 1320, 22968, 30232 }, A);
        //    ve.appendValuesList(valueList);
        //    //ve.append(new Value(1320, new short[2] { 1, 4 }));
        //    //ve.append(new Value(22968, new short[2] { 2, 5 }));
        //    //ve.append(new Value(30232, new short[2] { 3, 32767 }));
        //    ve.setContentClass("BLOODPRESSURE");
        //    ve.setSourceId("bloodpressure .csv");
        //    ve.setComment("Blutdruck ");
        //    ve.setSampleRate(1);
        //    ve.setLsbValue(1);
        //    ve.setUnit("mmHg");
        //    ve.setDataType(DataType.INT16);

        //    u.save();
        //    u.closeAll();
        //}
	
        //private void valuesBin()
        //{
        //    string[] Channelnames = { "systolisch", "diastolisch" };
        //    UnisensFactory uf = UnisensFactoryBuilder.createFactory();
        //    Unisens u = uf.createUnisens(path);
        //    ValuesEntry ve = u.createValuesEntry("values.bin", new String[] { "A", "B" }, DataType.INT8, 250);
        //    u.setMeasurementId("sampleData");
        //    u.setTimestampStart(DateTime.Now);
        //    u.setComment("Example data set , derived from Holtervergleich_0001 .");
           
        //    ve.setFileFormat(ve.createBinFileFormat());
        //    var A = new sbyte[][] { new sbyte[] { 1, 4 }, new sbyte[] { 2, 5 }, new sbyte[] { 3, 6 } };
        //    ValueList valueList = new ValueList(new long[3] {1320,22968,30232}, A);
        //    ve.appendValuesList(valueList);            
        //    //ve.append(new Value(1320, new sbyte[2] { 1, 4 }));
        //    //ve.append(new Value(22968, new sbyte[2] { 2, 5 }));
        //    //ve.append(new Value(30232, new sbyte[2] { 3, 6 }));
        //    ve.setContentClass("BLOODPRESSURE");
        //    ve.setSourceId("bloodpressure .csv");
        //    ve.setComment("Blutdruck ");
        //    ve.setSampleRate(1);
        //    ve.setLsbValue(1);
        //    ve.setUnit("mmHg");
        //    ve.setDataType(DataType.INT16);

        //    u.save();
        //    u.closeAll();
        //}
	
        //private void valuesXml()
        //{
        //    UnisensFactory uf = UnisensFactoryBuilder.createFactory();
        //    Unisens u = uf.createUnisens(path);
        //    ValuesEntry ve = u.createValuesEntry("values.xml", new String[] { "A", "B" }, DataType.INT16, 250);
        //    u.setMeasurementId("sampleData");
        //    u.setTimestampStart(DateTime.Now);
        //    u.setComment("Example data set , derived from Holtervergleich_0001 .");

        //    ve.setFileFormat(ve.createXmlFileFormat());
        //    ve.append(new Value(1320, new short[2] { 1, 4 }));
        //    ve.append(new Value(22968, new short[2] { 2, 5 }));
        //    ve.append(new Value(30232, new short[2] { 3, 6 }));
        //    ve.setContentClass("BLOODPRESSURE");
        //    ve.setSourceId("bloodpressure .csv");
        //    ve.setComment("Blutdruck ");
        //    ve.setSampleRate(1);
        //    ve.setLsbValue(1);
        //    ve.setUnit("mmHg");
        //    ve.setDataType(DataType.INT16);

        //    u.save();
        //    u.closeAll();
        //}
	
	    private void eventCsv() 
	    {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path);
            EventEntry ee = u.createEventEntry("event.csv", 1000);
            u.setMeasurementId("sampleData");
            u.setTimestampStart(DateTime.Now);
            u.setComment("Example data set , derived from Holtervergleich_0001 .");

            CsvFileFormat cff = ee.createCsvFileFormat();
            cff.setSeparator(";");
            cff.setDecimalSeparator(".");
            ee.setFileFormat(cff);

            List<Event> eventList = new List<Event>();
            eventList.Add(new Event(124, "N", "NORMAL"));
            eventList.Add(new Event(346, "N", "NORMAL"));
            eventList.Add(new Event(523, "N", "NORMAL"));
            ee.append(eventList);
          
            ee.setComment("Reference trigger list ");
            ee.setContentClass("TRIGGER");
            ee.setSourceId("trigger_reference .csv");
            ee.setSampleRate(1000);
            ee.setSource("PADSY /M. Kirst ");
            ee.setTypeLength(1);

            u.save();
            u.closeAll();
	    }
	
	    private void eventXml()
	    {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path);
            EventEntry ee = u.createEventEntry("event.xml", 1000);
            u.setMeasurementId("sampleData");
            u.setTimestampStart(DateTime.Now);
            u.setComment("Example data set , derived from Holtervergleich_0001 .");

            ee.setFileFormat(ee.createXmlFileFormat());
            List<Event> eventList = new List<Event>();
            eventList.Add(new Event(124, "N", "NORMAL"));
            eventList.Add(new Event(346, "N", "NORMAL"));
            eventList.Add(new Event(523, "V", "PVC"));
            ee.append(eventList);
            //ee.append(new Event(124, "N", "NORMAL"));
            //ee.append(new Event(346, "N", "NORMAL"));
            //ee.append(new Event(523, "V", "PVC"));
            ee.setComment("Reference trigger list ");
            ee.setContentClass("TRIGGER");
            ee.setSourceId("trigger_reference .csv");
            ee.setSampleRate(1000);
            ee.setSource("PADSY /M. Kirst ");
            ee.setTypeLength(1);

            u.save();
            u.closeAll();
	    }
	
	    private void eventBin()
	    {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens(path);
            EventEntry ee = u.createEventEntry("event.bin", 1000);
            u.setMeasurementId("sampleData");
            u.setTimestampStart(DateTime.Now);
            u.setComment("Example data set , derived from Holtervergleich_0001 .");

            ee.setFileFormat(ee.createBinFileFormat());
            ee.setCommentLength(6);
            ee.setTypeLength(1);
            List<Event> eventList = new List<Event>();
            eventList.Add(new Event(124, "N", "NORMAL"));
            eventList.Add(new Event(346, "N", "NORMAL"));
            eventList.Add(new Event(523, "V", "PVC"));
            ee.append(eventList);
            //ee.append(new Event(124, "N", "NORMAL"));
            //ee.append(new Event(346, "N", "NORMAL"));
            //ee.append(new Event(523, "V", "PVC   "));
            ee.setComment("Reference trigger list ");
            ee.setContentClass("TRIGGER");
            ee.setSourceId("trigger_reference .csv");
            ee.setSampleRate(1000);
            ee.setSource("PADSY /M. Kirst ");
            ee.setTypeLength(1);

            u.save();
            u.closeAll();
	    }	
    }
}
