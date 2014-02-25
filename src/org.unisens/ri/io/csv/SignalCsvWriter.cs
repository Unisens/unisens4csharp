

using System;
using System.Globalization;
using System.IO;
using org.unisens.ri.util;

namespace org.unisens.ri.io.csv
{
    public class SignalCsvWriter : SignalWriter
    {
        private StreamWriter file;
        private static String NEWLINE = Environment.NewLine;
        private String separator = null;
        private NumberFormatInfo decimalFormat = null;

        public SignalCsvWriter(SignalEntry signal)
            : base(signal)
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
                separator = ((CsvFileFormat)signalEntry.getFileFormat()).getSeparator();
                decimalFormat = new NumberFormatInfo
                                    {
                                        NumberGroupSizes = new int[] { 0 },
                                        NumberDecimalSeparator =
                        ((CsvFileFormat)signalEntry.getFileFormat()).getDecimalSeparator()
                                    };
                //decimalFormat.setDecimalSeparatorAlwaysShown(false);
            }
        }

        public override void close()
        {
            if (isOpened)
                file.Close();
            isOpened = false;
        }

        public override void Append(Object data)
        {
            open();
            appendData(data);
            file.Flush();
        }

        private void appendData(Object data)
        {
            if (channelCount == 1)
            {
                data = Utilities.convertFrom1DimTo2DimArray(data);
            }
            if ((dataType == DataType.INT8))
            {
                appendSByte((SByte[][])data);
                return;
            }
            if ((dataType == DataType.UINT8))
            {
                appendByte((byte[][])data);
                return;
            }
            if ((dataType == DataType.INT16)) 
            {
                appendShort((short[][])data);
                return;
            }
            if ((dataType == DataType.UINT16))
            {
                appendUShort((UInt16[][])data);
                return;
            }
            if ((dataType == DataType.INT32))  
            {
                appendInt((int[][])data);
                return;
            }
            if (dataType == DataType.UINT32)
            {
                appendUInt((UInt32[][])data);
                return;
            }
            if (dataType == DataType.FLOAT)
            {
                appendFloat((float[][])data);
                return;
            }
            if (dataType == DataType.DOUBLE)
            {
                appendDouble((double[][])data);
                return;
            }
        }


        private void appendByte(byte[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    line += data[i][j] + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1]);
            }
        }

        private void appendSByte(SByte[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    line += data[i][j] + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1]);
            }
        }
        private void appendShort(short[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    line += data[i][j] + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1]);
            }
        }

        private void appendUShort(ushort[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    line += data[i][j] + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1]);
            }
        }
        private void appendInt(int[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    line += data[i][j] + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1]);
            }
        }
        private void appendUInt(UInt32[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                    line += data[i][j] + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1]);
            }
        }
        private void appendFloat(float[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {
                
                    line += data[i][j].ToString("R", decimalFormat) + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1].ToString("R", decimalFormat));
            }
        }
        private void appendDouble(double[][] data)
        {
            String line;
            for (var i = 0; i < data.Length; i++)
            {
                line = "";
                for (var j = 0; j < data[i].Length - 1; j++)
                {

                    line += data[i][j].ToString("R", decimalFormat) + separator;
                }
                file.WriteLine(line + data[i][data[i].Length - 1].ToString("R", decimalFormat));
            }
        }
    }
}