using System;
using System.Xml;
using org.unisens.ri.io;
using org.unisens.ri.util;

namespace org.unisens.ri
{
    public class SignalEntryImpl : MeasurementEntryImpl, SignalEntry
    {
        private SignalReader signalReader = null;
        private SignalWriter signalWriter = null;

        internal SignalEntryImpl(Unisens unisens, XmlNode entryNode)
            : base(unisens, entryNode)
        {

        }

        internal SignalEntryImpl(Unisens unisens, string id, string[] channelNames, DataType dataType, double sampleRate)
            : base(unisens, id, channelNames, dataType, sampleRate)
        {

            fileFormat = new BinFileFormatImpl();
        }

        internal SignalEntryImpl(SignalEntry signalEntry)
            : base(signalEntry)
        {

        }

        public override void resetPos()
        {
            getReader<SignalReader>().resetPos();
        }

        public Object read(int length)
        {
            try
            {
                return getReader<SignalReader>().Read(length);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public Object read(long pos, int length)
        {
            return getReader<SignalReader>().Read(pos, length);
        }

        public void append(Object data)
        {
            getWriter<SignalWriter>().Append(data);
            getWriter<SignalWriter>().close();

            calculateCount(Utilities.getSampleCount(data, dataType));
        }

        internal override T getReader<T>()
        {
            if (signalReader == null)
                signalReader = SignalIoFactory.createSignalReader(this);
            return (T)(object)signalReader;
        }

        internal override T getWriter<T>()
        {
            if (signalWriter == null)
                signalWriter = SignalIoFactory.createSignalWriter(this);
            return (T)(object)signalWriter;
        }

        internal override bool isReaderOpened()
        {
            return signalReader != null;
        }

        internal override bool isWriterOpened()
        {
            return signalWriter != null;
        }

        public override T clone<T>()
        {
            return (T)(object)new SignalEntryImpl(this);
        }
    }
}
