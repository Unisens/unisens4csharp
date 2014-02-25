
/**
 * CsvFileFormat is used to define that a caracter separeted value representation of Entry data is used 
 * as file format. 
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
using System;

namespace org.unisens
{
    public interface CsvFileFormat : FileFormat
    {

        /**
         * Gets the separator character used in the csv format. The default separator is ';'.
         * 
         * @return the separtor character
         */
        String getSeparator();

        /**
         * Sets the separator character used in the csv format. The default separator is ';'.
         * 
         * @param separator the separtor character
         */
        void setSeparator(String separator);

        /**
         * Gets the decimal separator character used in the csv format. The default separator is '.'.
         * 
         * @return the decimal separtor character
         */
        String getDecimalSeparator();

        /**
         * Sets the deciaml separator character used in the csv format. The default separator is '.'.
         * 
         * @param decimalSeparator the decimal separtor character
         */
        void setDecimalSeparator(String decimalSeparator);
    }
}