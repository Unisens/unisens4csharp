using System;
using System.Globalization;
using System.IO;
using System.Xml;
using org.unisens.ri.config;
using org.unisens.ri.util;

namespace org.unisens.ri.io.xml
{
    public class SignalXmlWriter : SignalWriter
    {
        private XmlDocument document;
        private XmlNode root;
        private NumberFormatInfo decimalFormat;

        public SignalXmlWriter(SignalEntry signalEntry)
            : base(signalEntry)
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
                        root = document.CreateElement(Constants.SIGNAL_XML_READER_SIGNAL_ELEMENT);
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


        public override void Append(Object data)
        {
            open();
            if (channelCount == 1)
                data = Utilities.convertFrom1DimTo2DimArray(data);
            //try
            //{
                if (dataType == DataType.INT8)
                    appendData((sbyte[][])data);
                if (dataType == DataType.UINT8)
                    appendData((byte[][])data);
                if (dataType == DataType.INT16)
                    appendData((short[][])data);
                if (dataType == DataType.UINT16)
                    appendData((UInt16[][])data);
                if (dataType == DataType.INT32) 
                    appendData((int[][])data);
                if (dataType == DataType.UINT32)
                    appendData((UInt32[][])data);
                if (dataType == DataType.FLOAT)
                    appendData((float[][])data);
                if (dataType == DataType.DOUBLE)
                    appendData((double[][])data);

                document.Save(absoluteFileName);
                //Stream stream = null;
                //document.Save(stream);
                //XmlReader source = XmlReader.Create(stream);

                //var outFile = File.Open(absoluteFileName, FileMode.OpenOrCreate);
                //XmlWriter writer = XmlWriter.Create(absoluteFileName, new XmlWriterSettings() {Indent = true,OmitXmlDeclaration = false});

                //var transformer = new XslCompiledTransform();
                //transformer.OutputSettings.Indent = true;
                //transformer.OutputSettings.OutputMethod = XmlOutputMethod.Xml;
                //version
                //transformer.OutputSettings.OmitXmlDeclaration = false;
                //transformer.OutputSettings..STANDALONE, "yes");

                //transformer.Load("http://www.unisens.org/unisens2.0/unisens.xsd", new XsltSettings(), new XmlUrlResolver());
                //transformer.Transform(document, writer);
                ////source.Close();
                //writer.Close();
                //outFile.Close();
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
        }
        

        private void appendData(SByte[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {

                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(byte[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {

                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(short[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {
                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(ushort[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {
                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(int[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {
                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(UInt32[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {
                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(float[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {
                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString(decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }

        private void appendData(double[][] data)
        {
            XmlElement sampleElement;
            XmlNode dataNode;
            for (int i = 0; i < data.Length; i++)
            {
                sampleElement = document.CreateElement(Constants.SIGNAL_XML_READER_SAMPLE_ELEMENT);
                for (int j = 0; j < data[i].Length; j++)
                {
                    dataNode = sampleElement.AppendChild(document.CreateElement(Constants.SIGNAL_XML_READER_DATA_ELEMENT));
                    dataNode.InnerText = data[i][j].ToString("R",decimalFormat);
                }
                root.AppendChild(sampleElement);
            }
        }


        public override void close()
        {
            isOpened = false;
        }

    }
}