using System;
namespace org.unisens.ri.io
{
    public abstract class SignalWriter : AbstractWriter
    {
        internal SignalEntry signalEntry;
        internal DataType dataType;
        internal int channelCount;

        public SignalWriter(SignalEntry signalEntry)
            : base(signalEntry)
        {
            this.signalEntry = signalEntry;
            dataType = signalEntry.getDataType();
            channelCount = signalEntry.getChannelCount();
        }
        public abstract void Append(Object data);
    }
}