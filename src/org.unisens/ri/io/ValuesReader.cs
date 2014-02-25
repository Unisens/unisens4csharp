namespace org.unisens.ri.io
{
    public abstract class ValuesReader : AbstractReader
    {
        internal ValuesEntry valuesEntry = null;
        internal DataType dataType;
        internal int channelCount = 0;
        internal double lsbValue;
        internal int baseline;

        public ValuesReader(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {
            this.valuesEntry = valuesEntry;
            dataType = valuesEntry.getDataType();
            channelCount = valuesEntry.getChannelCount();
            lsbValue = valuesEntry.getLsbValue();
            baseline = valuesEntry.getBaseline();
        }
        public abstract Value[] Read(int length);
        public abstract Value[] Read(long pos, int length);
        public abstract Value[] readScaled(int length);
        public abstract Value[] readScaled(long pos, int length);
        public abstract ValueList ReadValuesList(int length);
        public abstract ValueList ReadValuesList(long pos, int length);
        public abstract ValueList readValuesListScaled(int length);
        public abstract ValueList readValuesListScaled(long pos, int length);
        public abstract void resetPos();
    }
}