

/**
 * The Context of a unisens dataset can contain additional information about the dataset. The 
 * information is stored in the context.xml file. The structure of the xml file is described with
 * a XML schema.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
using System;

namespace org.unisens
{
    public interface Context
    {

        /** 
         * Gets the URL of the xml schema describung the structure of the context.xml file
         * 
         * @return the URL of the xml schema
         */
        String getSchemaUrl();

        /** 
         * Sets the URL of the xml schema describung the structure of the context.xml file
         * 
         * @param url the URL of the xml schema
         */
        void setSchemaUrl(String url);
    }
}