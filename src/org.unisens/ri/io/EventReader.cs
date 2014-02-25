using System.Collections.Generic;

namespace org.unisens.ri.io
{
    public abstract class EventReader : AbstractReader
    {
        protected EventEntry eventEntry;

        public EventReader(EventEntry eventEntry)
            : base(eventEntry)
        {
            this.eventEntry = eventEntry;
        }

        public abstract List<Event> read(int length);
        public abstract List<Event> read(long position, int length);
    }
}