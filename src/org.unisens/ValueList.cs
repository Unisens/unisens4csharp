
/**
 * ValueList is a set of Values that can be accessed as Array of timestamps and object [length][].
 * ValueList can for example be used from Matlab for a easier access.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
using System;

namespace org.unisens
{
    public class ValueList
    {
        private long[] samplestamps;
        private Object data;

        public ValueList() { }

        /**
         * Constructor
         * 
         * @param samplestamps the Array of samplestamps
         * @param data of this ValueList as object [length][channelCount]
         */
        public ValueList(long[] samplestamps, Object data)
        {
            this.samplestamps = samplestamps;
            setData(data);
        }

        /**
         * Gets the samplestamps of this list of values.
         * 
         * @return the Array of samplestamps 
         */
        public long[] getSamplestamps()
        {
            return samplestamps;
        }

        /**
         * Sets the samplestamps of this list of values
         * 
         * @param samplestamps the Array of samplestamps
         */
        public void setSamplestamps(long[] samplestamps)
        {
            this.samplestamps = samplestamps;
        }

        /**
         * Gets the rows of data of this ValueList as object [length][]
         * 
         * @return the rows of data
         */
        public Object getData()
        {
            return data;
        }

        /**
         * Sets the rows of data of this ValueList as object [length][]
         * 
         * @param data the rows of data
         */
        public void setData(Object data)
        {
            this.data = data;
        }

        public Boolean equals(Object o)
        {
            if (!(o is ValueList))
                return false;
            var valueList = (ValueList)o;
            long[] valueListSamplestamps = valueList.getSamplestamps();
            if (samplestamps.Length != valueListSamplestamps.Length)
                return false;
            for (int i = 0; i < valueListSamplestamps.Length; i++)
            {
                if (valueListSamplestamps[i] != samplestamps[i])
                    return false;
            }
            if (data is byte[][] && valueList.getData() is byte[][])
            {
                var data1 = (byte[][])data;
                var data2 = (byte[][])valueList.getData();

                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (var i = 0; i < data2.Length; i++)
                {
                    for (var j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            if (data is short[][] && valueList.getData() is short[][])
            {
                var data1 = (short[][])data;
                var data2 = (short[][])valueList.getData();
                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (int i = 0; i < data2.Length; i++)
                {
                    for (int j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            if (data is int[][] && valueList.getData() is int[][])
            {
                var data1 = (int[][])data;
                var data2 = (int[][])valueList.getData();
                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (var i = 0; i < data2.Length; i++)
                {
                    for (var j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            if (data is long[][] && valueList.getData() is long[][])
            {
                var data1 = (long[][])data;
                var data2 = (long[][])valueList.getData();
                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (int i = 0; i < data2.Length; i++)
                {
                    for (int j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            if (data is float[][] && valueList.getData() is float[][])
            {
                float[][] data1 = (float[][])data;
                float[][] data2 = (float[][])valueList.getData();
                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (int i = 0; i < data2.Length; i++)
                {
                    for (int j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            if (data is double[][] && valueList.getData() is double[][])
            {
                double[][] data1 = (double[][])data;
                double[][] data2 = (double[][])valueList.getData();
                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (int i = 0; i < data2.Length; i++)
                {
                    for (int j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            return false;
        }

        public Boolean equals<T>(Object o) where T : Type
        {
            if (!(o is ValueList))
                return false;
            var valueList = (ValueList)o;
            long[] valueListSamplestamps = valueList.getSamplestamps();
            if (samplestamps.Length != valueListSamplestamps.Length)
                return false;
            for (int i = 0; i < valueListSamplestamps.Length; i++)
            {
                if (valueListSamplestamps[i] != samplestamps[i])
                    return false;
            }
            if (data is T[][] && valueList.getData() is T[][])
            {
                var data1 = (T[][])data;
                var data2 = (T[][])valueList.getData();

                if (data1.Length != data2.Length)
                    return false;
                if (data1[0].Length != data2[0].Length)
                    return false;
                for (int i = 0; i < data2.Length; i++)
                {
                    for (int j = 0; j < data2[0].Length; j++)
                    {
                        if (data1[i][j] != data2[i][j])
                            return false;
                    }

                }
                return true;
            }
            return false;
        }

    }
}