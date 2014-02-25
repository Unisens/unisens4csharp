
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using org.unisens.ri.config;
using System.Globalization;

namespace org.unisens.ri.io.xml
{
    public class SignalXmlReader : SignalReader
    {
        private XmlDocument document;
        private XPathNavigator xpath;
        private XPathExpression expr;
        private XPathNodeIterator sampleNodes;

        public SignalXmlReader(SignalEntry signalEntry)
            : base(signalEntry)
        {
            open();
        }


        public override void open()
        {
            //try
            //{
                if (!isOpened)
                {
                    /*
                    DocumentBuilderFactory domFactory = DocumentBuilderFactory.newInstance();
                    domFactory.setNamespaceAware(true); // never forget this!
                    DocumentBuilder builder = domFactory.newDocumentBuilder();
                    document = builder.parse(absoluteFileName);

                    XPathFactory factory = XPathFactory.newInstance();
                    xpath = factory.newXPath();
                     * */
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


        public override Object Read(int length)
        {
            return read(length, false);
        }


        public override Object Read(long pos, int length)
        {
            return read(pos, length, false);
        }


        public override double[][] readScaled(int length)
        {
            return (double[][])read(length, true);
        }


        public override double[][] readScaled(long pos, int length)
        {
            return (double[][])read(pos, length, true);
        }

        private Object read(int length, Boolean scaled)
        {
            return read(currentSample, length, scaled);
        }


        private Object read(long pos, int length, Boolean scaled)
        {
            //try
            //{


                expr = xpath.Compile(String.Format(Constants.SIGNAL_XML_READER_SAMPLES_PATH, pos, pos + length + 1));
                sampleNodes = xpath.Select(expr);
                //sampleNodes = (NodeList)expr.evaluate(document, XPathConstants.NODESET);
                return convertSampleNodeListToArray(sampleNodes, scaled);
            //}
            //catch (XPathException e)
            //{
            //    e.printStackTrace();
            //}
            //return null;
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
                expr = xpath.Compile(String.Format(Constants.SIGNAL_XML_READER_SAMPLES_PATH, 0, long.MaxValue));
                sampleNodes = xpath.Select(expr);
                return sampleNodes.Count;
            //}
            //catch (XPathException e)
            //{
            //    e.printStackTrace();
            //}
            //return 0;
        }

        private Object convertSampleNodeListToArray(XPathNodeIterator sampleNodes, Boolean scaled)
        {
            if (scaled)
            {
                if (dataType == DataType.INT8)
                    return convertSampleNodeListToSByteArrayScaled(sampleNodes);
                if (dataType == DataType.UINT8)
                    return convertSampleNodeListToByteArrayScaled(sampleNodes);
                if (dataType == DataType.INT16)
                    return convertSampleNodeListToShortArrayScaled(sampleNodes);
                if (dataType == DataType.UINT16)
                    return convertSampleNodeListToUInt16ArrayScaled(sampleNodes);
                if (dataType == DataType.INT32)
                    return convertSampleNodeListToIntArrayScaled(sampleNodes);
                if (dataType == DataType.UINT32)
                    return convertSampleNodeListToUInt32ArrayScaled(sampleNodes);
                if (dataType == DataType.FLOAT)
                    return convertSampleNodeListToFloatArrayScaled(sampleNodes);
                if (dataType == DataType.DOUBLE)
                    return convertSampleNodeListToDoubleArrayScaled(sampleNodes);
            }
            else
            {
                if (dataType == DataType.INT8)
                    return convertSampleNodeListToSByteArray(sampleNodes);
                if (dataType == DataType.UINT8)
                    return convertSampleNodeListToByteArray(sampleNodes);
                if (dataType == DataType.INT16)
                    return convertSampleNodeListToShortArray(sampleNodes);
                if (dataType == DataType.UINT16)
                    return convertSampleNodeListToUInt16Array(sampleNodes);
                if (dataType == DataType.INT32)
                    return convertSampleNodeListToIntArray(sampleNodes);
                if (dataType == DataType.UINT32)
                    return convertSampleNodeListToUInt32Array(sampleNodes);
                if (dataType == DataType.FLOAT)
                    return convertSampleNodeListToFloatArray(sampleNodes);
                if (dataType == DataType.DOUBLE)
                    return convertSampleNodeListToDoubleArray(sampleNodes);
            }
            return null;
        }

        private Object convertSampleNodeListToByteArray(XPathNodeIterator sampleNodes)
        {
            byte[][] result = new byte[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            ;
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new byte[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = Byte.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private Object convertSampleNodeListToSByteArray(XPathNodeIterator sampleNodes)
        {
            sbyte[][] result = new sbyte[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            ;
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new sbyte[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = sbyte.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private Object convertSampleNodeListToShortArray(XPathNodeIterator sampleNodes)
        {
            short[][] result = new short[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new short[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = short.Parse(datas.Current.Value);
                }
             }
            return result;
        }

        private Object convertSampleNodeListToUInt16Array(XPathNodeIterator sampleNodes)
        {
            UInt16[][] result = new UInt16[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new UInt16[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = UInt16.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private Object convertSampleNodeListToIntArray(XPathNodeIterator sampleNodes)
        {
            int[][] result = new int[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new int[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = int.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private Object convertSampleNodeListToUInt32Array(XPathNodeIterator sampleNodes)
        {
            UInt32[][] result = new UInt32[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new UInt32[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = UInt32.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private Object convertSampleNodeListToFloatArray(XPathNodeIterator sampleNodes)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            float[][] result = new float[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new float[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = float.Parse(datas.Current.Value, cultureInfo);
                    //result[i][j] = Convert.ToSingle(datas.Current.Value);
                }
            }
            return result;
        }

        private Object convertSampleNodeListToDoubleArray(XPathNodeIterator sampleNodes)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < 2; j++)
                  //for (int j = 0; j < datas.Count; j++)
                    {
                    datas.MoveNext();
                    result[i][j] = Double.Parse(datas.Current.Value, cultureInfo);
//                    result[i][j] = 1.5;
                    //result[i][j] = datas.Current.ValueAsDouble;
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToByteArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = Byte.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToSByteArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = SByte.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToShortArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = (short.Parse(datas.Current.Value) + baseline) * lsbValue;
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToUInt16ArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = (UInt16.Parse(datas.Current.Value) + baseline) * lsbValue;
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToIntArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = int.Parse(datas.Current.Value);
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToUInt32ArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = (UInt32.Parse(datas.Current.Value) + baseline) * lsbValue;
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToFloatArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = (float.Parse(datas.Current.Value) + baseline) * lsbValue;
                }
            }
            return result;
        }

        private double[][] convertSampleNodeListToDoubleArrayScaled(XPathNodeIterator sampleNodes)
        {
            double[][] result = new double[sampleNodes.Count][];
            expr = xpath.Compile(Constants.SIGNAL_XML_READER_SAMPLES_DATA_PATH);
            for (int i = 0; i < sampleNodes.Count; i++)
            {
                sampleNodes.MoveNext();
                //NodeList datas = (NodeList)expr.evaluate(valueNodes.item(i), XPathConstants.NODESET);
                var datas = xpath.Evaluate(expr, sampleNodes) as XPathNodeIterator;
                result[i] = new double[datas.Count];
                for (int j = 0; j < datas.Count; j++)
                {
                    datas.MoveNext();
                    result[i][j] = (Double.Parse(datas.Current.Value) + baseline) * lsbValue;
                }
            }
            return result;
        }

    }
}