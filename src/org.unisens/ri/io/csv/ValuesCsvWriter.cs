using System;
using System.IO;
using org.unisens.ri.util;

namespace org.unisens.ri.io.csv
{
    public class ValuesCsvWriter : ValuesWriter
    {
        private StreamWriter file = null;
        //private static String NEWLINE = Environment.NewLine;
        private String separator = null;

        public ValuesCsvWriter(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {
            open();
        }

        public override void open()
        {
            if (!isOpened)
            {
                FileStream fs = new FileStream(absoluteFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                file = new StreamWriter(fs);
                isOpened = true;
                separator = ((CsvFileFormat)valuesEntry.getFileFormat()).getSeparator();
            }
        }

        public override void close()
        {
            if (isOpened)
                file.Close();
            isOpened = false;
        }

        public override void append(Value value)
        {
            open();
            file.Write("" + value.GetSamplestamp() + separator);
            if (dataType == DataType.INT8)
            {
                appendSByte((sbyte[])value.GetData());
            }
            if (dataType == DataType.UINT8)
            {
                appendByte((byte[])value.GetData());
            }
            if (dataType == DataType.INT16)
            {
                appendShort((short[])value.GetData());
            }
            if  (dataType == DataType.UINT16)
            {
                appendUInt16((UInt16[])value.GetData());
            }
            if (dataType == DataType.INT32)
            {
                appendInt((int[])value.GetData());
            }
            if (dataType == DataType.UINT32)
            {
                appendUInt32((UInt32[])value.GetData());
            }
            if (dataType == DataType.FLOAT)
            {
                appendFloat((float[])value.GetData());
            }
            if (dataType == DataType.DOUBLE)
            {
                appendDouble((double[])value.GetData());
            }
            file.Flush();
        }

        public override void append(Value[] values)
        {
            open();
            if (dataType == DataType.UINT8)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendByte((byte[])t.GetData());
                }
            }
            if (dataType == DataType.INT8)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendSByte((sbyte[])t.GetData());
                }
            }
            if (dataType == DataType.INT16)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendShort((short[])t.GetData());
                }
            }
            if (dataType == DataType.UINT16)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendUInt16((UInt16[])t.GetData());
                }
            }
            if (dataType == DataType.INT32)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendInt((int[])t.GetData());
                }
            }
            if (dataType == DataType.UINT32)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendUInt32((UInt32[])t.GetData());
                }
            }
            if (dataType == DataType.FLOAT)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendFloat((float[])t.GetData());
                }
                return;
            }
            if (dataType == DataType.DOUBLE)
            {
                foreach (Value t in values)
                {
                    file.Write("" + t.GetSamplestamp() + separator);
                    appendDouble((double[])t.GetData());
                }
            }
            file.Flush();
        }

        public override void appendValuesList(ValueList valueList)
        {
            open();
            long[] timestamps = valueList.getSamplestamps();
            Object data = valueList.getData();
            if (valuesEntry.getChannelCount() == 1)
                data = Utilities.convertFrom1DimTo2DimArray(data);
            if (dataType == DataType.UINT8)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (byte[][])data;
                    var array = new byte[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendByte(array);
                }
            }
            if (dataType == DataType.INT8)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (sbyte[][])data;
                    var array = new sbyte[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendSByte(array);
                }
            }
            if (dataType == DataType.UINT16)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (UInt16[][])data;
                    var array = new UInt16[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendUInt16(array);
                }
            }
            if (dataType == DataType.INT16)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (short[][])data;
                    var array = new short[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendShort(array);
                }
            }
            if (dataType == DataType.INT32)
            {
                for(int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (int[][])data;
                    var array = new int[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendInt(array);
                }
            }
            if (dataType == DataType.UINT32)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (UInt32[][])data;
                    var array = new UInt32[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendUInt32(array);
                }
            }
            if (dataType == DataType.FLOAT)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (float[][])data;
                    var array = new float[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendFloat(array);
                } 
            }
            if (dataType == DataType.DOUBLE)
            {
                for (int i = 0; i < timestamps.Length; i++)
                {
                    long t = timestamps[i];
                    file.Write("" + t + separator);
                    var longData = (double[][])data;
                    var array = new double[longData[i].Length];
                    Array.Copy(longData[i], array, longData[i].Length);
                    appendDouble(array);
                } 
            }
            file.Flush();

        }

        private void appendByte(byte[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1]);
        }
        private void appendSByte(sbyte[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1]);
        }

        private void appendUInt16(UInt16[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1]);
        }
        private void appendShort(short[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1] );
        }

        private void appendInt(int[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1] );
        }

        private void appendUInt32(UInt32[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1] );
        }

        private void appendFloat(float[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1]);
        }

        private void appendDouble(double[] data)
        {
            String line = "";
            for (int i = 0; i < data.Length - 1; i++)
                line += data[i] + separator;
            file.WriteLine(line + data[data.Length - 1]);
        }

    }
}