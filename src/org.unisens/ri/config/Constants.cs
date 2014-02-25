using System;

namespace org.unisens.ri.config
{
    public class Constants
    {
        public static String UNISENS = "unisens";
        public static String CUSTOM_ATTRIBUTES = "customAttributes";
        public static String CUSTOM_ATTRIBUTE = "customAttribute";
        public static String CUSTOM_ATTRIBUTE_KEY = "key";
        public static String CUSTOM_ATTRIBUTE_VALUE = "value";
        public static String CONTEXT = "context";
        public static String ENTRY = "entry";
        public static String SIGNALENTRY = "signalEntry";
        public static String VALUESENTRY = "valuesEntry";
        public static String EVENTENTRY = "eventEntry";
        public static String CUSTOMENTRY = "customEntry";
        public static String CHANNEL = "channel";
        public static String GROUP = "group";
        public static String GROUPENTRY = "groupEntry";
        public static String XMLFILEFORMAT = "xmlFileFormat";
        public static String BINFILEFORMAT = "binFileFormat";
        public static String CSVFILEFORMAT = "csvFileFormat";
        public static String CUSTOMFILEFORMAT = "customFileFormat";

        public static String UNISENS_VERSION = "version";
        public static String UNISENS_MEASUREMENT_ID = "measurementId";
        public static String UNISENS_TIMESTAMP_START = "timestampStart";
        public static String UNISENS_DURATION = "duration";
        public static String UNISENS_COMMENT = "comment";

        public static String CONTEXT_SCHEMAURL = "schemaUrl";

        public static String ENTRY_ID = "id";
        public static String ENTRY_CONTENTCLASS = "contentClass";
        public static String ENTRY_SOURCE = "source";
        public static String ENTRY_SOURCE_ID = "sourceId";
        public static String ENTRY_COMMENT = "comment";

        public static String MEASUREMENTENTRY_ADCRESOLUTION = "adcResolution";
        public static String MEASUREMENTENTRY_UNIT = "unit";
        public static String MEASUREMENTENTRY_LSBVALUE = "lsbValue";
        public static String MEASUREMENTENTRY_ADCZERO = "adcZero";
        public static String MEASUREMENTENTRY_BASELINE = "baseline";
        public static String MEASUREMENTENTRY_DATATYPE = "dataType";

        public static String TIMEDENTRY_SAMPLERATE = "sampleRate";

        public static String EVENTENTRY_COMMENT_LENGTH = "commentLength";
        public static String EVENTENTRY_TYPE_LENGTH = "typeLength";

        public static String GROUP_ID = "id";
        public static String GROUP_COMMENT = "comment";

        public static String GROUPENTRY_REF = "ref";

        public static String CHANNEL_NAME = "name";

        public static String FILEFORMAT_COMMENT = "comment";

        public static String CSVFILEFORMAT_SEPARATOR = "separator";
        public static String CSVFILEFORMAT_DECIMAL_SEPARATOR = "decimalSeparator";

        public static String BINFILEFORMAT_ENDIANESS = "endianess";

        public static String CUSTOMFILEFORMAT_FILEFORMATNAME = "fileFormatName";

        public static String PATH_CONFIG = "config/unisens.cfg";
        public static String PATH_UNISENS_SCHEMA = "config/unisens.xsd";

        public static String SIGNAL_READER = "signal_format_reader";
        public static String SIGNAL_WRITER = "signal_format_writer";

        public static String EVENT_READER = "event_format_reader";
        public static String EVENT_WRITER = "event_format_writer";

        public static String VALUES_READER = "values_format_reader";
        public static String VALUES_WRITER = "values_format_writer";

        public static String CUSTOM_READER = "custom_format_reader";
        public static String CUSTOM_WRITER = "custom_format_writer";

        public static String PROPERTIE_VALIDATION = "validation";//"org.unisens.validation";
        public static string PROPERTIE_STANDARDUNISENSFACTORYCLASS = "StandardUnisensFactoryClass"; //org.unisens.StandardUnisensFactoryClass

        public static String SIGNAL_XML_READER_SAMPLES_PATH = "/signal/sample[ position() > {0} and position() < {1}]";
        public static String SIGNAL_XML_READER_SAMPLES_DATA_PATH = "./data/text()";
        public static String SIGNAL_XML_READER_SIGNAL_ELEMENT = "signal";
        public static String SIGNAL_XML_READER_SAMPLE_ELEMENT = "sample";
        public static String SIGNAL_XML_READER_DATA_ELEMENT = "data";

        public static String EVENT_XML_READER_EVENTS_PATH = "/events/event[ position() > {0} and position() < {1}]";
        public static String EVENT_XML_READER_EVENTS_ELEMENT = "events";
        public static String EVENT_XML_READER_EVENT_ELEMENT = "event";
        public static String EVENT_XML_READER_SAMPLESTAMP_ATTR = "samplestamp";
        public static String EVENT_XML_READER_TYPE_ATTR = "type";
        public static String EVENT_XML_READER_COMMENT_ATTR = "comment";

        public static String VALUES_XML_READER_VALUES_PATH = "/values/value[ position() > {0} and position() < {1}]";
        public static String VALUES_XML_READER_VALUES_DATA_PATH = "./data/text()";
        public static String VALUES_XML_READER_VALUES_ELEMENT = "values";
        public static String VALUES_XML_READER_VALUE_ELEMENT = "value";
        public static String VALUES_XML_READER_DATA_ELEMENT = "data";
        public static String VALUES_XML_READER_SAMPLESTAMP_ATTR = "sampleStamp";
    }
}