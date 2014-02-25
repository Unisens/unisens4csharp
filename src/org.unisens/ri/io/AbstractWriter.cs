using System;
using System.IO;


namespace org.unisens.ri.io
{
    public abstract class AbstractWriter
    {
        protected String absoluteFileName = null;
        protected Boolean isOpened = false;

        public AbstractWriter(Entry entry)
        {
            var path = entry.getUnisens().getPath();
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path += Path.DirectorySeparatorChar.ToString();

            absoluteFileName = path + entry.getId();
        }
        public String getAbsoluteFileName()
        {
            return absoluteFileName;
        }

        public abstract void open();
        public abstract void close();
    }
}