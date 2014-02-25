
using System;
using System.IO;
using System.Text;
using org.unisens.ri.util;
//using java.nio;

namespace org.unisens.ri.io
{
    public class BufferedFileReader
    {
        //internal FileStream file;
        internal string absoluteFileName;
        internal BinaryReader inFile; //BufferedStream
        //internal StreamReader inFile;
        //internal ByteBuffer buf1;
        //internal ByteBuffer buf2;
        //internal ByteBuffer buf4;
        //internal ByteBuffer buf8;
        internal ByteBuffer buf;
        internal byte[] byteBuf1;
        internal byte[] byteBuf2;
        internal byte[] byteBuf4;
        internal byte[] byteBuf8;

        //internal ByteOrder endian;

        internal bool signed;

        internal long currentPosition;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public BufferedFileReader(string fileName)
        {
            absoluteFileName = fileName;

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            inFile = new BinaryReader(new StreamReader(fs).BaseStream);
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
            currentPosition = 0;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public short read8()
        {
            //int readed = inFile.Read(byteBuf1, 0, byteBuf1.Length);
            //currentPosition += readed;
            currentPosition += byteBuf1.Length;
            byte result = inFile.ReadByte();
            if (signed)
                return result;//byteBuf1[0];
            else
                return (short)result;//(byteBuf1[0] & 0xff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public int read16()
        {
            //int readed = inFile.Read(byteBuf2, 0, byteBuf2.Length);
            //currentPosition += readed;
            //buf2.clear();
            //buf2.put(byteBuf2);
            //buf2.rewind();

            //int result = buf2.getShort();

            currentPosition += byteBuf2.Length;
            int result; 
            if (signed)
            {
                result = inFile.ReadInt16();
                if ((result & 0x8000) == 0x8000)
                    result = -(0x10000 - result);
            }
            else
            {
                result = inFile.ReadUInt16();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public long read32()
        {
            //int readed = inFile.Read(byteBuf4, 0, byteBuf4.Length);//byteBuf4
            currentPosition += byteBuf4.Length;// readed;
            //buf4.clear();
            //buf4.put(byteBuf4);
            //buf4.rewind();

            long result;// buf4.getInt();
            if (signed)
            {
                result = inFile.ReadInt32();
                if ((result & 0x80000000L) == 0x80000000L)
                    result = -(0x100000000L - result);
            }
            else
            {
                result = inFile.ReadUInt32();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public long read64()
        {
            //int readed = inFile.Read(byteBuf8, 0, byteBuf8.Length);
            //currentPosition += readed;
            //buf8.clear();
            //buf8.put(byteBuf8);
            //buf8.rewind();

            //long result = buf8.getLong();
            currentPosition += byteBuf8.Length;
            var result = inFile.ReadInt64(); 

            if (signed)
                if ((result & 0x800000000000000L) == 0x800000000000000L)
                    result = -(0x1000000000000000L - result);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public float readFloat()
        {
            //int readed = inFile.Read(byteBuf4, 0, byteBuf4.Length);
            //currentPosition += readed;
            //buf4.clear();
            //buf4.put(byteBuf4);
            //buf4.rewind();

            //float result = buf4.getFloat();
            currentPosition += byteBuf4.Length;
            var result = inFile.ReadSingle(); 

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public double readDouble()
        {
            //int readed = inFile.Read(byteBuf8, 0, byteBuf8.Length);
            //currentPosition += readed;
            //buf8.clear();
            //buf8.put(byteBuf8);
            //buf8.rewind();

            //double result = buf8.getDouble();
            currentPosition += byteBuf8.Length;
            var result = inFile.ReadDouble(); 

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public string readstring(int byteLength)
        {
            byte[] buff = new byte[byteLength];
            int readed = inFile.Read(buff, 0, byteLength);
            currentPosition += readed;
            return Encoding.UTF8.GetString(buff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public byte[] readBytes(int length)
        {
            //byte[] buf = new byte[length];
            byte[] buf = inFile.ReadBytes(length);//buf, 0, length);//Convert.ToInt32(currentPosition)
            currentPosition += length;
            return buf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public long length()
        {
            return inFile.BaseStream.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public void seek(long pos)
        {
            if (currentPosition <= pos)
            {
                if (currentPosition != pos)// while
                {
                    currentPosition = inFile.BaseStream.Seek(pos, SeekOrigin.Begin);
                    //long skipped = inFile.BaseStream.Seek(pos - currentPosition, SeekOrigin.Current);
                    //currentPosition += skipped;
                }
            }
            else
            {
                inFile.Close();
                FileStream fs = new FileStream(absoluteFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                inFile = new BinaryReader(new StreamReader(fs).BaseStream); //new BufferedStream(file);
                currentPosition = 0;
                seek(pos);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public void close()
        {
            inFile.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="File"></param>
        /// <exception cref="IOException"></exception>
        public void empty()
        {
            inFile.Close();
            File.Delete(absoluteFileName);
            //file = File.Create(absoluteFileName);
            FileStream fs = new FileStream(absoluteFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            inFile = new BinaryReader(new StreamReader(fs).BaseStream); //new BufferedStream(file);
            currentPosition = 0;
        }
    }


}
