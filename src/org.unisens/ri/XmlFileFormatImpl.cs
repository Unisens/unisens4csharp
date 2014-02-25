
using System.Xml;

namespace org.unisens.ri
{
    public class XmlFileFormatImpl : FileFormatImpl, XmlFileFormat
    {
        internal XmlFileFormatImpl(XmlNode xmlFileFormatNode)
            : base(xmlFileFormatNode, "XML")
        {

        }

        internal XmlFileFormatImpl(XmlFileFormat xmlFileFormat)
            : base(xmlFileFormat)
        {
        }

        public XmlFileFormatImpl()
            : base("XML")
        {
        }

        public override FileFormat clone()
        {
            return new XmlFileFormatImpl(this);
        }
    }
}
