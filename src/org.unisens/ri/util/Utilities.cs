using System;
using System.IO;

namespace org.unisens.ri.util
{
    public class Utilities
    {

        public static Object convertFrom1DimTo2DimArray(Object data)
        {
            if (data is short[])
            {
                short[] data1 = (short[])data;
                short[][] result = new short[data1.Length][];
                for (int i = 0; i < data1.Length; i++)
                {
                    result[i] = new short[1];
                    result[i][0] = data1[i];
                }
                return result;
            }
            if (data is int[])
            {
                int[] data1 = (int[])data;
                int[][] result = new int[data1.Length][];
                for (int i = 0; i < data1.Length; i++)
                {
                    result[i] = new int[1];
                    result[i][0] = data1[i];
                }
                return result;
            }
            if (data is long[])
            {
                long[] data1 = (long[])data;
                long[][] result = new long[data1.Length][];
                for (int i = 0; i < data1.Length; i++)
                {
                    result[i] = new long[1];
                    result[i][0] = data1[i];
                }
                return result;
            }
            if (data is double[])
            {
                double[] data1 = (double[])data;
                double[][] result = new double[data1.Length][];
                for (int i = 0; i < data1.Length; i++)
                {
                    result[i] = new double[1];
                    result[i][0] = data1[i];
                }
                return result;
            }
            if (data is float[])
            {
                float[] data1 = (float[])data;
                float[][] result = new float[data1.Length][];
                for (int i = 0; i < data1.Length; i++)
                {
                    result[i] = new float[1];
                    result[i][0] = data1[i];
                }
                return result;
            }
            return data;
        }

        public static DateTime convertStringToDate(String date)
        {
            //try
            //{
                return DateTime.Parse(date);
            //}
            //catch (FormatException pe)
            //{
            //    pe.printStackTrace();
            //    return DateTime.MinValue;
            //}
        }

        public static String convertDateToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fff");
        }

        public static void copyFile(FileStream inFile, FileStream outFile)
        {
            //try
            //{
                byte[] buf = new byte[1024];
                int i = 0;
                while ((i = inFile.Read(buf, i, 1024)) != -1)
                {
                    outFile.Write(buf, 0, i);
                }
                if (inFile != null) inFile.Close();
                if (outFile != null) outFile.Close();
            //}
            //catch (IOException e)
            //{
            //    e.printStackTrace();
            //}
        }

        public static int getSampleCount(Object data, DataType dataType)
        {
            int length = 0;

            switch (Convert.ToInt32(dataType))
            {
                case 0:
                    {
                        if (data is SByte[][])
                            length = ((SByte[][])data).Length;
                        else
                            length = ((SByte[])data).Length;
                        break;
                    }
                case 1:
                    {
                        if (data is byte[][])
                            length = ((byte[][])data).Length;
                        else
                            length = ((byte[])data).Length;
                        break;
                    }
                case 2:
                    {
                        if (data is short[][])
                            length = ((short[][])data).Length;
                        else
                            length = ((short[])data).Length;
                        break;
                    }
                case 3:
                    {
                        if (data is ushort[][])
                            length = ((ushort[][])data).Length;
                        else
                            length = ((ushort[])data).Length;
                        break;
                    }
                case 4:
                    {
                        if (data is int[][])
                            length = ((int[][])data).Length;
                        else
                            length = ((int[])data).Length;
                        break;
                    }
                case 5:
                    {
                        if (data is UInt32[][])
                            length = ((UInt32[][])data).Length;
                        else
                            length = ((UInt32[])data).Length;
                        break;
                    }
                case 6:
                    {
                        if (data is float[][])
                            length = ((float[][])data).Length;
                        else
                            length = ((float[])data).Length;
                        break;
                    }
                case 7:
                    {
                        if (data is double[][])
                            length = ((double[][])data).Length;
                        else
                            length = ((double[])data).Length;
                        break;
                    }
            }
            return length;
        }
    }
}