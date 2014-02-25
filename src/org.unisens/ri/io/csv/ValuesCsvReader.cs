using System;
using System.IO;
using org.unisens.ri.util;

namespace org.unisens.ri.io.csv
{
    public class ValuesCsvReader : ValuesReader
    {
        private StreamReader file = null;
        private String separator;
        public ValuesCsvReader(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {
            open();
        }

        public override void open()
        {
            if (!isOpened)
            {
                FileStream fs = new FileStream(absoluteFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                file = new StreamReader(fs);
                separator = ((CsvFileFormat)valuesEntry.getFileFormat()).getSeparator();
                isOpened = true;
            }
        }


        public override void close()
        {
            if (isOpened)
                file.Close();
            isOpened = false;
        }

        public override Value[] Read(int length)
        {
            return read(currentSample, length, false);
        }


        public override Value[] Read(long pos, int length)
        {
            return read(pos, length, false);
        }




        public override Value[] readScaled(int length)
        {
            return read(currentSample, length, true);
        }


        public override Value[] readScaled(long pos, int length)
        {
            return read(pos, length, true);
        }

        private Value[] read(long pos, int length, Boolean scaled)
        {
            open();
            long sampleCount = valuesEntry.getCount();
            if (pos > sampleCount)
                return null;
            if (pos + length > sampleCount)
                length = (int)(sampleCount - pos);
            if (pos != currentSample)
                seek(pos);

            if (scaled)
            {
                if (dataType == DataType.INT8)
                    return readByteAsArrayScaled(length);
                if ((dataType == DataType.INT16) || (dataType == DataType.UINT8))
                    return readShortAsArrayScaled(length);
                if ((dataType == DataType.INT32) || (dataType == DataType.UINT16))
                    return readIntAsArrayScaled(length);
                if (dataType == DataType.UINT32)
                    return readLongAsArrayScaled(length);
                if (dataType == DataType.FLOAT)
                    return readFloatAsArrayScaled(length);
                if (dataType == DataType.DOUBLE)
                    return readDoubleAsArrayScaled(length);
            }
            else
            {
                if (dataType == DataType.INT8)
                    return readByteAsArray(length);
                if ((dataType == DataType.INT16) || (dataType == DataType.UINT8))
                    return readShortAsArray(length);
                if ((dataType == DataType.INT32) || (dataType == DataType.UINT16))
                    return readIntAsArray(length);
                if (dataType == DataType.UINT32)
                    return readLongAsArray(length);
                if (dataType == DataType.FLOAT)
                    return readFloatAsArray(length);
                if (dataType == DataType.DOUBLE)
                    return readDoubleAsArray(length);
            }

            return null;
        }


        public override ValueList readValuesListScaled(int length)
        {
            return readValuesList(currentSample, length, true);
        }


        public override ValueList readValuesListScaled(long pos, int length)
        {
            return readValuesList(pos, length, true);
        }

        public override ValueList ReadValuesList(int length)
        {
            return readValuesList(currentSample, length, false);
        }

        public override ValueList ReadValuesList(long pos, int length)
        {
            return readValuesList(pos, length, false);
        }

        private ValueList readValuesList(long pos, int length, Boolean scaled)
        {
            open();
            if (pos > valuesEntry.getCount())
                return null;
            if (pos < 0)
                pos = 0;
            if (pos != currentSample)
                seek(pos);
            if (pos + length > valuesEntry.getCount())
                length = (int)(valuesEntry.getCount() - pos);

            if (scaled)
            {
                if (dataType == DataType.INT8)
                    return readByteAsListScaled(length);
                if (dataType == DataType.INT16 || dataType == DataType.UINT8)
                    return readShortAsListScaled(length);
                if (dataType == DataType.INT32 || dataType == DataType.UINT16)
                    return readIntAsListScaled(length);
                if (dataType == DataType.UINT32)
                    return readLongAsListScaled(length);
                if ((dataType == DataType.FLOAT))
                    return readFloatAsListScaled(length);
                if (dataType == DataType.DOUBLE)
                    return readDoubleAsListScaled(length);
            }
            else
            {
                if (dataType == DataType.INT8)
                    return readByteAsList(length);
                if (dataType == DataType.INT16 || dataType == DataType.UINT8)
                    return readShortAsList(length);
                if (dataType == DataType.INT32 || dataType == DataType.UINT16)
                    return readIntAsList(length);
                if (dataType == DataType.UINT32)
                    return readLongAsList(length);
                if ((dataType == DataType.FLOAT))
                    return readFloatAsList(length);
                if (dataType == DataType.DOUBLE)
                    return readDoubleAsList(length);
            }
            return null;
        }


        public override long getSampleCount()
        {
            //try
            //{
                open();
                long sampleCount = 0;
                while (file.ReadLine() != null)
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
                    file.ReadLine();
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

        private Value[] readByteAsArray(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                byte[] data = new byte[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = Byte.Parse(tokenizer.NextToken().Trim());
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readShortAsArray(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                short[] data = new short[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = short.Parse(tokenizer.NextToken().Trim());
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readIntAsArray(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                int[] data = new int[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = int.Parse(tokenizer.NextToken().Trim());
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readLongAsArray(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                long[] data = new long[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = long.Parse(tokenizer.NextToken().Trim());
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readFloatAsArray(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                float[] data = new float[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = float.Parse(tokenizer.NextToken().Trim());
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readDoubleAsArray(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = Double.Parse(tokenizer.NextToken().Trim());
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readByteAsArrayScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = (Byte.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue; ;
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readShortAsArrayScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = (short.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readIntAsArrayScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = (int.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readLongAsArrayScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (var i = 0; i < channelCount; i++)
                    data[i] = (long.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readFloatAsArrayScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = (float.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }

        private Value[] readDoubleAsArrayScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            Value[] values = new Value[length];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                double[] data = new double[channelCount];
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                long samplestamp = long.Parse(tokenizer.NextToken().Trim());
                for (int i = 0; i < channelCount; i++)
                    data[i] = (Double.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                values[sampleNumber] = new Value(samplestamp, data);
                currentSample++;
            }
            return values;
        }


        private ValueList readByteAsList(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            byte[][] data = new byte[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new byte[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = Byte.Parse(tokenizer.NextToken().Trim());
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readShortAsList(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            short[][] data = new short[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new short[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = short.Parse(tokenizer.NextToken().Trim());
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readIntAsList(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            var valueList = new ValueList();
            var timestamps = new long[length];
            var data = new int[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = int.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new int[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = int.Parse(tokenizer.NextToken().Trim());
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readLongAsList(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            long[][] data = new long[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new long[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = long.Parse(tokenizer.NextToken().Trim());
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readFloatAsList(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            float[][] data = new float[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new float[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = float.Parse(tokenizer.NextToken().Trim());
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readDoubleAsList(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            var valueList = new ValueList();
            var timestamps = new long[length];
            var data = new double[length][];
            for (var sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = Double.Parse(tokenizer.NextToken().Trim());
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }

        private ValueList readByteAsListScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            double[][] data = new double[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = (Byte.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readShortAsListScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            double[][] data = new double[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = (short.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readIntAsListScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            double[][] data = new double[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = int.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = (int.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readLongAsListScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            var valueList = new ValueList();
            var timestamps = new long[length];
            var data = new double[length][];
            for (var sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = (long.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readFloatAsListScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            double[][] data = new double[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = (float.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
        private ValueList readDoubleAsListScaled(int length)
        {
            String line = "";
            StringTokenizer tokenizer;
            ValueList valueList = new ValueList();
            long[] timestamps = new long[length];
            double[][] data = new double[length][];
            for (int sampleNumber = 0; sampleNumber < length; sampleNumber++)
            {
                line = file.ReadLine();
                tokenizer = new StringTokenizer(line, separator);
                timestamps[sampleNumber] = long.Parse(tokenizer.NextToken().Trim());
                data[sampleNumber] = new double[channelCount];
                for (int i = 0; i < channelCount; i++)
                    data[sampleNumber][i] = (Double.Parse(tokenizer.NextToken().Trim()) + baseline) * lsbValue;
                currentSample++;
            }
            valueList.setSamplestamps(timestamps);
            valueList.setData(data);
            return valueList;
        }
    }
}