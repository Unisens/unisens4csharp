using System;
using System.Collections.Generic;

/**
 * CustomEntry can be used to describe a data file in a unisens dataset that is not 
 * specified in the unisens specification. Attributes can be used. These are stored 
 * and retreived from the unisens.xml file. THIS FEATURE IS STILL EXPERIMENTAL AND
 * MAY CHANGE IN FUTURE.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
namespace org.unisens
{
    public interface CustomEntry : Entry
    {

        /**
         * Gets the value of an attribute by its name
         * 
         * @param attributeName the name of the attribute
         * @return the value of the attribute
         */
        String getAttribute(String attributeName);

        /**
         * Sets a new attriubte and its values
         * 
         * @param attributeName the name of the attribute
         * @param attributeValue the value of the attriute
         */
        void setAttribute(String attributeName, String attributeValue);

        /**
         * Gets all attributes as a HashMap
         * 
         * @return all attributes
         */
        Dictionary<String, String> getAttributes();
    }
}