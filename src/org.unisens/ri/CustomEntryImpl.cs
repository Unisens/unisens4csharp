using System;
using System.Collections.Generic;
using System.Xml;
using org.unisens.ri.config;
namespace org.unisens.ri
{
    public class CustomEntryImpl : EntryImpl, CustomEntry
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        internal CustomEntryImpl(Unisens unisens, XmlNode entryNode)
            : base(unisens, entryNode)
        {
            parse(entryNode);
        }

        internal CustomEntryImpl(Unisens unisens, string id)
            : base(unisens, id)
        {
            fileFormat = new CustomFileFormatImpl("custom");
        }

        internal CustomEntryImpl(CustomEntry customEntry)
            : base(customEntry)
        {
            attributes = new Dictionary<string, string>(customEntry.getAttributes());
        }

        private void parse(XmlNode undefNode)
        {
            var attrs = undefNode.Attributes;
            int lenght = attrs.Count;
            XmlNode attr;
            string name;
            string value;
            for (int i = 0; i < lenght; i++)
            {
                attr = attrs.Item(i);
                name = attr.Name;
                value = attr.Value;
                if (!name.Equals(Constants.ENTRY_ID, StringComparison.CurrentCultureIgnoreCase)
                    && !name.Equals(Constants.ENTRY_COMMENT, StringComparison.CurrentCultureIgnoreCase)
                    && !name.Equals(Constants.ENTRY_SOURCE, StringComparison.CurrentCultureIgnoreCase)
                    && !name.Equals(Constants.ENTRY_SOURCE_ID, StringComparison.CurrentCultureIgnoreCase)
                    && !name.Equals(Constants.ENTRY_CONTENTCLASS, StringComparison.CurrentCultureIgnoreCase))
                    attributes.Add(name, value);
            }
        }


        public string getAttribute(string attributeName)
        {
            if (attributes.ContainsKey(attributeName))
                return attributes[attributeName];
            return null;
        }

        public Dictionary<string, string> getAttributes()
        {
            return attributes;
        }

        public void setAttribute(string attributeName, string attributeValue)
        {
            attributes.Add(attributeName, attributeValue);
        }

        internal override T getReader<T>()
        {
            return null;
        }

        internal override T getWriter<T>()
        {
            return null;
        }

        internal override bool isReaderOpened()
        {
            return false;
        }

        internal override bool isWriterOpened()
        {
            return false;
        }

        public override T clone<T>()
        {
            return (T)(object)new CustomEntryImpl(this);
        }
    }
}