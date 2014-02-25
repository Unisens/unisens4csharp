

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using org.unisens.ri.config;

namespace org.unisens.ri.io.xml
{
    public class EventXmlReader : EventReader
    {
        private XmlDocument document;
        private XPathNavigator xpath;
        private XPathExpression expr;
        private XPathNodeIterator eventNodes;

        public EventXmlReader(EventEntry eventEntry)
            : base(eventEntry)
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
                    document = new XmlDocument();
                    document.Load(absoluteFileName);

                    //XPathFactory factory = XPathFactory.newInstance();
                    //xpath = factory.newXPath();
                    var doc = new XPathDocument(File.OpenRead(absoluteFileName));
                    xpath = doc.CreateNavigator();
                }
            //}
            //catch (IOException e)
            //{
            //    e.printStackTrace();
            //}
            //catch (XmlException e)
            //{
            //    e.printStackTrace();
            //}
        }


        public override long getSampleCount()
        {
            //try
            //{
                expr = xpath.Compile(String.Format(Constants.EVENT_XML_READER_EVENTS_PATH, 0, long.MaxValue));
                eventNodes = xpath.Select(expr);
                return eventNodes.Count;
            //}
            //catch (XPathException e)
            //{
            //    e.printStackTrace();
            //}
            //return 0;
        }


        public override void close()
        {
            isOpened = false;
        }


        public override List<Event> read(int length)
        {
            return read(currentSample, length);
        }


        public override List<Event> read(long position, int length)
        {
            //try
            //{
                List<Event> events = new List<Event>();
                expr = xpath.Compile(String.Format(Constants.EVENT_XML_READER_EVENTS_PATH, position, position + length + 1));
                eventNodes = xpath.Select(expr);
                //eventNodes = (NodeList)expr.evaluate(document, XPathConstants.NODESET);

                while(eventNodes.MoveNext())
                {
                    var eventNode = eventNodes.Current;
                    var attrNode = eventNode.GetAttribute(Constants.EVENT_XML_READER_SAMPLESTAMP_ATTR, "");
                    long samplestamp = Convert.ToInt64(string.IsNullOrEmpty(attrNode) ? "0" : attrNode);
                    attrNode = eventNode.GetAttribute(Constants.EVENT_XML_READER_TYPE_ATTR, "");
                    String type = attrNode ?? "";
                    attrNode = eventNode.GetAttribute(Constants.EVENT_XML_READER_COMMENT_ATTR, "");
                    String comment = attrNode ?? "";
                    events.Add(new Event(samplestamp, type, comment));
                }
                return events;
            //}
            //catch (XPathException e)
            //{
            //    e.printStackTrace();
            //}
            //return null;
        }

    }
}