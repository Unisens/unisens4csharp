namespace org.unisens
{


    /**
     * UnisensFactory is used to create all Objects of a unisens implementation, that have 
     * to be created directly (i.e not by a Unisens object).  
     *  
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface UnisensFactory
    {

        /**
         * Creates a new Unisens object. If path points to an existing unisens dataset, it is 
         * loaded. Else new unisens dataset is created.
         * 
         * @param path path to existing or new unisens dataset
         * @return Unisens object
         */
        Unisens createUnisens(string path);

        /**
         *  
         * Creates a new BinFileFormat object. 
         * 
         * @return new BinFileFormat object
         * 
         * @see org.unisens.Entry
         * @deprecated
         */
        BinFileFormat createBinFileFormat();

        /**
         * Creates a new CsvFileFormat object. 
         * 
         * @return new CsvFileFormat object
         * @see org.unisens.Entry
         * @deprecated
         */
        CsvFileFormat createCsvFileFormat();

        /**
         * Creates a new XmlFileFormat object. 
         * 
         * @return new XmlFileFormat object
         * @see org.unisens.Entry
         * @deprecated
         */
        XmlFileFormat createXmlFileFormat();

        /**
         * Creates a new CustomFileFormat object. 
         * 
         * @param fileFormatName the name of the CustomFileFormat
         * @return new CustomFileFormat object
         * @see org.unisens.Entry
         * @deprecated
         */
        CustomFileFormat createCustomFileFormat(string fileFormatName);
    }
}