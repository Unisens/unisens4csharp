using System;

namespace org.unisens
{

    /**
     * A SignalEntry represents continuously sampled data with a fixed sample rate.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface SignalEntry : MeasurementEntry
    {

        /**
         * Reads rows of data from a data file beginning at a given postion. The data is returned 
         * as object [length][], where channelCount is the number of channels in this SignalEntry
         * 
         * @param pos the position to start from
         * @param length the number of rows to read
         * @return the data as object [length][]
         * @
         */
        Object read(long pos, int length);

        /**
         * Reads data from a data file beginning at the current position of the file pointer. The data 
         * is returned as object [length][], where channelCount is the number of channels in 
         * this SignalEntry
         * 
         * @param length the number of rows to read
         * @return the data as object [length][]
         * @
         */
        Object read(int length);

        /**
         * Appends rows of data at the end of this SignalEntry.
         * 
         * @param data the data to add as object [length][]
         * @
         * @throws IllegalArgumentException
         */
        void append(Object data);
    }
}