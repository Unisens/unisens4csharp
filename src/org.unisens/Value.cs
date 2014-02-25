using System;

namespace org.unisens
{


    /**
     * Value represent data values acquired at a certain point in time. Points in time 
     * are given as samplestamps, i.e. sample number using a specific sampleRate that 
     * is set in the {@link ValuesEntry}. 
     *
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public class Value
    {

        private long _samplestamp;
        private Object _data;

        /**
         * Constructor
         * 
         * @param samplestamp point in time determined by sample number
         * @param data the data as object [channelCount]
         */
        public Value(long samplestamp, Object data)
        {
            _samplestamp = samplestamp;
            SetData(data);
        }

        /**
         * Gets the samplestamp of this Value.
         * 
         * @return the samplestamp
         */
        public long GetSamplestamp()
        {
            return _samplestamp;
        }

        /**
         * Sets the samplestamp to this Value
         * 
         * @param samplestamp
         */
        public void SetSamplestamp(long samplestamp)
        {
            this._samplestamp = samplestamp;
        }

        /**
         * Gets the data of this Value as object [channelCount]. Where channelCount is the number of 
         * channels of this Value
         * 
         * @return the data of this value
         */
        public Object GetData()
        {
            return _data;
        }


        /**
         * Sets the data of this Values. 
         * 
         * @param data the data of this Value
         */
        public void SetData(Object data)
        {
            if (data is Byte)
            {
                _data = new byte[] { (Byte)data };
                return;
            }
            if (data is short)
            {
                _data = new short[] { (short)data };
                return;
            }
            if (data is int)
            {
                _data = new int[] { (int)data };
                return;
            }
            if (data is long)
            {
                _data = new long[] { (long)data };
                return;
            }
            if (data is float)
            {
                _data = new float[] { (float)data };
                return;
            }
            if (data is Double)
            {
                _data = new double[] { (Double)data };
                return;
            }
            _data = data;
        }

        public new bool Equals(Object o)
        {
            if (!(o is Value))
                return false;
            var aValue = (Value)o;
            if (_samplestamp != aValue.GetSamplestamp())
                return false;
            if (_data is byte[] && aValue.GetData() is byte[])
            {
                var data1 = (byte[])_data;
                var data2 = (byte[])aValue.GetData();
                if (data1.Length != data2.Length)
                    return false;
                for(int i = 0; i < data1.Length; i ++)
                {
                    if (data1[i] != data2[i])
                        return false;
                }
                return true;
            }
            if (_data is short[] && aValue.GetData() is short[])
            {
                var data1 = (short[])_data;
                var data2 = (short[])aValue.GetData();
                if (data1.Length != data2.Length)
                    return false;
                for (int i = 0; i < data1.Length; i++)
                {
                    if (data1[i] != data2[i])
                        return false;
                }
                return true;
            }
            if (_data is int[] && aValue.GetData() is int[])
            {
                var data1 = (int[])_data;
                var data2 = (int[])aValue.GetData();
                if (data1.Length != data2.Length)
                    return false;
                for (int i = 0; i < data1.Length; i++)
                {
                    if (data1[i] != data2[i])
                        return false;
                }
                return true;
            }
            if (_data is long[] && aValue.GetData() is long[])
            {
                var data1 = (long[])_data;
                var data2 = (long[])aValue.GetData();
                if (data1.Length != data2.Length)
                    return false;
                for (int i = 0; i < data1.Length; i++)
                {
                    if (data1[i] != data2[i])
                        return false;
                }
                return true;
            }
            if (_data is double[] && aValue.GetData() is double[])
            {
                var data1 = (double[])_data;
                var data2 = (double[])aValue.GetData();
                if (data1.Length != data2.Length)
                    return false;
                for (int i = 0; i < data1.Length; i++)
                {
                    if (data1[i] != data2[i])
                        return false;
                }
                return true;
            }
            if (_data is float[] && aValue.GetData() is float[])
            {
                var data1 = (float[])_data;
                var data2 = (float[])aValue.GetData();
                if (data1.Length != data2.Length)
                    return false;
                for (int i = 0; i < data1.Length; i++)
                {
                    if (data1[i] != data2[i])
                        return false;
                }
                return true;
            }
            return false;
        }

    }
}