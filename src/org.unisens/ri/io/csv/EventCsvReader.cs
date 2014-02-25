using System;
using System.Collections.Generic;
using System.IO;
using org.unisens.ri.util;

namespace org.unisens.ri.io.csv
{
    public class EventCsvReader : EventReader
    {
        private StreamReader _file;//BufferedReader
        private String _separator;


        public EventCsvReader(EventEntry eventEntry)
            : base(eventEntry)
        {
            open();
        }

        public override void open()
        {
            if (!isOpened)
            {
                FileStream fs = new FileStream(absoluteFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _file = new StreamReader(fs);
                _separator = ((CsvFileFormat)eventEntry.getFileFormat()).getSeparator();
                isOpened = true;
                currentSample = 0;
            }
        }


        public override void close()
        {
            if (isOpened)
                _file.Close();
            isOpened = false;
        }

        public override List<Event> read(int length)
        {
            return read(currentSample, length);
        }


        public override List<Event> read(long pos, int length)
        {
            open();
            long sampleCount = eventEntry.getCount();
            if (pos > sampleCount)
                return null;

            if (pos != currentSample)
                Seek(pos);

            if (pos + length > sampleCount)
                length = (int)(sampleCount - pos);

            return ReadLong(length);
        }


        private List<Event> ReadLong(int numberOfEvents)
        {
            var events = new List<Event>();
            String currentEvent;
            StringTokenizer tokenizer;
            long timestamp = -1;
            String type = "";
            String comment = "";
            while (numberOfEvents > 0)
            {
                currentEvent = _file.ReadLine();
                tokenizer = new StringTokenizer(currentEvent, _separator);
                if (tokenizer.HasMoreTokens())
                    timestamp = Convert.ToInt64(tokenizer.NextToken());
                if (tokenizer.HasMoreTokens())
                    type = tokenizer.NextToken();
                if (tokenizer.HasMoreTokens())
                    comment = tokenizer.NextToken();
                events.Add(new Event(timestamp, type, comment));
                type = "";
                comment = "";
                timestamp = -1;
                numberOfEvents--;
                currentSample++;
            }
            close();
            return events;
        }

        private void Seek(long pos)
        {
            //try
            //{
                while (currentSample < pos)
                {
                    _file.ReadLine();
                    currentSample++;
                }
                if (currentSample > pos)
                {
                    ResetPos();
                    Seek(pos);
                }
            //}
            //catch (IOException ioe)
            //{
            //    ioe.printStackTrace();
            //}
        }

        public void ResetPos()
        {
            currentSample = 0;
            close();
            open();
        }

        public override long getSampleCount()
        {
            //try
            //{
                open();
                long sampleCount = 0;
                while (_file.ReadLine() != null)
                    sampleCount++;
                ResetPos();
                return sampleCount;
            //}
            //catch (IOException ioe)
            //{
            //    ioe.printStackTrace();
            //    return 0;
            //}
        }
    }
}