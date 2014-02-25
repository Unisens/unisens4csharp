using System;
using System.IO;
//using java.nio;

namespace org.unisens.ri.io
{
    public class BufferedFileWriter
    {
        internal FileStream file;
        internal string absoluteFileName;
        //internal BufferedStream outFile;
        internal BinaryWriter outFile;
        //internal ByteBuffer buf1;
        //internal ByteBuffer buf2;
        //internal ByteBuffer buf4;
        //internal ByteBuffer buf8;
        internal byte[] byteBuf1;
        internal byte[] byteBuf2;
        internal byte[] byteBuf4;
        internal byte[] byteBuf8;

        //internal ByteOrder endian;

        internal bool signed;
        private Guid id = Guid.NewGuid();

        public BufferedFileWriter(string fileName)
        {
            absoluteFileName = fileName;
            //outFile = new BinaryWriter(new StreamWriter(absoluteFileName).BaseStream);
            outFile = new BinaryWriter(new FileStream(absoluteFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite));
            outFile.Seek(0, SeekOrigin.End);
            //outFile.AutoFlush = true;
            //buf1 = ByteBuffer.allocate(1);
            //buf2 = ByteBuffer.allocate(2);
            //buf4 = ByteBuffer.allocate(4);
            //buf8 = ByteBuffer.allocate(8);
            //buf1.order(ByteOrder.LITTLE_ENDIAN);
            //buf2.order(ByteOrder.LITTLE_ENDIAN);
            //buf4.order(ByteOrder.LITTLE_ENDIAN);
            //buf8.order(ByteOrder.LITTLE_ENDIAN);
            byteBuf1 = new byte[1];
            byteBuf2 = new byte[2];
            byteBuf4 = new byte[4];
            byteBuf8 = new byte[8];
            signed = true;
        }

        //public void setEndian(ByteOrder byteOrder)
        //{
        //    buf1.order(byteOrder);
        //    buf2.order(byteOrder);
        //    buf4.order(byteOrder);
        //    buf8.order(byteOrder);
        //}

        //public ByteOrder getEndian()
        //{
        //    return buf1.order();
        //}

        public void setSigned(bool b)
        {
            signed = b;
        }

        public bool getSigned()
        {
            return signed;
        }
        public void write8(sbyte sb)
        {
            outFile.Write(sb);
        }
        public void write8(byte b)
        {
            outFile.Write(b);
        }
        public void write8(short b)
        {
            //buf1.clear();
            //buf1.put((byte)(b & 0xff));
            //buf1.rewind();
            outFile.Write((byte)(b & 0xff));//(b);
        }

        public void write16(int i)
        {
            //buf2.clear();
            //buf2.putShort((short)(i & 0xffff));
            //buf2.rewind();
            outFile.Write((short)(i & 0xffff));
        }

        public void write32(int l)
        {
            //buf4.clear();
            //buf4.putInt((int)(l & 0xffffffff));
            //buf4.rewind();
            outFile.Write((int)(l & 0xffffffff));
        }

        public void write32(long l)
        {
            //buf4.clear();
            //buf4.putInt((int)(l & 0xffffffff));
            //buf4.rewind();
            outFile.Write((int)(l & 0xffffffff));
        }


        public void write64(long l)
        {
            //buf8.clear();
            //buf8.putLong(l);
            //buf8.rewind();
            outFile.Write(l);
        }

        public void writeFloat(float f)
        {
            //buf4.clear();
            //buf4.putFloat(f);
            //buf4.rewind();
            outFile.Write(f);
        }

        public void writeDouble(double d)
        {
            //buf8.clear();
            //buf8.putDouble(d);
            //buf8.rewind();
            outFile.Write(d);
        }

        public void writestring(string str)
        {
            outFile.Write(str);
        }

        public void writeBytes(byte[] bytes)
        {
            outFile.Write(bytes);
        }

        public long length()
        {
            return file.Length;
        }

        public void flush()
        {
            outFile.Flush();
        }
        public void close()
        {
            outFile.Close();
        }

        public void empty()
        {
            outFile.Close();
            File.Delete(absoluteFileName);
            file = File.Create(absoluteFileName);
            FileStream fs = new FileStream(file.Name, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            outFile = new BinaryWriter(new StreamWriter(fs).BaseStream);
        }
    }
}



