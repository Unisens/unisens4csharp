using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using org.unisens.ri.config;

namespace org.unisens.ri.io.xml
{
    public class ValuesXmlReader : ValuesReader
    {
        private XmlDocument document;
        private XPathNavigator xpath;
        private XPathExpression expr;
        private XPathNodeIterator valueNodes;

        public ValuesXmlReader(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {

            open();
        }


        public override void open()
        {
            //try
            //{
                if (!isOpened)
                {
                    //DocumentBuilderFactory domFactory = DocumentBuilderFactory.newInstance();
                    //domFactory.setNamespaceAware(true); // never forget this!
                    //DocumentBuilder builder = domFactory.newDocumentBuilder();
                    //document = builder.parse(absoluteFileName);

                    //XPathFactory factory = XPathFactory.newInstance();
                    //xpath = factory.newXPath();
                    document = new XmlDocument();
                    document.Load(absoluteFileName);

                    var doc = new XPathDocument(File.OpenRead(absoluteFileName));
                    xpath = doc.CreateNavigator();
                }
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
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
            //try
            //{
                expr = xpath.Compile(String.Format(Constants.VALUES_XML_READER_VALUES_PATH, pos, pos + length + 1));
                //valueNodes = (NodeList)expr.evaluate(document, XPathConstants.NODESET);
                valueNodes = xpath.Select(expr);
                if (scaled)
                {
                    if (dataType == DataType.INT8)
                        return convertValueNodeListToValueByteArrayScaled(valueNodes);
                    if (dataType == DataType.INT16 || (dataType == DataType.UINT8))
                        return convertValueNodeListToValueShortArrayScaled(valueNodes);
                    if (dataType == DataType.INT32 || (dataType == DataType.UINT16))
                        return convertValueNodeListToValueIntArrayScaled(valueNodes);
                    if (dataType == DataType.UINT32)
                        return convertValueNodeListToValueLongArrayScaled(valueNodes);
                    if (dataType == DataType.FLOAT)
                        return convertValueNodeListToValueFloatArrayScaled(valueNodes);
                    if (dataType == DataType.DOUBLE)
                        return convertValueNodeListToValueDoubleArrayScaled(valueNodes);
                }
                else
                {
                    if (dataType == DataType.INT8)
                        return convertValueNodeListToValueByteArray(valueNodes);
                    if (dataType == DataType.INT16 || (dataType == DataType.UINT8))
                        return convertValueNodeListToValueShortArray(valueNodes);
                    if (dataType == DataType.INT32 || (dataType == DataType.UINT16))
                        return convertValueNodeListToValueIntArray(valueNodes);
                    if (dataType == DataType.UINT32)
                        return convertValueNodeListToValueLongArray(valueNodes);
                    if (dataType == DataType.FLOAT)
                        return convertValueNodeListToValueFloatArray(valueNodes);
                    if (dataType == DataType.DOUBLE)
                        return convertValueNodeListToValueDoubleArray(valueNodes);
                }
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
            return null;
        }


        public override ValueList ReadValuesList(int length)
        {
            return ReadValuesList(currentSample, length);
        }


        public override ValueList ReadValuesList(long pos, int length)
        {
            Value[] values = Read(pos, length);
            return convertValueArrayToValueList(values, false);
        }


        public override ValueList readValuesListScaled(int length)
        {
            return readValuesListScaled(currentSample, length);
        }


        public override ValueList readValuesListScaled(long pos, int length)
        {
            Value[] values = readScaled(pos, length);
            return convertValueArrayToValueList(values, true);
        }


        public override void resetPos()
        {
            currentSample = 0;
        }


        public override void close()
        {
            isOpened = false;
        }


        public override long getSampleCount()
        {
            //try
            //{
                expr = xpath.Compile(String.Format(Constants.VALUES_XML_READER_VALUES_PATH, 0, long.MaxValue));
                //valueNodes = (NodeList)expr.evaluate(document, XPathConstants.NODESET);
                valueNodes = xpath.Select(expr);
                return valueNodes.Count;
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
            //return 0;
        }

        private Value[] convertValueNodeListToValueByteArray(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                Byte[] data = new Byte[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = Byte.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueShortArray(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr,valueNodes) as XPathNodeIterator;
                short[] data = new short[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = short.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueIntArray(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                int[] data = new int[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = int.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueLongArray(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                long[] data = new long[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = long.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueFloatArray(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                float[] data = new float[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = float.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueDoubleArray(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                double[] data = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = double.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueByteArrayScaled(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                double[] data = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = double.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueShortArrayScaled(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                short[] data = new short[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = short.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueIntArrayScaled(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                int[] data = new int[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = int.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueLongArrayScaled(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                long[] data = new long[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = long.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueFloatArrayScaled(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                float[] data = new float[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = float.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private Value[] convertValueNodeListToValueDoubleArrayScaled(XPathNodeIterator valueNodes)
        {
            Value[] values = new Value[valueNodes.Count];
            expr = xpath.Compile(Constants.VALUES_XML_READER_VALUES_DATA_PATH);
            for (int i = 0; i < valueNodes.Count; i++)
            {
                valueNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, valueNodes) as XPathNodeIterator;
                double[] data = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    data[j] = double.Parse(datas.Current.Value);
                }
                values[i] = new Value(long.Parse(valueNodes.Current.GetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, "")), data);
            }
            return values;
        }

        private ValueList convertValueArrayToValueList(Value[] values, Boolean scaled)
        {
            ValueList valueList = new ValueList();
            long[] samplestamp = new long[values.Length];
            if (scaled)
            {
                double[][] valuesData = new double[values.Length][];
                for (int i = 0; i < values.Length; i++)
                {
                    samplestamp[i] = values[i].GetSamplestamp();
                    double[] valueData = (double[])values[i].GetData();
                    Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                }
                valueList.setSamplestamps(samplestamp);
                valueList.setData(valuesData);
                return valueList;
            }
            else
            {
                if (dataType == DataType.INT8)
                {
                    byte[][] valuesData = new byte[values.Length][];
                    for (int i = 0; i < values.Length; i++)
                    {
                        samplestamp[i] = values[i].GetSamplestamp();
                        byte[] valueData = (byte[])values[i].GetData();
                        Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                    }
                    valueList.setSamplestamps(samplestamp);
                    valueList.setData(valuesData);
                    return valueList;
                }
                if (dataType == DataType.INT16 || (dataType == DataType.UINT8))
                {
                    short[][] valuesData = new short[values.Length][];
                    for (int i = 0; i < values.Length; i++)
                    {
                        samplestamp[i] = values[i].GetSamplestamp();
                        short[] valueData = (short[])values[i].GetData();
                        Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                    }
                    valueList.setSamplestamps(samplestamp);
                    valueList.setData(valuesData);
                    return valueList;
                }
                if (dataType == DataType.INT32 || (dataType == DataType.UINT16))
                {
                    int[][] valuesData = new int[values.Length][];
                    for (int i = 0; i < values.Length; i++)
                    {
                        samplestamp[i] = values[i].GetSamplestamp();
                        int[] valueData = (int[])values[i].GetData();
                        Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                    }
                    valueList.setSamplestamps(samplestamp);
                    valueList.setData(valuesData);
                    return valueList;
                }
                if (dataType == DataType.UINT32)
                {
                    long[][] valuesData = new long[values.Length][];
                    for (int i = 0; i < values.Length; i++)
                    {
                        samplestamp[i] = values[i].GetSamplestamp();
                        long[] valueData = (long[])values[i].GetData();
                        Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                    }
                    valueList.setSamplestamps(samplestamp);
                    valueList.setData(valuesData);
                    return valueList;
                }
                if (dataType == DataType.FLOAT)
                {
                    float[][] valuesData = new float[values.Length][];
                    for (int i = 0; i < values.Length; i++)
                    {
                        samplestamp[i] = values[i].GetSamplestamp();
                        float[] valueData = (float[])values[i].GetData();
                        Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                    }
                    valueList.setSamplestamps(samplestamp);
                    valueList.setData(valuesData);
                    return valueList;
                }
                if (dataType == DataType.DOUBLE)
                {
                    double[][] valuesData = new double[values.Length][];
                    for (int i = 0; i < values.Length; i++)
                    {
                        samplestamp[i] = values[i].GetSamplestamp();
                        double[] valueData = (double[])values[i].GetData();
                        Array.Copy(valueData, 0, valuesData[i], 0, valueData.Length);
                    }
                    valueList.setSamplestamps(samplestamp);
                    valueList.setData(valuesData);
                    return valueList;
                }
            }
            return null;
        }
    }
}