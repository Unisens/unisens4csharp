
using System;
using System.Collections.Generic;
using System.Xml;
using org.unisens.ri.config;
using org.unisens.ri.io;


namespace org.unisens.ri
{
    public class EventEntryImpl : TimedEntryImpl, EventEntry
    {
        private int typeLength = 0;
        private int commentLength = 0;

        private EventReader eventReader = null;
        private EventWriter eventWriter = null;


        internal EventEntryImpl(Unisens unisens, XmlNode eventNode)
            : base(unisens, eventNode)
        {
            parse(eventNode);
        }

        internal EventEntryImpl(Unisens unisens, string id, double sampleRate)
            : base(unisens, id, sampleRate)
        {
            ;
            fileFormat = new CsvFileFormatImpl();
        }

        internal EventEntryImpl(EventEntry eventEntry)
            : base(eventEntry)
        {
            typeLength = eventEntry.getTypeLength();
            commentLength = eventEntry.getCommentLength();
        }

        private void parse(XmlNode eventNode)
        {
            var attrs = eventNode.Attributes;
            var attrNode = attrs.GetNamedItem(Constants.EVENTENTRY_COMMENT_LENGTH);
            commentLength = (attrNode != null) ? int.Parse(attrNode.Value) : 0;
            attrNode = attrs.GetNamedItem(Constants.EVENTENTRY_TYPE_LENGTH);
            typeLength = (attrNode != null) ? int.Parse(attrNode.Value) : 0;
        }

        public void empty()
        {
            close();
            getWriter<EventWriter>().empty();
            count = 0;
        }

        public int getCommentLength()
        {
            return commentLength;
        }

        public int getTypeLength()
        {
            return typeLength;
        }

        public void setCommentLength(int commentLength)
        {
            this.commentLength = commentLength;
        }

        public void setTypeLength(int typeLength)
        {
            this.typeLength = typeLength;

        }

        public new double getSampleRate()
        {
            return sampleRate;
        }

        public new void setSampleRate(double sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        public List<Event> read(int length)
        {
            List<Event> result = getReader<EventReader>().read(length);
            getReader<EventReader>().close();
            return result;
        }


        public List<Event> read(long position, int length)
        {
            List<Event> result = getReader<EventReader>().read(position, length);
            getReader<EventReader>().close();
            return result;
        }

        public void append(Event e)
        {
            getWriter<EventWriter>().append(e);
            getWriter<EventWriter>().close();
            calculateCount(1);
        }


        public void append(List<Event> events)
        {
            getWriter<EventWriter>().append(events);
            getWriter<EventWriter>().close();
            calculateCount(events.ToArray().Length);
        }

        internal override T getReader<T>()
        {
            if (eventReader == null)
                eventReader = EventIoFactory.createEventReader(this);
            return (T)(object)eventReader;
        }

        internal override T getWriter<T>()
        {
            if (eventWriter == null)
                eventWriter = EventIoFactory.createEventWriter(this);
            return (T)(object)eventWriter;
        }

        internal override bool isReaderOpened()
        {
            return eventReader != null;
        }

        internal override bool isWriterOpened()
        {
            return eventWriter != null;
        }

        public override T clone<T>()
        {
            return (T)(object)new EventEntryImpl(this);
        }
    }
}
