

/**
 * This exception is thrown when a new Entry or Group is created with an id that already 
 * exists in the unisens dataset.
 * 
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
using System;

namespace org.unisens
{
    public class DuplicateIdException : Exception
    {
        static readonly long serialVersionUID = 732728392787475L;
        public DuplicateIdException(String message)
            : base(message)
        {
        }
    }
}