using System;
using System.Xml;
using org.unisens.ri.config;
using org.unisens.ri.io;

namespace org.unisens.ri
{
    public abstract class TimedEntryImpl : EntryImpl, TimedEntry
    {
        internal long count = -1;
        internal double sampleRate = 0;
        public TimedEntryImpl(Unisens unisens, XmlNode timedEntryNode)
            : base(unisens, timedEntryNode)
        {
            parse(timedEntryNode);

        }

        public TimedEntryImpl(Unisens unisens, string id, double sampleRate)
            : base(unisens, id)
        {
            this.sampleRate = sampleRate;
        }

        internal TimedEntryImpl(TimedEntry timedEntry)
            : base(timedEntry)
        {

            sampleRate = timedEntry.getSampleRate();
        }

        private void parse(XmlNode timedEntryNode)
        {
            var attrs = timedEntryNode.Attributes;
            var attrNode = attrs.GetNamedItem(Constants.TIMEDENTRY_SAMPLERATE);
            //sampleRate = (attrNode != null) ? Double.Parse(attrNode.Value) : 0;
            sampleRate = (attrNode != null) ? Double.Parse(attrNode.Value, System.Globalization.CultureInfo.InvariantCulture) : 0;
            return;
        }

        public long getCount()
        {
            if (count == -1)
                calculateCount();
            return count;
        }

        internal void calculateCount()
        {
            var reader = this.getReader<AbstractReader>();
            count = reader.getSampleCount();
        }
        internal void calculateCount(int increase)
        {
            if (count == -1)
                calculateCount();
            else
                count += increase;
        }

        public double getSampleRate()
        {
            return sampleRate;
        }

        public void setSampleRate(double sampleRate)
        {
            this.sampleRate = sampleRate;
        }
    }
}
