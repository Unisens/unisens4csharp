
using System;
using System.Collections.Generic;
using System.Xml;
using org.unisens.ri.config;

namespace org.unisens.ri
{
    public class CustomFileFormatImpl : FileFormatImpl, CustomFileFormat
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        internal CustomFileFormatImpl(XmlNode customFileFormatNode, string fileFormatName)
            : base(customFileFormatNode, fileFormatName)
        {
            parse(customFileFormatNode);
        }

        internal CustomFileFormatImpl(CustomFileFormat customFileFormat)
            : base(customFileFormat)
        {
            attributes = customFileFormat.getAttributes();
            //foreach (var attr in customFileFormat.getAttributes())
            //{
            //    attributes.Add(attr.Key, attr.Value);
            //}
        }

        public CustomFileFormatImpl(string fileFormatName)
            : base(fileFormatName)
        {

        }

        private void parse(XmlNode customFileFormatNode)
        {
            var attrs = customFileFormatNode.Attributes;
            int length = attrs.Count;
            for (int i = 0; i < length; i++)
            {
                var attr = attrs.Item(i);
                string name = attr.Name;
                string value = attr.Value;
                if (!name.Equals(Constants.FILEFORMAT_COMMENT, StringComparison.CurrentCultureIgnoreCase)
                    && !name.Equals(Constants.CUSTOMFILEFORMAT_FILEFORMATNAME, StringComparison.CurrentCultureIgnoreCase))
                    attributes.Add(name, value);
            }
        }

        public Dictionary<string, string> getAttributes()
        {
            return attributes;
        }

        public void setAttributes(Dictionary<string, string> attributes)
        {
            this.attributes = attributes;
        }


        public override FileFormat clone()
        {
            return new CustomFileFormatImpl(this);
        }

    }
}