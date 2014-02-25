
using System;
using System.Collections.Generic;

namespace org.unisens
{

    /**
     * An Entry is a structural unit of a unisens dataset. Entry is the base class of the following Entries:
     * 	 
     * <ul>
     * <li>SignalEntry</li>
     * <li>ValuesEntry</li>
     * <li>EventEntry</li>
     * <li>CustomEntry</li>
     * </ul>
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface Entry 
    {


        /**
         * Gets the comment of this Entry.  
         * 
         * @return the comment
         */
        string getComment();

        /**
         * Gets the id of this Entry. The id is unique for all Entries in a unsiens dataset.
         *  
         * @return the id of this Entry
         */
        string getId();

        /**
         * Gets the source id of this Entry. The source id can be used to describe origin of data contained 
         * in an Entry. For example a device serial number or MAC adress.
         * 
         * @return the source id of this Entry
         */
        string getSourceId();

        /**
         * Gets the source of this Entry. The source can be used to comment the origin of data contained 
         * in an Entry. For example the name of the measurement device.
         * 
         * @return the source of this Entry
         */
        string getSource();

        /**
         * Gets an identifier of the content class of the data contained in this Entry. Recommended 
         * content classes are listed in the unisens documentation.
         * 
         * @return the content class of this Entry
         */
        string getContentClass();

        /**
         * Sets  the name of this Entry. 
         * 
         * @param name of the entry
         */
        void setName(String name);

        /**
         * Sets  the content class of the data contained in this Entry. Recommended 
         * content classes are listed in the unisens documentation.
         * 
         * @param theClass the content class
         */
        void setContentClass(string theClass);

        /**
         * Sets the comment of this Entry.  
         * 
         * @param comment the comment
         */
        void setComment(string comment);

        /**
         * Rename this Entry. Also the data file containing the assiciated data will be renamed.
         * 
         * @param newId the new id of this Entry
         * @throws IOException
         * @throws DuplicateIdException
         */
        void rename(string newId);

        /**
         * Sets the source id of this Entry. The source id can be used to describe origin of data contained 
         * in an Entry. For example a device serial number or MAC adress.	 
         *  
         * @param sourceId the source id of this Entry
         */
        void setSourceId(string sourceId);

        /**
         * Sets the source of this Entry. The source can be used to comment the origin of data contained 
         * in an Entry.
         * 
         * @param source the source of this Entry
         */
        void setSource(string source);

        /**
         * Returns the custom attributes of this unisens entry. Custom attributes can be used
         * to add simple context information as key/values pairs.  
         * 
         * @return all custom attribues as Dictionary
         */
        Dictionary<string, string> getCustomAttributes();

        /**
         * Add a ned custom attributes to this unisens entry. Custom attributes can be used
         * to add simple context information as key/values pairs.
         * 
         * @param key the key of the new attribute
         * @param value the value of the new attribute
         */
        void addCustomAttribute(string key, string value);

        /**
         * Gets the file format that is used to represet this Entry in the associated data file
         * 
         * @return the file format of this Entry
         */
        FileFormat getFileFormat();

        /**
         * Sets the file format that is used to represet this Entry in the associated data file
         * 
         * @param fileFormat the file format of this Entry
         */
        void setFileFormat(FileFormat fileFormat);

        /**
         * Gets the unisens object which contains this Entry 
         * 
         * @return the unisend object
         */
        Unisens getUnisens();

        /**
         * Set the unisens object which contains this Entry 
         * 
         */
        void setUnisens(Unisens unisens);


        /**
         * Closes the data file associated with this Entry
         */
        void close();

        T clone<T>() where T : Entry;

        /**
         *  
         * Creates a new BinFileFormat object. 
         * 
         * @return new BinFileFormat object
         * 
         */
        BinFileFormat createBinFileFormat();

        /**
         * Creates a new CsvFileFormat object. 
         * 
         * @return new CsvFileFormat object
         */
        CsvFileFormat createCsvFileFormat();

        /**
         * Creates a new XmlFileFormat object. 
         * 
         * @return new XmlFileFormat object
         */
        XmlFileFormat createXmlFileFormat();

        /**
         * Creates a new CustomFileFormat object. 
         * 
         * @param fileFormatName the name of the CustomFileFormat
         * @return new CustomFileFormat object
         */
        CustomFileFormat createCustomFileFormat(string fileFormatName);

    }
}