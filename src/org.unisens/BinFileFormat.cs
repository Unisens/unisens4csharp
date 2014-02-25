

/**
 * BinFileFormat is used to define that a binary representation of Entry data is used as file format.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
namespace org.unisens
{
    public interface BinFileFormat : FileFormat
    {

        /**
         * Gets the Endianess of this BinFileFormat. LITTLE Endianess means least significant byte first. 
         * The default Endianess is LITTLE.
         * 
         * @return the Endianess
         */
        Endianess getEndianess();

        /**
         * Sets the Endianess of this BinFileFormat. LITTLE Endianess means least significant byte first.
         * The default Endianess is LITTLE.
         * 
         * @param endianess the Endianess
         */
        void setEndianess(Endianess endianess);
    }
}