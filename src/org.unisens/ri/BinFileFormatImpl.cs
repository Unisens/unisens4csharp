using System;
using System.Xml;
using org.unisens.ri.config;

namespace org.unisens.ri
{
    public class BinFileFormatImpl : FileFormatImpl, BinFileFormat
    {
        private Endianess _endianess = Endianess.LITTLE;

        internal BinFileFormatImpl(XmlNode binFileFormatNode)
            : base(binFileFormatNode, "BIN")
        {

            parse(binFileFormatNode);
        }

        internal BinFileFormatImpl(BinFileFormat binFileFormat)
            : base(binFileFormat)
        {

            _endianess = binFileFormat.getEndianess();
        }

        public BinFileFormatImpl()
            : base("BIN")
        {

        }

        private void parse(XmlNode binFileFormatNode)
        {
            XmlNode attrNode = binFileFormatNode.Attributes.GetNamedItem(Constants.BINFILEFORMAT_ENDIANESS);
            if (attrNode == null) return;
            _endianess = (Endianess)Enum.Parse(typeof(Endianess), attrNode.Value);
        }

        public Endianess getEndianess()
        {
            return _endianess;
        }

        public void setEndianess(Endianess endianess)
        {
            _endianess = endianess;
        }

        public override FileFormat clone()
        {
            return new BinFileFormatImpl(this);
        }
    }
}
