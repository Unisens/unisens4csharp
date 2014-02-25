
/**
 * Enumeration of all data types that can be used in the unsiens file format. See 
 * unisens documentation for detailed information.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst  
 */
using System;

namespace org.unisens
{
    public enum DataType
    {
        INT8,
        UINT8,
        INT16,
        UINT16,
        INT32,
        UINT32,
        FLOAT,
        DOUBLE
    }

    public static class DataTypeExtensions
    {
        private static String _value;

        /** 
         * This method gives the representation of a datatype enum object as string.
         * 
         * @return correspoding string representation for a DataType enum object
         */
        public static String value(DataType dataType)
        {
            return _value ?? dataType.ToString().ToLower();
        }

        public static void SetValue(DataType dataType, string value)
        {
            _value = value.ToLower();
        }


        /** 
         * This method gives a DataType enum object that correspod to a given datatype name 
         * 
         * @param name the name of a datatype. See unisens documentation for a list of supported data types
         * @return DataType enum object, that correspod to that datatype name 
         * @{
         */
        public static DataType fromValue(DataType dataType, String name)
        {
            if (Enum.IsDefined(typeof(DataType), name.ToUpper()))
            {
                dataType = (DataType)Enum.Parse(typeof(DataType), name.ToUpper());
                return dataType;
            }
            throw new ArgumentOutOfRangeException(name);
        }

    }
}