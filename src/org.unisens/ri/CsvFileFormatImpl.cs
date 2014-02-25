
using System.Xml;
using org.unisens.ri.config;

namespace org.unisens.ri
{
    public class CsvFileFormatImpl : FileFormatImpl, CsvFileFormat
    {
        private string separator = ";";
        private string decimalSeparator = ".";

        internal CsvFileFormatImpl(XmlNode csvFileFormatNode)
            : base(csvFileFormatNode, "CSV")
        {
            parse(csvFileFormatNode);
        }

        internal CsvFileFormatImpl(CsvFileFormatImpl csvFileFormat)
            : base(csvFileFormat)
        {
            separator = csvFileFormat.getSeparator();
            decimalSeparator = csvFileFormat.getDecimalSeparator();
        }

        public CsvFileFormatImpl()
            : base("CSV")
        {
        }

        private void parse(XmlNode csvFileFormatNode)
        {
            var attrNode = csvFileFormatNode.Attributes.GetNamedItem(Constants.CSVFILEFORMAT_SEPARATOR);
            separator = (attrNode != null) ? attrNode.Value : ";";
            attrNode = csvFileFormatNode.Attributes.GetNamedItem(Constants.CSVFILEFORMAT_DECIMAL_SEPARATOR);
            decimalSeparator = (attrNode != null) ? attrNode.Value : ".";
        }

        public string getSeparator()
        {
            return separator;
        }

        public void setSeparator(string separator)
        {
            this.separator = separator;
        }


        public override FileFormat clone()
        {
            return new CsvFileFormatImpl(this);
        }

        public string getDecimalSeparator()
        {
            return decimalSeparator;
        }

        public void setDecimalSeparator(string decimalSeparator)
        {
            this.decimalSeparator = decimalSeparator;
        }


    }
}