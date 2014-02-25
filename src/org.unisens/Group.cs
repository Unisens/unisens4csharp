using System.Collections.Generic;

namespace org.unisens
{


    /**
     * A Group can be used to semantically group related Entries. A unisens dataset can have multiple Groups.
     * Within a dataset groups have to have a unique id.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public interface Group
    {

        /**
         * Sets this comment for this Group
         * 
         * @param comment the comment
         */
        void setComment(string comment);

        /**
         * Gets the comment for this Group
         * 
         * @return the comment
         */
        string getComment();

        /**
         * Gets the id of this group. Within a dataset groups have to have a unique id.
         * 
         * @return the id of this Group
         */
        string getId();

        /**
         * Sets the id of this group. Within a dataset groups have to have a unique id.
         * 
         * @param id the new id of this Group
         */
        void setId(string id);

        /**
         * Gets all Entries of this Group.
         * 
         * @return the List of all Entries in this Group
         */
        List<Entry> getEntries();

        /**
         * Adds an Entry to this Group.
         * 
         * @param entry the Entry to be added
         */
        void addEntry(Entry entry);

        /**
         * Removes an Entry from this Group
         * 
         * @param entry the Entry to be removed
         */
        void removeEntry(Entry entry);
    }
}