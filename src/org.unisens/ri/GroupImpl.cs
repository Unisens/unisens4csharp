using System.Collections.Generic;
using System.Xml;
using org.unisens;
using org.unisens.ri.config;
namespace org.unisens.ri
{
    public class GroupImpl : Group
    {
        private string id = null;
        private string comment = null;

        private List<Entry> entries = new List<Entry>();
        private Unisens unisens = null;

        public GroupImpl(Unisens unisens, XmlNode groupNode)
        {
            this.unisens = unisens;
            parse(groupNode);
        }

        public GroupImpl(Unisens unisens, string id, string comment)
        {
            this.id = id;
            this.comment = comment;
            this.unisens = unisens;
        }


        private void parse(XmlNode groupNode)
        {
            var attr = groupNode.Attributes;
            var attrNode = attr.GetNamedItem(Constants.GROUP_ID);
            id = (attrNode != null) ? attrNode.Value : "";
            attrNode = attr.GetNamedItem(Constants.GROUP_COMMENT);
            comment = (attrNode != null) ? attrNode.Value : "";

            var groupEntryNodes = groupNode.ChildNodes;
            XmlNode groupEntryNode;
            int length = groupEntryNodes.Count;
            for (int i = 0; i < length; i++)
            {
                groupEntryNode = groupEntryNodes.Item(i);
                if (groupEntryNode.NodeType == XmlNodeType.Element)
                {
                    string entryId = groupEntryNode.Attributes.GetNamedItem(Constants.GROUPENTRY_REF).Value;
                    Entry entry = unisens.getEntry(entryId);
                    if (entry != null)
                        entries.Add(entry);
                    else
                        throw new UnisensParseException(string.Format("Unvalid unisens.xml : the entry with id {0} in group with id {1} not exist!", entryId, getId()), UnisensParseExceptionTypeEnum.UNISENS_GROUP_ENTRY_NOT_EXIST);
                }
            }
        }

        public void addEntry(Entry entry)
        {
            entries.Add(entry);
        }


        public List<Entry> getEntries()
        {
            return entries;
        }

        public void removeEntry(Entry entry)
        {
            entries.Remove(entry);
        }


        public string getComment()
        {
            return comment;
        }

        public void setComment(string comment)
        {
            this.comment = comment;
        }

        public string getId()
        {
            return id;
        }

        public void setId(string id)
        {
            this.id = id;
        }
    }
}