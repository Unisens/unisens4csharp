using System;
using System.Collections.Generic;
using System.IO;


namespace org.unisens.ri.io.csv
{
    public class EventCsvWriter : EventWriter
    {
        private StreamWriter _file;// BufferedFileWriter _file;
        private String _separator;
        private static readonly String Newline = Environment.NewLine;


        public EventCsvWriter(EventEntry eventEntry)
            : base(eventEntry)
        {
            open();
        }

        public override void open()
        {
            if (!isOpened)
            {
                FileStream fs = new FileStream(absoluteFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                _file = new StreamWriter(fs);// BufferedFileWriter(absoluteFileName);
                isOpened = true;
                _separator = ((CsvFileFormat)eventEntry.getFileFormat()).getSeparator();
            }
        }

        public override void close()
        {
            if (isOpened)
                _file.Close();
            isOpened = false;
        }

        public override void empty()
        {
            close();
            File.Delete(absoluteFileName);
            var file = File.Create(absoluteFileName);
            file.Close();
        }

        public override void append(Event newEvent)
        {
            open();
            _file.WriteLine(newEvent.getSamplestamp() + _separator + newEvent.getType() + _separator + newEvent.getComment());
            _file.Flush();
        }

        public override void append(List<Event> events)
        {
            open();
            foreach (Event newEvent in events)
                _file.WriteLine(newEvent.getSamplestamp() + _separator + newEvent.getType() + _separator + newEvent.getComment());
            _file.Flush();
        }
    }
}