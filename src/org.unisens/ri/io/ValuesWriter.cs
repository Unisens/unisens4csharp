namespace org.unisens.ri.io
{
    public abstract class ValuesWriter : AbstractWriter
    {
        internal ValuesEntry valuesEntry = null;
        internal DataType dataType;
        internal int channelCount = 0;

        public ValuesWriter(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {
            this.valuesEntry = valuesEntry;
            dataType = valuesEntry.getDataType();
            channelCount = valuesEntry.getChannelCount();
        }

        public abstract void append(Value data);
        public abstract void append(Value[] data);
        public abstract void appendValuesList(ValueList valueList);
    }
}