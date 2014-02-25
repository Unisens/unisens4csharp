using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using org.unisens.ri.config;
using org.unisens.ri.io;
namespace org.unisens.ri
{
    /**
     * @author Radoslav Nedkov
     * @email radi.nedkov@gmail.com
     *
     * 25.02.2007
     */
    public abstract class EntryImpl : Entry
    {
        internal Unisens unisens = null;
        internal FileFormat fileFormat = null;
        private string id = null;
        private String name = null;
        private string contentClass = null;
        private string source = null;
        private string sourceId = null;
        private string comment = null;

        private Dictionary<string, string> customAttributes = new Dictionary<string, string>();

        internal EntryImpl(Unisens unisens, XmlNode entryNode)
        {
            parse(entryNode);
            this.unisens = unisens;
        }

        public EntryImpl(Unisens unisens, string id)
        {
            this.unisens = unisens;
            this.id = id;
        }

        internal EntryImpl(Entry entry)
        {
            unisens = entry.getUnisens();
            id = entry.getId();
            contentClass = entry.getContentClass();
            source = entry.getSource();
            sourceId = entry.getSourceId();
            comment = entry.getComment();
            fileFormat = entry.getFileFormat().clone();
        }

        internal abstract T getReader<T>() where T : AbstractReader;
        internal abstract T getWriter<T>() where T : AbstractWriter;
        internal abstract bool isReaderOpened();
        internal abstract bool isWriterOpened();

        private void parse(XmlNode entryNode)
        {
            var attrs = entryNode.Attributes;
            var attrNode = attrs.GetNamedItem(Constants.ENTRY_ID);
            id = (attrNode != null) ? attrNode.Value : null;
            attrNode = attrs.GetNamedItem(Constants.ENTRY_SOURCE);
            source = (attrNode != null) ? attrNode.Value : null;
            attrNode = attrs.GetNamedItem(Constants.ENTRY_SOURCE_ID);
            sourceId = (attrNode != null) ? attrNode.Value : null;
            attrNode = attrs.GetNamedItem(Constants.ENTRY_CONTENTCLASS);
            contentClass = (attrNode != null) ? attrNode.Value : null;
            attrNode = attrs.GetNamedItem(Constants.ENTRY_COMMENT);
            comment = (attrNode != null) ? attrNode.Value : null;


            var childNodes = entryNode.ChildNodes;
            XmlNode childNode = null;

            for (int i = 0; i < childNodes.Count; i++)
            {
                childNode = childNodes.Item(i);
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    string nodeName = childNode.Name;
                    if (nodeName.Equals(Constants.BINFILEFORMAT, StringComparison.CurrentCultureIgnoreCase))
                        fileFormat = new BinFileFormatImpl(childNode);
                    if (nodeName.Equals(Constants.CSVFILEFORMAT, StringComparison.CurrentCultureIgnoreCase))
                        fileFormat = new CsvFileFormatImpl(childNode);
                    if (nodeName.Equals(Constants.XMLFILEFORMAT, StringComparison.CurrentCultureIgnoreCase))
                        fileFormat = new XmlFileFormatImpl(childNode);
                    if (nodeName.Equals(Constants.CUSTOMFILEFORMAT, StringComparison.CurrentCultureIgnoreCase))
                    {
                        attrNode = childNode.Attributes.GetNamedItem(Constants.CUSTOMFILEFORMAT_FILEFORMATNAME);
                        string fileFormatName = (attrNode != null) ? attrNode.Value : "CST";
                        fileFormat = new CustomFileFormatImpl(childNode, fileFormatName);
                    }
                    if (nodeName.Equals(Constants.CUSTOM_ATTRIBUTES, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var customAttributeNodes = childNode.ChildNodes;
                        for (int j = 0; j < customAttributeNodes.Count; j++)
                        {
                            var customAttributeNode = customAttributeNodes.Item(j);
                            if (customAttributeNode.Name.Equals(Constants.CUSTOM_ATTRIBUTE, StringComparison.CurrentCultureIgnoreCase))
                            {
                                attrs = customAttributeNode.Attributes;
                                var keyNode = attrs.GetNamedItem(Constants.CUSTOM_ATTRIBUTE_KEY);
                                string key = "";
                                if (keyNode != null)
                                {
                                    key = keyNode.Value;
                                }
                                var valueNode = attrs.GetNamedItem(Constants.CUSTOM_ATTRIBUTE_VALUE);
                                string value = "";
                                if (valueNode != null)
                                {
                                    value = valueNode.Value;
                                }
                                if (key != "")
                                    customAttributes.Add(key, value);
                            }
                        }
                    }
                }
            }
            return;
        }

        public Unisens getUnisens()
        {
            return unisens;
        }

        public void setUnisens(Unisens unisens)
        {
            this.unisens = unisens;
        }

        public string getContentClass()
        {
            return contentClass;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public void setContentClass(string contentClass)
        {
            this.contentClass = contentClass;
        }
        public string getComment()
        {
            return comment;
        }
        public void setComment(string comment)
        {
            this.comment = comment;
        }
        public string getId()
        {
            return id;
        }
        public void setId(string id)
        {
            this.id = id;
        }
        public string getSourceId()
        {
            return sourceId;
        }
        public void setSourceId(string sourceId)
        {
            this.sourceId = sourceId;
        }
        public string getSource()
        {
            return source;
        }
        public void setSource(string source)
        {
            this.source = source;
        }

        public Dictionary<string, string> getCustomAttributes()
        {
            return customAttributes;
        }

        public void addCustomAttribute(string key, string value)
        {
            customAttributes.Add(key, value);
        }

        public FileFormat getFileFormat()
        {
            return fileFormat;
        }
        public void setFileFormat(FileFormat fileFormat)
        {
            this.fileFormat = fileFormat.clone();
        }

        public void rename(string newId)
        {
            if (unisens.getEntry(newId) != null)
                throw new DuplicateIdException("Dublicate group id : " + newId);
            else
            {
                string fileFrom = unisens.getPath() + getId();
                string fileTo = unisens.getPath() + newId;

                if (File.Exists(fileFrom))
                {
                    File.Move(fileFrom, fileTo);
                }
                setId(newId);
            }
        }

        public void close()
        {

            if (isReaderOpened())
                getReader<AbstractReader>().close();
            if (isWriterOpened())
                getWriter<AbstractWriter>().close();

        }

        public BinFileFormat createBinFileFormat()
        {
            return new BinFileFormatImpl();
        }

        public CustomFileFormat createCustomFileFormat(string fileFormatName)
        {
            return new CustomFileFormatImpl(fileFormatName);
        }

        public CsvFileFormat createCsvFileFormat()
        {
            return new CsvFileFormatImpl();
        }

        public XmlFileFormat createXmlFileFormat()
        {
            return new XmlFileFormatImpl();
        }

        public abstract T clone<T>() where T : Entry;
    }
}