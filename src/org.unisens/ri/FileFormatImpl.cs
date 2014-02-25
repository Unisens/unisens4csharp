
using System;
using System.Xml;
using org.unisens.ri.config;

namespace org.unisens.ri
{
    public abstract class FileFormatImpl : FileFormat
    {
        private string comment = null;
        private string fileFormatName = null;

        internal FileFormatImpl(XmlNode fileFormatNode, string fileFormatName)
        {
            this.fileFormatName = fileFormatName;
            parse(fileFormatNode);
        }

        private void parse(XmlNode fileFormatNode)
        {
            var attrNode = fileFormatNode.Attributes.GetNamedItem(Constants.FILEFORMAT_COMMENT);
            comment = (attrNode != null) ? attrNode.Value : null;
        }

        internal FileFormatImpl(string fileFormatName)
        {
            this.fileFormatName = fileFormatName;
        }
        internal FileFormatImpl(FileFormat fileFormat)
        {
            fileFormatName = fileFormat.getFileFormatName();
            comment = fileFormat.getComment();
        }

        public string getComment()
        {
            return comment;
        }

        public string getFileFormatName()
        {
            return fileFormatName;
        }

        public void setComment(string comment)
        {
            this.comment = comment;
        }

        public abstract FileFormat clone();

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
