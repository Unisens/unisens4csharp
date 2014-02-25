using System.Collections.Generic;
namespace org.unisens
{
    /**
     * CustomFileFormat is used to define that a custom file format is used, that is not specified 
     * in the unisens specification. New custom file formats have to implement this interface. 
     * THIS FEATURE IS STILL EXPERIMENTAL AND MAY CHANGE IN FUTURE.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface CustomFileFormat : FileFormat
    {


        /**
         * Gets the attributes of this file format
         * 
         * @return all attributes od this file format
         */
        Dictionary<string, string> getAttributes();
    }
}
