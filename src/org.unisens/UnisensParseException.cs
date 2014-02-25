using System;

namespace org.unisens
{

    /**
     * UnisensParseException represents specific Exceptions that may occur while parsing 
     * the unisens.xml file of a unisens DataSet.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public class UnisensParseException : Exception
    {
        private readonly long serialVersionUID = 2344359345923985L;
        private UnisensParseExceptionTypeEnum unisensParseExceptionType;

        public UnisensParseException(UnisensParseExceptionTypeEnum unisensParseExceptionType)
            : base()
        {
            this.unisensParseExceptionType = unisensParseExceptionType;
        }

        public UnisensParseException(string message, UnisensParseExceptionTypeEnum unisensParseExceptionType)
            : base(message)
        {

            this.unisensParseExceptionType = unisensParseExceptionType;
        }

        public UnisensParseExceptionTypeEnum getUnisensParseExceptionType()
        {
            return unisensParseExceptionType;
        }
    }
}