using System.Globalization;
using System.IO;
using System.Xml;
using org.unisens.ri.config;
using System;

namespace org.unisens.ri.io.xml
{
    public class ValuesXmlWriter : ValuesWriter
    {
        private XmlDocument document;
        private XmlElement root;
        private NumberFormatInfo decimalFormat;

        public ValuesXmlWriter(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {

            decimalFormat = new NumberFormatInfo { NumberGroupSizes = new int[] { 0 }, NumberDecimalSeparator = "." };
            //decimalFormat.setDecimalSeparatorAlwaysShown(false);
            open();
        }


        public override void open()
        {
            //try
            //{
                if (!isOpened)
                {
                    document = new XmlDocument();
                    isOpened = true;
                    if (File.Exists(absoluteFileName))
                    {
                        document.Load(absoluteFileName);
                        root = document.DocumentElement;
                    }
                    else
                    {
                        root = document.CreateElement(Constants.VALUES_XML_READER_VALUES_ELEMENT);
                        document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
                        document.AppendChild(root);
                    }
                }
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
        }


        public override void append(Value value)
        {
            open();
            //try
            //{
                if (dataType == DataType.INT8)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (sbyte[])value.GetData()));
                if (dataType == DataType.UINT8)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (byte[])value.GetData()));
                if (dataType == DataType.INT16)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (short[])value.GetData()));
                if (dataType == DataType.UINT16)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (UInt16[])value.GetData()));
                if (dataType == DataType.INT32)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (int[])value.GetData()));
                if (dataType == DataType.UINT32)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (UInt32[])value.GetData()));
                if (dataType == DataType.FLOAT)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (float[])value.GetData()));
                if (dataType == DataType.DOUBLE)
                    root.AppendChild(createValueElement(value.GetSamplestamp(), (double[])value.GetData()));

                document.Save(absoluteFileName);

                //XmlReader source = XmlReader.Create(File.Open(absoluteFileName, FileMode.OpenOrCreate));

                //var outFile = File.Open(absoluteFileName, FileMode.OpenOrCreate);
                //XmlWriter writer = XmlWriter.Create(outFile);

                //var transformer = new XslCompiledTransform();
                //transformer.OutputSettings.Indent = true;
                ////transformer.OutputSettings.OutputMethod = XmlOutputMethod.Xml;
                ////version
                //transformer.OutputSettings.OmitXmlDeclaration = false;
                ////transformer.OutputSettings..STANDALONE, "yes");

                //transformer.Transform(source, writer);
                //outFile.Close();
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
        }


        public override void append(Value[] values)
        {
            open();
            //try
            //{
                if (dataType == DataType.INT8)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (sbyte[])values[i].GetData()));
                if (dataType == DataType.UINT8)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (byte[])values[i].GetData()));
                if (dataType == DataType.INT16)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (short[])values[i].GetData()));
                if (dataType == DataType.UINT16)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (UInt16[])values[i].GetData()));
                if (dataType == DataType.INT32 || (dataType == DataType.UINT16))
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (int[])values[i].GetData()));
                if (dataType == DataType.UINT32)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (UInt32[])values[i].GetData()));
                if (dataType == DataType.FLOAT)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (float[])values[i].GetData()));
                if (dataType == DataType.DOUBLE)
                    for (int i = 0; i < values.Length; i++)
                        root.AppendChild(createValueElement(values[i].GetSamplestamp(), (double[])values[i].GetData()));

                document.Save(absoluteFileName);

                //XmlReader source = XmlReader.Create(File.Open(absoluteFileName, FileMode.OpenOrCreate));

                //var outFile = File.Open(absoluteFileName, FileMode.OpenOrCreate);
                //XmlWriter writer = XmlWriter.Create(outFile);

                //var transformer = new XslCompiledTransform();
                //transformer.OutputSettings.Indent = true;
                ////transformer.OutputSettings.OutputMethod = XmlOutputMethod.Xml;
                ////version
                //transformer.OutputSettings.OmitXmlDeclaration = false;
                ////transformer.OutputSettings..STANDALONE, "yes");

                //transformer.Transform(source, writer);
                //outFile.Close();
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
        }


        public override void appendValuesList(ValueList valueList)
        {
            open();
            //try
            //{
                if (dataType == DataType.INT8)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (sbyte[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.UINT8)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (byte[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.INT16)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (short[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.UINT16)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (UInt16[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.INT32)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (int[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.UINT32)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (UInt32[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.FLOAT)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (float[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }
                if (dataType == DataType.DOUBLE)
                {
                    long[] samplestamps = valueList.getSamplestamps();
                    var valuesDate = (double[][])valueList.getData();
                    for (int i = 0; i < valueList.getSamplestamps().Length; i++)
                        root.AppendChild(createValueElement(samplestamps[i], valuesDate[i]));
                }

                document.Save(absoluteFileName);
                //XmlReader source = XmlReader.Create(File.Open(absoluteFileName, FileMode.OpenOrCreate));

                //var outFile = File.Open(absoluteFileName, FileMode.OpenOrCreate);
                //XmlWriter writer = XmlWriter.Create(outFile);

                //var transformer = new XslCompiledTransform();
                //transformer.OutputSettings.Indent = true;
                ////transformer.OutputSettings.OutputMethod = XmlOutputMethod.Xml;
                ////version
                //transformer.OutputSettings.OmitXmlDeclaration = false;
                ////transformer.OutputSettings..STANDALONE, "yes");

                //transformer.Transform(source, writer);
                //outFile.Close();
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}

        }


        public override void close()
        {
            isOpened = false;
        }

        private XmlElement createValueElement(long samplestamp, byte[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }
        private XmlElement createValueElement(long samplestamp, sbyte[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }

        private XmlElement createValueElement(long samplestamp, short[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }

        private XmlElement createValueElement(long samplestamp, UInt16[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }
        private XmlElement createValueElement(long samplestamp, int[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }

        private XmlElement createValueElement(long samplestamp, UInt32[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }

        private XmlElement createValueElement(long samplestamp, float[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }

        private XmlElement createValueElement(long samplestamp, double[] data)
        {
            XmlElement valueElement = document.CreateElement(Constants.VALUES_XML_READER_VALUE_ELEMENT);
            valueElement.SetAttribute(Constants.VALUES_XML_READER_SAMPLESTAMP_ATTR, samplestamp.ToString(decimalFormat));
            for (int i = 0; i < data.Length; i++)
            {
                XmlNode dataNode = valueElement.AppendChild(document.CreateElement(Constants.VALUES_XML_READER_DATA_ELEMENT));
                dataNode.InnerText = data[i].ToString(decimalFormat);
            }
            return valueElement;
        }

    }
}