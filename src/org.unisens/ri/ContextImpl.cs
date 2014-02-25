
using System.Xml;
using org.unisens.ri.config;
namespace org.unisens.ri
{
    public class ContextImpl : Context
    {
        private string schemaUrl = null;

        internal ContextImpl(XmlNode contextNode)
        {
            parse(contextNode);
        }

        internal ContextImpl(string schemaUrl)
        {
            this.schemaUrl = schemaUrl;
        }

        private void parse(XmlNode contextNode)
        {
            var attrs = contextNode.Attributes;
            var attrNode = attrs.GetNamedItem(Constants.CONTEXT_SCHEMAURL);
            schemaUrl = (attrNode != null) ? attrNode.Value : null;
        }

        internal XmlElement createElement(XmlDocument document)
        {
            var context = document.CreateElement(Constants.CONTEXT);
            if (getSchemaUrl() != null)
                context.SetAttribute(Constants.CONTEXT_SCHEMAURL, getSchemaUrl());

            return context;
        }

        public string getSchemaUrl()
        {
            return schemaUrl;
        }
        public void setSchemaUrl(string schemaUrl)
        {
            this.schemaUrl = schemaUrl;
        }

    }
}
