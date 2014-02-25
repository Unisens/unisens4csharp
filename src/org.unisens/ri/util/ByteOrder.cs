using System;

namespace org.unisens.ri.util
{
    public sealed class ByteOrder : Object
    {
        public static readonly ByteOrder BIG_ENDIAN;
        public static readonly ByteOrder LITTLE_ENDIAN;

        public static short Swap16Bit(short v)
        {
            return (short)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }
        public static UInt16 Swap16Bit(UInt16 v)
        {
            return (UInt16)(((v & 0xff) << 8) | ((v >> 8) & 0xff));    
        }

        public static int Swap32Bit(int v)
        {
            return (int)(((Swap16Bit((short)v) & 0xffff) << 0x10) |
                          (Swap16Bit((short)(v >> 0x10)) & 0xffff));
        }
        public static uint Swap32Bit(uint v)
        {
            return (uint)((((UInt16)Swap16Bit((UInt16)v) & 0xffff) << 0x10) |

                          ((UInt16)Swap16Bit((UInt16)(v >> 0x10)) & 0xffff));
        }

        public static long Swap64Bit(long v)
        {

            return (long)((((int)Swap32Bit((int)v) & 0xffffffff) << 32) |

                           ((int)Swap32Bit((int)(v >> 0x32)) & 0xffffffff));

        }

        public static float Swap32Bit(float v)
        {
            // Convert Float to 4 Bytes
            byte temp;
            int k = 3;
            byte[] ab4ByteVector = BitConverter.GetBytes(v);           
            for (int i = 0; i <= 3; i++)
            {
                k--;
                for (int j = 0; j <= k; j++)
                {
                    temp = ab4ByteVector[j];
                    ab4ByteVector[j] = ab4ByteVector[j + 1];
                    ab4ByteVector[j + 1] = temp;
                }
            }
            return BitConverter.ToSingle(ab4ByteVector,0);
        }

        public static double Swap64Bit(double v)
        {
            // Convert Float to 4 Bytes
            byte temp;
            int k = 7;
            byte[] ab4ByteVector = BitConverter.GetBytes(v);          
            for (int i = 0; i <= 7; i++)
            {
                k--;
                for (int j = 0; j <= k; j++)
                {
                    temp = ab4ByteVector[j];
                    ab4ByteVector[j] = ab4ByteVector[j + 1];
                    ab4ByteVector[j + 1] = temp;
                }
            }
            return BitConverter.ToDouble(ab4ByteVector,0);
        }
    }
}
