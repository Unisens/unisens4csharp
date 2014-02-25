using System;

namespace org.unisens
{


    /**
     * Enumeration to describe the Endianess of a binary file format.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public enum Endianess
    {
        LITTLE,
        BIG
    }

    public static class EndianessExtension
    {
        /**
            * Gets the value ???
            * 
            * @return the value of the attribute
            */
        //public static string GetValue(this Endianess endianess)
        //{
        //    return endianess.ToString();
        //}

        /** 
            * This method gives a Endianess enum object that correspod to a given endianess name. 
            *  
            * @param name endianess name as string
            * @return Endianess enum object, that correspod to that endianess name 
            */
        public static Endianess fromValue(string name)
        {
            return (Endianess)Enum.Parse(typeof(Endianess), name);
        }
    }
}