
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using org.unisens.ri.config;
using org.unisens.ri.util;
using System.Reflection;

namespace org.unisens.ri
{
    /**
     * @author Radoslav Nedkov
     * @email radi.nedkov@gmail.com
     *
     * 25.02.2007
     */
    public class UnisensImpl : Unisens
    {
        private string absolutePath;
        private string absoluteFileName;
        private string version;
        private string measurementId;
        private DateTime timestampStart;
        private double duration;
        private string comment;
        private NumberFormatInfo decimalFormat = null;

        private Dictionary<string, string> customAttributes = new Dictionary<string, string>();

        private List<Entry> entries = new List<Entry>();
        private Context context = null;
        private List<Group> groups = new List<Group>();

        public UnisensImpl(string absolutePath)
        {
            this.absolutePath = absolutePath + @"/";
            if (!absolutePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                absolutePath += Path.DirectorySeparatorChar.ToString();

            absoluteFileName = absolutePath + "unisens.xml";
            decimalFormat = new NumberFormatInfo
                                {
                                    NumberGroupSizes = new int[] { 0 },
                                    NumberDecimalSeparator = ".",
                                    NumberDecimalDigits = 99
                                };
            //decimalFormat.setDecimalSeparatorAlwaysShown(false);
            if (File.Exists(absoluteFileName))
                parse();
            if (getVersion() == null)
                version = "2.0";
        }

        public Dictionary<string, string> getCustomAttributes()
        {
            return customAttributes;
        }

        public void addCustomAttribute(string key, string value)
        {
            customAttributes.Add(key, value);
        }

        public SignalEntry createSignalEntry(string id, string[] channelNames, DataType dataType, double sampleRate)
        {
            if (getEntry(id) != null)
                throw new DuplicateIdException("The entry with " + id + " exist!");
            SignalEntry signalEntry = new SignalEntryImpl(this, id, channelNames, dataType, sampleRate);
            entries.Add(signalEntry);
            return signalEntry;
        }

        public Group createGroup(string id)
        {
            if (getGroup(id) != null)
                throw new DuplicateIdException("The group with " + id + " exist!");
            Group group = new GroupImpl(this, id, null);
            groups.Add(group);
            return group;
        }

        public Context createContext(string schemaUrl)
        {
            Context context = new ContextImpl(schemaUrl);
            this.context = context;
            return context;
        }


        public EventEntry createEventEntry(string id, double sampleRate)
        {
            if (getEntry(id) != null)
                throw new DuplicateIdException("The entry with " + id + " exist!");
            EventEntry eventEntry = new EventEntryImpl(this, id, sampleRate);
            entries.Add(eventEntry);
            return eventEntry;
        }

        public ValuesEntry createValuesEntry(string id, string[] channelNames, DataType dataType, double sampleRate)
        {
            if (getEntry(id) != null)
                throw new DuplicateIdException("The entry with " + id + " exist!");
            ValuesEntry valuesEntry = new ValuesEntryImpl(this, id, channelNames, dataType, sampleRate);
            entries.Add(valuesEntry);
            return valuesEntry;
        }

        public CustomEntry createCustomEntry(string id)
        {
            if (getEntry(id) != null)
                throw new DuplicateIdException("The entry with " + id + " exist!");
            CustomEntry customEntry = new CustomEntryImpl(this, id);
            entries.Add(customEntry);
            return customEntry;
        }

        public Group addGroup(Group group, bool deepCopy)
        {
            if (getGroup(group.getId()) != null)
                throw new DuplicateIdException("The group with " + group.getId() + " exist!");
            Group myGroup = new GroupImpl(this, group.getId(), group.getComment());
            List<Entry> groupEntries = group.getEntries();
            foreach (Entry groupEntry in groupEntries)
            {
                myGroup.addEntry(addEntry(groupEntry, deepCopy));
            }

            groups.Add(myGroup);
            return myGroup;
        }

        public void deleteGroup(Group group)
        {
            groups.Remove(group);
        }


        public List<Entry> getEntries()
        {
            return entries;
        }

        public Entry addEntry(Entry entry, bool deepCopy)
        {
            if (getEntry(entry.getId()) != null)
                throw new DuplicateIdException("The entry with " + entry.getId() + " exist!");
            var myEntry = entry.clone<Entry>();
            myEntry.setUnisens(this);
            entries.Add(myEntry);
            if (deepCopy)
            {
                if (File.Exists(entry.getUnisens().getPath() + entry.getId()))
                    File.Copy(entry.getUnisens().getPath() + entry.getId(), getPath() + entry.getId(),true);
                    ///Utilities.copyFile(new FileStream(entry.getUnisens().getPath() + entry.getId(), FileMode.OpenOrCreate), new FileStream(getPath() + entry.getId(), FileMode.OpenOrCreate));
            }
            return myEntry;
        }

        public Entry getEntry(string id)
        {
            foreach (Entry entry in entries)
                if (entry.getId().Equals(id, StringComparison.CurrentCultureIgnoreCase))
                    return entry;
            return null;
        }

        public void deleteEntry(Entry entry)
        {
            entry.close();
            File.Delete(absolutePath + entry.getId());
            entries.Remove(entry);
            foreach (Group group in groups)
                group.getEntries().Remove(entry);

        }


        public string getComment()
        {
            return comment;
        }

        public void setComment(string comment)
        {
            this.comment = comment;
        }

        public double getDuration()
        {
            return duration;
        }

        public void setDuration(double duration)
        {
            this.duration = duration;
        }

        public void setTimestampStart(DateTime timestampStart)
        {
            this.timestampStart = timestampStart;
        }

        public string getMeasurementId()
        {
            return measurementId;
        }

        public void setMeasurementId(string measurementId)
        {
            this.measurementId = measurementId;
        }

        public DateTime getTimestampStart()
        {
            return timestampStart;
        }

        public string getVersion()
        {
            return version;
        }


        public Context getContext()
        {
            return context;
        }

        public void deleteContext()
        {
            context = null;
        }

        public List<Group> getGroups()
        {
            return groups;
        }

        public Group getGroup(string id)
        {
            foreach (Group group in groups)
            {
                if (group.getId().Equals(id, StringComparison.CurrentCultureIgnoreCase))
                {
                    return group;
                }
            }

            return null;
        }


        public string getPath()
        {
            return absolutePath;
        }

        private void parse()
        {

            //DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            //factory.setNamespaceAware(true);

            //var fis = new BinaryReader(File.OpenRead(absoluteFileName));
            //DocumentBuilder documentBuilder = factory.newDocumentBuilder();
            var document = new XmlDocument();// documentBuilder.parse(fis);
            document.Load(absoluteFileName);
            string validate = UserSettings.Default[Constants.PROPERTIE_VALIDATION].ToString();
            if (validate != null && validate.ToLower().Equals("true"))
                this.validate(document);

            XmlNode unisensNode = document.DocumentElement;//.getDocumentElement();
            var unisensChildNodes = unisensNode.ChildNodes;
            parse(unisensNode);

            XmlNode currentNode;

            EntryImpl entry = null;
            ContextImpl context = null;
            GroupImpl group = null;

            for (int i = 0; i < unisensChildNodes.Count; i++)
            {
                currentNode = unisensChildNodes.Item(i);
                if (currentNode.NodeType == XmlNodeType.Element)
                {
                    if (currentNode.Name.ToUpper().Equals(Constants.SIGNALENTRY.ToUpper()))
                    {
                        entry = new SignalEntryImpl(this, currentNode);
                        entries.Add(entry);
                        continue;
                    }
                    if (currentNode.Name.ToUpper().Equals(Constants.EVENTENTRY.ToUpper()))
                    {
                        entry = new EventEntryImpl(this, currentNode);
                        entries.Add(entry);
                        continue;
                    }
                    if (currentNode.Name.ToUpper().Equals(Constants.CUSTOMENTRY.ToUpper()))
                    {
                        entry = new CustomEntryImpl(this, currentNode);
                        entries.Add(entry);
                        continue;
                    }
                    if (currentNode.Name.ToUpper().Equals(Constants.VALUESENTRY.ToUpper()))
                    {
                        entry = new ValuesEntryImpl(this, currentNode);
                        entries.Add(entry);
                        continue;
                    }
                    if (currentNode.Name.ToUpper().Equals(Constants.CONTEXT.ToUpper()))
                    {
                        context = new ContextImpl(currentNode);
                        this.context = context;
                        continue;
                    }
                    if (currentNode.Name.ToUpper().Equals(Constants.GROUP.ToUpper()))
                    {
                        group = new GroupImpl(this, currentNode);
                        groups.Add(group);
                        continue;
                    }
                    if (currentNode.Name.ToUpper().Equals(Constants.CUSTOM_ATTRIBUTES.ToUpper()))
                    {
                        var customAttributeNodes = currentNode.ChildNodes;
                        for (int j = 0; j < customAttributeNodes.Count; j++)
                        {
                            var customAttributeNode = customAttributeNodes.Item(j);
                            if (customAttributeNode.Name.ToUpper().Equals(Constants.CUSTOM_ATTRIBUTE.ToUpper()))
                            {
                                var attrs = customAttributeNode.Attributes;
                                var keyNode = attrs.GetNamedItem(Constants.CUSTOM_ATTRIBUTE_KEY);
                                string key = "";
                                if (keyNode != null)
                                {
                                    key = keyNode.Value;
                                }
                                var valueNode = attrs.GetNamedItem(Constants.CUSTOM_ATTRIBUTE_VALUE);
                                string value = "";
                                if (valueNode != null)
                                {
                                    value = valueNode.Value;
                                }
                                if (key != "")
                                    customAttributes.Add(key, value);
                            }
                        }
                    }
                }

            }
        }

        private void parse(XmlNode unisensNode)
        {
            var attr = unisensNode.Attributes;
            var attrNode = attr.GetNamedItem(Constants.UNISENS_VERSION);
            version = (attrNode != null) ? attrNode.Value : null;
            attrNode = attr.GetNamedItem(Constants.UNISENS_MEASUREMENT_ID);
            measurementId = (attrNode != null) ? attrNode.Value : null;
            attrNode = attr.GetNamedItem(Constants.UNISENS_TIMESTAMP_START);
            timestampStart = (attrNode != null) ? Utilities.convertStringToDate(attrNode.Value) : DateTime.MinValue;
            attrNode = attr.GetNamedItem(Constants.UNISENS_DURATION);
            duration = (attrNode != null) ? double.Parse(attrNode.Value, System.Globalization.CultureInfo.InvariantCulture) : 0;
            attrNode = attr.GetNamedItem(Constants.UNISENS_COMMENT);
            comment = (attrNode != null) ? attrNode.Value : null;

            return;
        }

        internal XmlElement createElement(XmlDocument document)
        {
            document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", ""));
            var unisensElement = document.CreateElement(Constants.UNISENS, "http://www.unisens.org/unisens2.0");
            unisensElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            unisensElement.SetAttribute("xsi:schemaLocation", "http://www.unisens.org/unisens2.0 http://www.unisens.org/unisens2.0/unisens.xsd");
            //.Attributes.Append(document.CreateAttribute("http://www.w3.org/2001/XMLSchema-instance",);
            //"xsi:schemaLocation",
            //"http://www.unisens.org/unisens2.0 http://www.unisens.org/unisens2.0/unisens.xsd")));

            if (getComment() != null)
                unisensElement.SetAttribute(Constants.UNISENS_COMMENT, getComment());
            if (getDuration() != 0)
                unisensElement.SetAttribute(Constants.UNISENS_DURATION, "" + getDuration());
            if (getTimestampStart() != null)
                unisensElement.SetAttribute(Constants.UNISENS_TIMESTAMP_START,
                                                                          Utilities.convertDateToString(timestampStart));

            ;
            if (getMeasurementId() != null)
                unisensElement.SetAttribute(Constants.UNISENS_MEASUREMENT_ID, getMeasurementId());
            if (getVersion() != null)
                unisensElement.SetAttribute(Constants.UNISENS_VERSION, getVersion());

            if (customAttributes.Count > 0)
            {
                XmlElement customAttributesElement = document.CreateElement(Constants.CUSTOM_ATTRIBUTES, unisensElement.NamespaceURI);
                foreach (string key in customAttributes.Keys)
                {
                    XmlElement customAttributeElement = document.CreateElement(Constants.CUSTOM_ATTRIBUTE);
                    customAttributeElement.SetAttribute(Constants.CUSTOM_ATTRIBUTE_KEY, key);
                    customAttributeElement.SetAttribute(Constants.CUSTOM_ATTRIBUTE_VALUE, customAttributes[key]);
                    customAttributesElement.AppendChild(customAttributeElement);
                }
                unisensElement.AppendChild(customAttributesElement);
            }

            if (context != null)
            {
                XmlElement contextElement = document.CreateElement(Constants.CONTEXT, unisensElement.NamespaceURI);
                if (context.getSchemaUrl() != null)
                    contextElement.SetAttribute(Constants.CONTEXT_SCHEMAURL, context.getSchemaUrl());
                unisensElement.AppendChild(contextElement);
            }

            Entry entry;

            foreach (Entry t in entries)
            {
                entry = (Entry)t;
                XmlElement entryElement = null;

                if (entry is SignalEntry)
                {
                    entryElement = document.CreateElement(Constants.SIGNALENTRY, unisensElement.NamespaceURI);
                }
                if (entry is ValuesEntry)
                {
                    entryElement = document.CreateElement(Constants.VALUESENTRY, unisensElement.NamespaceURI);
                }
                if (entry is EventEntry)
                {
                    entryElement = document.CreateElement(Constants.EVENTENTRY, unisensElement.NamespaceURI);
                }
                if (entry is CustomEntry)
                {
                    entryElement = document.CreateElement(Constants.CUSTOMENTRY, unisensElement.NamespaceURI);
                }

                if (entry.getComment() != null)
                    entryElement.SetAttribute(Constants.ENTRY_COMMENT, entry.getComment());
                if (entry.getContentClass() != null)
                    entryElement.SetAttribute(Constants.ENTRY_CONTENTCLASS, entry.getContentClass());
                if (entry.getSource() != null)
                    entryElement.SetAttribute(Constants.ENTRY_SOURCE, entry.getSource());
                if (entry.getSourceId() != null)
                    entryElement.SetAttribute(Constants.ENTRY_SOURCE_ID, entry.getSourceId());
                if (entry.getId() != null)
                    entryElement.SetAttribute(Constants.ENTRY_ID, entry.getId());

                if (entry.getCustomAttributes().Count > 0)
                {
                    XmlElement customAttributesElement = document.CreateElement(Constants.CUSTOM_ATTRIBUTES, unisensElement.NamespaceURI);
                    foreach (string key in customAttributes.Keys)
                    {
                        var customAttributeElement = document.CreateElement(Constants.CUSTOM_ATTRIBUTE);
                        customAttributeElement.SetAttribute(Constants.CUSTOM_ATTRIBUTE_KEY, key);
                        customAttributeElement.SetAttribute(Constants.CUSTOM_ATTRIBUTE_VALUE, customAttributes[key]);
                        customAttributesElement.AppendChild(customAttributeElement);
                    }
                    unisensElement.AppendChild(customAttributesElement);
                }

                FileFormat fileFormat = entry.getFileFormat();
                XmlElement fileFormatElement = null;

                if (fileFormat is BinFileFormat)
                {
                    BinFileFormat binFileFormat = (BinFileFormat)fileFormat;
                    fileFormatElement = document.CreateElement(Constants.BINFILEFORMAT, unisensElement.NamespaceURI);
                    fileFormatElement.SetAttribute(Constants.BINFILEFORMAT_ENDIANESS, binFileFormat.getEndianess().ToString().ToUpper());
                }
                if (fileFormat is CsvFileFormat)
                {
                    CsvFileFormat csvFileFormat = (CsvFileFormat)fileFormat;
                    fileFormatElement = document.CreateElement(Constants.CSVFILEFORMAT, unisensElement.NamespaceURI);
                    fileFormatElement.SetAttribute(Constants.CSVFILEFORMAT_SEPARATOR, csvFileFormat.getSeparator());
                    fileFormatElement.SetAttribute(Constants.CSVFILEFORMAT_DECIMAL_SEPARATOR, csvFileFormat.getDecimalSeparator());
                }
                if (fileFormat is XmlFileFormat)
                {
                    fileFormatElement = document.CreateElement(Constants.XMLFILEFORMAT, unisensElement.NamespaceURI);
                }
                if (fileFormat is CustomFileFormat)
                {
                    CustomFileFormat customFileFormat = (CustomFileFormat)fileFormat;
                    fileFormatElement = document.CreateElement(Constants.CUSTOMFILEFORMAT, unisensElement.NamespaceURI);
                    fileFormatElement.SetAttribute(Constants.CUSTOMFILEFORMAT_FILEFORMATNAME, customFileFormat.getFileFormatName());
                    Dictionary<string, string> attributes = customFileFormat.getAttributes();
                    var attrNames = attributes.Keys;
                    foreach (string attrName in attrNames)
                    {
                        fileFormatElement.SetAttribute(attrName, attributes[attrName]);
                    }
                }
                if (fileFormat.getComment() != null)
                    fileFormatElement.SetAttribute(Constants.FILEFORMAT_COMMENT, fileFormat.getComment());

                entryElement.AppendChild(fileFormatElement);

                if (entry is MeasurementEntry)
                {
                    var measurementEntry = (MeasurementEntry)entry;
                    if (measurementEntry.getAdcResolution() != 0)
                        entryElement.SetAttribute(Constants.MEASUREMENTENTRY_ADCRESOLUTION, "" + measurementEntry.getAdcResolution());
                    if (measurementEntry.getAdcZero() != 0)
                        entryElement.SetAttribute(Constants.MEASUREMENTENTRY_ADCZERO, "" + measurementEntry.getAdcZero());
                    if (measurementEntry.getBaseline() != 0)
                        entryElement.SetAttribute(Constants.MEASUREMENTENTRY_BASELINE, "" + measurementEntry.getBaseline());
                    entryElement.SetAttribute(Constants.MEASUREMENTENTRY_DATATYPE, measurementEntry.getDataType().ToString().ToLower());
                    if (measurementEntry.getLsbValue() != 0)
                        entryElement.SetAttribute(Constants.MEASUREMENTENTRY_LSBVALUE, measurementEntry.getLsbValue().ToString(decimalFormat));
                    if (measurementEntry.getSampleRate() != 0)
                        entryElement.SetAttribute(Constants.TIMEDENTRY_SAMPLERATE, measurementEntry.getSampleRate().ToString(decimalFormat));
                    if (measurementEntry.getUnit() != null)
                        entryElement.SetAttribute(Constants.MEASUREMENTENTRY_UNIT, measurementEntry.getUnit());

                    string[] channelNames = measurementEntry.getChannelNames();

                    foreach (string t1 in channelNames)
                    {
                        XmlElement channelElement = document.CreateElement(Constants.CHANNEL, unisensElement.NamespaceURI);
                        channelElement.SetAttribute(Constants.CHANNEL_NAME, t1);
                        entryElement.AppendChild(channelElement);
                    }
                }
                if (entry is EventEntry)
                {
                    EventEntry eventEntry = (EventEntry)entry;
                    if (eventEntry.getSampleRate() != 0)
                        entryElement.SetAttribute(Constants.TIMEDENTRY_SAMPLERATE, eventEntry.getSampleRate().ToString(decimalFormat));
                    if (eventEntry.getTypeLength() != 0)
                        entryElement.SetAttribute(Constants.EVENTENTRY_TYPE_LENGTH, "" + eventEntry.getTypeLength());
                    if (eventEntry.getCommentLength() != 0)
                        entryElement.SetAttribute(Constants.EVENTENTRY_COMMENT_LENGTH, "" + eventEntry.getCommentLength());
                }

                if (entry is CustomEntry)
                {
                    CustomEntry customEntry = (CustomEntry)entry;
                    Dictionary<string, string> attributes = customEntry.getAttributes();
                    ICollection<string> keySet = attributes.Keys;
                    foreach (String key in keySet)
                    {
                        entryElement.SetAttribute(key, attributes[key]);
                    }
                }

                unisensElement.AppendChild(entryElement);
            }
            Group group;
            XmlElement groupElement;
            XmlElement groupEntryElement;

            foreach (Group t in groups)
            {
                group = (Group)t;
                groupElement = document.CreateElement(Constants.GROUP, unisensElement.NamespaceURI);

                if (group.getComment() != null)
                    groupElement.SetAttribute(Constants.GROUP_COMMENT, group.getComment());
                if (group.getId() != null)
                    groupElement.SetAttribute(Constants.GROUP_ID, group.getId());

                var entriList = group.getEntries();
                foreach (Entry e in entriList)
                {
                    groupEntryElement = document.CreateElement(Constants.GROUPENTRY, unisensElement.NamespaceURI);
                    groupEntryElement.SetAttribute(Constants.GROUPENTRY_REF, ((Entry)e).getId());
                    groupElement.AppendChild(groupEntryElement);
                }
                unisensElement.AppendChild(groupElement);
            }
            return unisensElement;
        }


        public void save()
        {
            var document = new XmlDocument();// factory.newDocumentBuilder().newDocument();
            //try
            //{
                //File file = new File(absoluteFileName);
                //DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
                //factory.setNamespaceAware(true);
                //document.Load(absoluteFileName);

                XmlElement unisensElement = createElement(document);
                document.AppendChild(unisensElement);

                string validate = UserSettings.Default[Constants.PROPERTIE_VALIDATION].ToString();
                if (validate != null && validate.ToLower().Equals("true"))
                    this.validate(document);

                document.Save(absoluteFileName);
                //var outFile = File.Open(absoluteFileName, FileMode.OpenOrCreate);
                //XmlWriter writer = XmlWriter.Create(outFile);

                //var transformer = new XslCompiledTransform();
                //transformer.OutputSettings.Indent = true;
                ////transformer.OutputSettings.OutputMethod = XmlOutputMethod.Xml; 
                ////transformer.setOutputProperty(OutputKeys.VERSION, "1.0");
                //transformer.OutputSettings.OmitXmlDeclaration = false;
                //transformer.OutputSettings.Encoding = Encoding.UTF8;

                //transformer.Transform(document.CreateNavigator(), writer);

                //outFile.Flush();
                //outFile.Close();

            //}
            //catch (Exception pce)
            //{
            //    pce.printStackTrace();
            //    throw;
            //}
        }

        public void closeAll()
        {
            foreach (Entry entry in entries)
                entry.close();
        }

        internal void sealedize()
        {
            closeAll();        // close open files
        }

        private void deleteFile(string absoluteFileName)
        {
            if (File.Exists(absoluteFileName))
                File.Delete(absoluteFileName);
        }

        private void validate(XmlDocument document)
        {
            //try
            //{
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("org.unisens.unisens.xsd"))
                {
                    XmlSchema schema = XmlSchema.Read(XmlReader.Create(stream), null);
                    document.Schemas.Add(schema);
                    document.Validate(settings_ValidationEventHandler);
                }
            //}
            //catch (XmlSchemaValidationException e)
            //{
            //    e.printStackTrace();
            //}

            //var schemaFactory = SchemaFactory.newInstance(XmlConstants.W3C_XML_SCHEMA_NS_URI);	
            //    var schemaUrl = Unisens.class.getClassLoader().getResource("unisens.xsd");
            //    Schema schema = schemaFactory.newSchema(schemaUrl);
            //    Validator validator = schema.newValidator();
            //    validator.setErrorHandler(new UnisensErrorHandler());
            //    validator.validate(new DOMSource(document));
            //}catch (SAXException saxe) {
            //    saxe.printStackTrace();
            //}catch (IOException ioe) {
            //    ioe.printStackTrace();
            //}
        }

        static void settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            //throw e.Exception;
        }
    }
}

