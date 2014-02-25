namespace org.unisens
{
    /**
     * A ValuesEntry represents data acquired at certain points in time. Points in time are
     * determined by sample number using a specific sampleRate.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */

    public interface ValuesEntry : MeasurementEntry
    {

        /**
         * Reads from the data file beginning at a given postion and presents the data as Array of Value
         * 
         * @param pos the position/row to start from
         * @param length number of rows to read
         * @return an Array of Value objects
         * @
         */
        Value[] read(long pos, int length);

        /**
         * Reads from the data file beginning at the current postion of the file pointer and presents 
         * the data as Array of Value
         * 
         * @param length number of rows to read
         * @return an Array of Value objects
         * @
         */
        Value[] read(int length);

        /**
         * Appends a Value object to this ValueEntry.
         * @param data the Value to append
         * @throws IllegalArgumentException
         * @
         */
        void append(Value data);


        /**
         * Appends an Array of Values object to this ValueEntry
         * @param data the Values to append
         * @throws IllegalArgumentException
         * @
         */
        void append(Value[] data);

        /**
         * Appends the Values contained in a ValuesList object to this ValueEntry
         * @param valueList the ValuesList to append
         * @
         * @throws IllegalArgumentException
         */
        void appendValuesList(ValueList valueList);

        /**
         * Reads from the data file beginning at the current postion of the file pointer and presents 
         * the data as a ValuesList object
         * 
         * @param length number of rows to read
         * @return a ValuesList
         * @
         */
        ValueList readValuesList(int length);

        /**
         * Reads from the data file beginning at a given postion and presents the data as a ValuesList object
         * 
         * @param pos the position/row to start from
         * @param length number of rows to read
         * @return a ValuesList
         * @
         */
        ValueList readValuesList(long pos, int length);
    }
}