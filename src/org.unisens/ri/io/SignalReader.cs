using System;
namespace org.unisens.ri.io
{
    public abstract class SignalReader : AbstractReader
    {
        internal SignalEntry signalEntry = null;
        internal DataType dataType;
        internal int channelCount = 0;
        internal double lsbValue;
        internal int baseline;

        public SignalReader(SignalEntry signalEntry)
            : base(signalEntry)
        {

            this.signalEntry = signalEntry;
            dataType = signalEntry.getDataType();
            channelCount = signalEntry.getChannelCount();
            lsbValue = signalEntry.getLsbValue();
            baseline = signalEntry.getBaseline();
        }
        public abstract Object Read(int length);
        public abstract Object Read(long pos, int length);
        public abstract double[][] readScaled(int length);
        public abstract double[][] readScaled(long pos, int length);
        public abstract void resetPos();
    }
}