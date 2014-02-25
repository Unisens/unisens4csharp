using System;

namespace org.unisens
{


    /**
     * FileFormat is used to define which representation of Entry data is used 
     * as file format. 
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface FileFormat : ICloneable
    {

        /**
         * Gets the comment for this file format. 
         * 
         * @return the comment for this file format
         */
        string getComment();

        /**
         * Gets the name of this file format as string.
         * 
         * @return the name of this file format
         */
        string getFileFormatName();

        /**
         * Sets the comment for this file format.
         * 
         * @param comment the comment for this file format
         */
        void setComment(string comment);

        FileFormat clone();
    }
}