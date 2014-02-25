

/**
 * TimedEntry is the superclass of MeasurementEntry and EventEntry and defines 
 * their common methods.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
namespace org.unisens
{
    public interface TimedEntry : Entry
    {

        /**
         * Sets the sample rate of this Entry in samples per second.
         * 
         * @param sampleRate the sample rate
         */
        void setSampleRate(double sampleRate);

        /**
         * Gets the sample rate of this Entry in samples per second.
         * 
         * @return the sample rate
         */
        double getSampleRate();

        /**
         * Return the number of items or rows in this Entry.
         * 
         * @return the number of items or rows in this Entry
         */
        long getCount();
    }
}