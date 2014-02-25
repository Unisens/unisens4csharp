using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using org.unisens.ri.config;


namespace org.unisens.ri.io.xml
{
    public class EventXmlWriter : EventWriter
    {
        private XmlDocument document;
        private XmlElement root;
        private NumberFormatInfo decimalFormat;

        public EventXmlWriter(EventEntry eventEntry)
            : base(eventEntry)
        {
            decimalFormat = new NumberFormatInfo
            {
                NumberGroupSizes = new int[] { 0 },
                NumberDecimalSeparator = "."
            };
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
                        root = document.DocumentElement;//.GetElementsByTagName(Constants.EVENT_XML_READER_EVENTS_ELEMENT);
                    }
                    else
                    {
                        root = document.CreateElement(Constants.EVENT_XML_READER_EVENTS_ELEMENT);
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


        public override void append(Event newEvent)
        {
            //try
            //{
                open();
                var eventElement = document.CreateElement(Constants.EVENT_XML_READER_EVENT_ELEMENT);
                eventElement.SetAttribute(Constants.EVENT_XML_READER_SAMPLESTAMP_ATTR, newEvent.getSamplestamp().ToString(decimalFormat));
                eventElement.SetAttribute(Constants.EVENT_XML_READER_TYPE_ATTR, newEvent.getType());
                if (newEvent.getComment() != "")
                    eventElement.SetAttribute(Constants.EVENT_XML_READER_COMMENT_ATTR, newEvent.getComment());
                root.AppendChild(eventElement);
                document.Save(absoluteFileName);
                //XmlReader source = XmlReader.Create(File.Open(absoluteFileName,FileMode.OpenOrCreate));

                //var outFile = File.Open(absoluteFileName,FileMode.OpenOrCreate);
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
            //catch (ArgumentNullException e)
            //{
            //    e.printStackTrace();
            //}
            //catch (XsltException e)
            //{
            //    e.printStackTrace();
            //}
        }


        public override void append(List<Event> events)
        {
            //try
            //{
                open();
                foreach (Event newEvent in events)
                {
                    var eventElement = document.CreateElement(Constants.EVENT_XML_READER_EVENT_ELEMENT);
                    eventElement.SetAttribute(Constants.EVENT_XML_READER_SAMPLESTAMP_ATTR, newEvent.getSamplestamp().ToString(decimalFormat));
                    eventElement.SetAttribute(Constants.EVENT_XML_READER_TYPE_ATTR, newEvent.getType());
                    if (newEvent.getComment() != "")
                        eventElement.SetAttribute(Constants.EVENT_XML_READER_COMMENT_ATTR, newEvent.getComment());
                    root.AppendChild(eventElement);
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
            //catch (ArgumentNullException e)
            //{
            //    e.printStackTrace();
            //}
            //catch (XsltException e)
            //{
            //    e.printStackTrace();
            //}
        }


        public override void empty()
        {
            File.Delete(absoluteFileName);
            File.Create(absoluteFileName);
            isOpened = false;
        }


        public override void close()
        {
            isOpened = false;
        }
    }
}