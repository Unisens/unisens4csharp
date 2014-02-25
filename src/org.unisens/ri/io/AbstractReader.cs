using System;
using System.IO;

namespace org.unisens.ri.io
{
    public abstract class AbstractReader
    {
        protected long currentSample = 0;
        protected String absoluteFileName = null;
        protected bool isOpened = false;

        public AbstractReader(Entry entry)
        {
            var path = entry.getUnisens().getPath();
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                path += Path.DirectorySeparatorChar.ToString();
            absoluteFileName = path + entry.getId();
        }

        public virtual long getSampleCount()
        {
            throw new NotImplementedException();
        }

        public abstract void open();

        public abstract void close();

        public String getAbsoluteFileName()
        {
            return absoluteFileName;
        }
    }
}