using System.Collections.Generic;


namespace org.unisens.ri.io
{
    public abstract class EventWriter : AbstractWriter
    {
        internal EventEntry eventEntry = null;

        public EventWriter(EventEntry eventEntry)
            : base(eventEntry)
        {

            this.eventEntry = eventEntry;
        }

        public abstract void append(Event e);
        public abstract void append(List<Event> events);
        public abstract void empty();
    }
}