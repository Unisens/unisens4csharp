using System;

namespace org.unisens
{

    /**
     * An Event happens at a cetain point in time and be of a certain type. It can have a comment. Events
     * have no signal or value data.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public class Event : ICloneable
    {
        private long samplestamp;
        private string type = null;
        private string comment = null;

        /**
         * Gets the comment of this Event
         * 
         * @return the comment
         */
        public string getComment()
        {
            return comment;
        }

        /**
         * Sets the comment for this Event. The maximum comment length is defined in the 
         * EventEntry. If the comment is too long it will be truncated.
         * 
         * @param comment
         */
        public void setComment(string comment)
        {
            this.comment = comment;
        }

        /**
         * Gets the samplestamp of this Event. The samplestamp is given in sample counts. 
         * The real time offset can be calculated using the sampleRate of the EventEntry.
         * 
         * @return the samplestamp in sample counts 
         */
        public long getSamplestamp()
        {
            return samplestamp;
        }

        /**
         * Sets the samplestamp of this Event. The samplestamp is given in sample counts. The real time 
         * offset can be calculated using the sampleRate of the EventEntry
         * 
         * @param samplestamp the samplestamp in sample counts
         */
        public void setSamplestamp(long samplestamp)
        {
            this.samplestamp = samplestamp;
        }

        /**
         * Gets the the type of this Event. The type can be one ore more characters. The length of the type 
         * is defined in the EventEntry.
         * 
         * @return the type of this Event
         */
        public string getType()
        {
            return type;
        }

        /**
         * Sets the the type of this Event. The type can be one ore more characters. The length of the type 
         * is defined in the EventEntry. 

         * @param type the type of this Event
         */
        public void setType(string type)
        {
            this.type = type;
        }

        /**
         * Event Contructor
         * 
         * @param samplestamp the samplestamp of this Event in sample counts
         * @param type the type of this Event
         * @param comment the comment of this Event
         */
        public Event(long samplestamp, string type, string comment)
        {
            this.samplestamp = samplestamp;
            this.type = type;
            this.comment = comment;
        }

        public override bool Equals(Object o)
        {
            if (!(o is Event))
                return false;
            if (samplestamp == ((Event)o).getSamplestamp()
                && type.Equals(((Event)o).getType(), StringComparison.CurrentCultureIgnoreCase)
                && (((comment == null) && (((Event)o).getComment() == null))
                || comment.Equals(((Event)o).getComment(), StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}