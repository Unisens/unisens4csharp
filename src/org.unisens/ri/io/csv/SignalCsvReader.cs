using System;
using System.Globalization;
using System.IO;
using org.unisens.ri.util;

namespace org.unisens.ri.io.csv
{
    public class SignalCsvReader : SignalReader
    {
        private StreamReader _file;
        private String _separator;
        private NumberFormatInfo _decimalFormat;

        public SignalCsvReader(SignalEntry signalEntry)
            : base(signalEntry)
        {
            open();
        }

        public override void open()
        {
            if (!isOpened)
            {
                FileStream fs = new FileStream(absoluteFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _file = new StreamReader(fs);
                _separator = ((CsvFileFormat)signalEntry.getFileFormat()).getSeparator();
                _decimalFormat = new NumberFormatInfo
                                    {
                                        NumberDecimalSeparator =
                                            ((CsvFileFormat)signalEntry.getFileFormat()).getDecimalSeparator(),
                                        NumberDecimalDigits = 99
                                    };
                isOpened = true;
                currentSample = 0;
            }
        }


        public override void close()
        {
            if (isOpened)
                _file.Close();
            isOpened = false;
        }


        public override Object Read(int length)
        {
            return Read(length, false);
        }


        public override Object Read(long pos, int length)
        {
            return Read(pos, length, false);
        }


        public override double[][] readScaled(int length)
        {
            return (double[][])Read(length, true);
        }


        public override double[][] readScaled(long pos, int length)
        {
            return (double[][])Read(pos, length, true);
        }

        private Object Read(int length, Boolean scaled)
        {
            return Read(currentSample, length, scaled);
        }
        private Object Read(long pos, int length, Boolean scaled)
        {
            open();
            long sampleCount = signalEntry.getCount();

            if (pos > sampleCount)
                return null;
            if (pos != currentSample)
                seek(pos);
            if (currentSample + length > sampleCount)
                length = (int)(sampleCount - currentSample);

            if (scaled)
            {
                if (dataType == DataType.INT8)
                    return readSByteScaled(length);
                if (dataType == DataType.UINT8)
                    return readByteScaled(length);
                if (dataType == DataType.INT16)
                    return readShortScaled(length);
                if (dataType == DataType.UINT16)
                    return readUInt16Scaled(length); 
                if (dataType == DataType.INT32)
                    return readIntScaled(length);
                if (dataType == DataType.UINT32)
                    return readUInt32Scaled(length);
                if (dataType == DataType.FLOAT)
                    return readFloatScaled(length);
                if (dataType == DataType.DOUBLE)
                    return readDoubleScaled(length);
            }
            else
            {
                if (dataType == DataType.INT8)
                    return ReadSByte(length);
                if (dataType == DataType.UINT8)
                    return ReadByte(length);
                if (dataType == DataType.INT16) 
                    return ReadShort(length);
                if (dataType == DataType.UINT16)
                    return ReadUInt16(length);
                if (dataType == DataType.INT32) 
                    return ReadInt(length);
                if (dataType == DataType.UINT32)
                    return readUInt32(length);
                if (dataType == DataType.FLOAT)
                    return readFloat(length);
                if (dataType == DataType.DOUBLE)
                    return readDouble(length);
            }
            close();
            return null;
        }

        private byte[][] ReadByte(int length)
        {
            StringTokenizer tokenizer;
            var data = new byte[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                String line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new byte[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    byte currentValue = Convert.ToByte(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = currentValue;
                }
                currentSample++;
            }

            return data;
        }

        private sbyte[][] ReadSByte(int length)
        {
            StringTokenizer tokenizer;
            var data = new sbyte[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                String line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new sbyte[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    sbyte currentValue = Convert.ToSByte(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = currentValue;
                }
                currentSample++;
            }

            return data;
        }

        private short[][] ReadShort(int length)
        {
            StringTokenizer tokenizer;
            var data = new short[length][];

            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                var line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new short[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    short currentValue = Convert.ToInt16(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = currentValue;
                }
                currentSample++;
            }
            return data;
        }

        private UInt16[][] ReadUInt16(int length)
        {
            StringTokenizer tokenizer;
            var data = new UInt16[length][];

            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                var line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new UInt16[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    UInt16 currentValue = Convert.ToUInt16(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = currentValue;
                }
                currentSample++;
            }
            return data;
        }

        private int[][] ReadInt(int length)
        {
            StringTokenizer tokenizer;
            var data = new int[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                String line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new int[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    int currentValue = Convert.ToInt32(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = currentValue;
                }
                currentSample++;
            }

            return data;
        }
        private UInt32[][] readUInt32(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            UInt32[][] data = new UInt32[length][];
            UInt32 currentValue = 0;
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new UInt32[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToUInt32(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = currentValue;
                }
                currentSample++;
            }
            return data;
        }
        private float[][] readFloat(int length)
        {
            //try
            //{
                String line = "";
                StringTokenizer tokenizer;
                float[][] data = new float[length][];
                float currentValue = 0;
                for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
                {
                    line = _file.ReadLine();
                    tokenizer = new StringTokenizer(line, _separator);
                    data[sampleNumber] = new float[channelCount];
                    for (int i = 0; i < channelCount; i++)
                    {
                        currentValue = Convert.ToSingle(tokenizer.NextToken().Trim(), _decimalFormat);
                        data[sampleNumber][i] = currentValue;
                    }
                    currentSample++;
                }
                return data;
            //}
            //catch (FormatException pe)
            //{
            //    return null;
            //}
        }
        private double[][] readDouble(int length)
        {
            //try
            //{
                String line = "";
                StringTokenizer tokenizer;
                double[][] data = new double[length][];
                double currentValue = 0;
                for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
                {
                    line = _file.ReadLine();
                    tokenizer = new StringTokenizer(line, _separator);
                    data[sampleNumber] = new double[channelCount];
                    for (int i = 0; i < channelCount; i++)
                    {
                        currentValue = Convert.ToDouble(tokenizer.NextToken().Trim(), CultureInfo.InvariantCulture);// _decimalFormat);
                        data[sampleNumber][i] = currentValue;
                    }
                    currentSample++;
                }
                return data;
            //}
            //catch (FormatException pe)
            //{
            //    pe.printStackTrace();
            //    return null;
            //}
        }

        private double[][] readByteScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            double[][] data = new double[length][];
            byte currentValue = 0;
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToByte(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                }
                currentSample++;
            }

            return data;
        }

        private double[][] readSByteScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            double[][] data = new double[length][];
            sbyte currentValue = 0;
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToSByte(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                }
                currentSample++;
            }

            return data;
        }


        private double[][] readShortScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            double[][] data = new double[length][];
            short currentValue = 0;

            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToInt16(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                }
                currentSample++;
            }
            return data;
        }

        private double[][] readUInt16Scaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            double[][] data = new double[length][];
            UInt16 currentValue = 0;

            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToUInt16(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                }
                currentSample++;
            }
            return data;
        }

        private double[][] readIntScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            double[][] data = new double[length][];
            int currentValue = 0;
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToInt32(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                }
                currentSample++;
            }

            return data;
        }
        private double[][] readUInt32Scaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            double[][] data = new double[length][];
            UInt32 currentValue = 0;
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = _file.ReadLine();
                tokenizer = new StringTokenizer(line, _separator);
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                {
                    currentValue = Convert.ToUInt32(tokenizer.NextToken().Trim());
                    data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                }
                currentSample++;
            }
            return data;
        }
        private double[][] readFloatScaled(int length)
        {
            //try
            //{
                String line = "";
                StringTokenizer tokenizer;
                double[][] data = new double[length][];
                float currentValue = 0;
                for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
                {
                    line = _file.ReadLine();
                    tokenizer = new StringTokenizer(line, _separator);
                    data[sampleNumber] = new double[channelCount];
                    for (int i = 0; i < channelCount; i++)
                    {
                        currentValue = Convert.ToSingle(tokenizer.NextToken().Trim(), _decimalFormat);
                        data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                    }
                    currentSample++;
                }
                return data;
            //}
            //catch (FormatException pe)
            //{
            //    pe.printStackTrace();
            //    return null;
            //}
        }
        private double[][] readDoubleScaled(int length)
        {
            //try
            //{
                String line = "";
                StringTokenizer tokenizer;
                double[][] data = new double[length][];
                double currentValue = 0;
                for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
                {
                    line = _file.ReadLine();
                    tokenizer = new StringTokenizer(line, _separator);
                    data[sampleNumber] = new double[channelCount];
                    for (int i = 0; i < channelCount; i++)
                    {
                        currentValue = Convert.ToDouble(tokenizer.NextToken().Trim(), _decimalFormat);
                        data[sampleNumber][i] = (currentValue + baseline) * lsbValue;
                    }
                    currentSample++;
                }
                return data;
            //}
            //catch (FormatException pe)
            //{
            //    pe.printStackTrace();
            //    return null;
            //}
        }

        public override void resetPos()
        {
            //try
            //{
                currentSample = 0;
                close();
                open();
            //}
            //catch (IOException ioe)
            //{
            //    ioe.printStackTrace();
            //}

        }

        private void seek(long pos)
        {
            //try
            //{
                while (currentSample < pos)
                {
                    _file.ReadLine();
                    currentSample++;
                }
                if (currentSample > pos)
                {
                    resetPos();
                    seek(pos);
                }
            //}
            //catch (IOException ioe)
            //{
            //    ioe.printStackTrace();
            //}
        }

        public override long getSampleCount()
        {
            //try
            //{
                open();
                long sampleCount = 0;
                while (_file.ReadLine() != null)
                    sampleCount++;
                resetPos();
                return sampleCount;
            //}
            //catch (IOException ioe)
            //{
            //    ioe.printStackTrace();
            //    return -1;
            //}
        }


    }
}