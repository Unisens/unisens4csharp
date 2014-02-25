using System.Collections.Generic;
namespace org.unisens
{
    /**
     * EventEntry represents an Entry containing a list of Events. Events have no signal 
     * or value data. Each Event has a timstamp and a type and can have a comment. Type
     * and comment are strings.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface EventEntry : TimedEntry
    {


        /**
         * Appends an Event to this EventEntry
         * 
         * @param event the Event to append
         */
        void append(Event e);

        /**
         * Reads Events from a data file beginning at a given postion
         * 
         * @param position the position to start from
         * @param length the number of Events to read
         * @return a List of Events
         * @throws IOException
         */
        List<Event> read(long position, int length);

        /**
         * Reads Events from a data file beginning at the current position of the file pointer
         * 
         * @param length the numer of Events to read
         * @return a List of Events
         * @throws IOException
         */
        List<Event> read(int length);

        /**
         * Deletes all Events from the data file.
         * 
         * @throws IOException
         */
        void empty();


        /**
         * Appends a List of Events to this EventEntry
         * 
         * @param events
         * @throws IOException
         */
        void append(List<Event> events);

        /**
         * Gets the comment length of this EventEntry. This 
         * value defines the maximum allowed length for the 
         * comment field
         * 
         * @return the comment length
         */
        int getCommentLength();

        /**
         * Gets the type length of this EventEntry. This 
         * value defines the maximum allowed length for the 
         * type field
         * 
         * @return the type length
         */
        int getTypeLength();

        /**
         * Sets the type length of this EventEntry. This 
         * value defines the maximum allowed length for the 
         * type field
         * 
         * @param typeLength the type length
         */
        void setTypeLength(int typeLength);

        /**
         * Sets the comment length of this EventEntry. This 
         * value defines the maximum allowed length for the 
         * comment field
         * 
         * @param commentLength the comment length
         */
        void setCommentLength(int commentLength);
    }
}