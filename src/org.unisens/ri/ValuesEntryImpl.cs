using System;
using System.IO;
using System.Xml;
using org.unisens.ri.io;

namespace org.unisens.ri
{
    public class ValuesEntryImpl : MeasurementEntryImpl, ValuesEntry
    {
        private ValuesReader valuesReader = null;
        private ValuesWriter valuesWriter = null;

        internal ValuesEntryImpl(Unisens unisens, XmlNode entryNode)
            : base(unisens, entryNode)
        {
        }

        internal ValuesEntryImpl(Unisens unisens, string id, string[] channelNames, DataType dataType, double sampleRate)
            : base(unisens, id, channelNames, dataType, sampleRate)
        {
            ;
            fileFormat = new CsvFileFormatImpl();

            //var file = File.Create(unisens.getPath() + Path.DirectorySeparatorChar + getId());
            //file.Close();
        }

        internal ValuesEntryImpl(ValuesEntry valuesEntry)
            : base(valuesEntry)
        {
        }

        public override void resetPos()
        {
            getReader<ValuesReader>().resetPos();
        }

        public void append(Value data)
        {
            getWriter<ValuesWriter>().append(data);
            //getWriter<ValuesWriter>().close();
            calculateCount(1);
        }

        public void append(Value[] data)
        {
            getWriter<ValuesWriter>().append(data);
            //getWriter<ValuesWriter>().close();
            calculateCount(data.Length);
        }

        public void appendValuesList(ValueList valueList)
        {
            getWriter<ValuesWriter>().appendValuesList(valueList);
           // getWriter<ValuesWriter>().close();
            calculateCount(valueList.getSamplestamps().Length);
        }


        public Value[] read(int length)
        {
            return getReader<ValuesReader>().Read(length);
        }

        public Value[] read(long pos, int length)
        {
            return getReader<ValuesReader>().Read(pos, length);
        }

        public ValueList readValuesList(int length)
        {
            return getReader<ValuesReader>().ReadValuesList(length);
        }

        public ValueList readValuesList(long pos, int length)
        {
            return getReader<ValuesReader>().ReadValuesList(pos, length);
        }

        internal override T getReader<T>()
        {
            if (valuesReader == null)
            {
                valuesReader = ValuesIoFactory.createValuesReader(this);
            }
            return (T)(object)valuesReader;
        }

        internal override T getWriter<T>()
        {
            if (valuesWriter == null)
                valuesWriter = ValuesIoFactory.createValuesWriter(this);
            return (T)(object)valuesWriter;
        }

        internal override bool isReaderOpened()
        {
            return valuesReader != null;
        }

        internal override bool isWriterOpened()
        {
            return valuesWriter != null;
        }

        public override T clone<T>()
        {
            return (T)(object)new ValuesEntryImpl(this);
        }
    }
}
