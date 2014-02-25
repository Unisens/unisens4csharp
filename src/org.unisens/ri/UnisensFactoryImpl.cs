
namespace org.unisens.ri
{
    public class UnisensFactoryImpl : UnisensFactory
    {

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

        public Unisens createUnisens(string path)
        {
            return new UnisensImpl(path);
        }

        public XmlFileFormat createXmlFileFormat()
        {
            return new XmlFileFormatImpl();
        }

    }
}