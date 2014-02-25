
using System;
using System.Collections.Generic;
using System.Xml;
using org.unisens.ri.config;

namespace org.unisens.ri
{
    public abstract class MeasurementEntryImpl : TimedEntryImpl, MeasurementEntry
    {
        internal int adcResolution = 0;
        internal int adcZero = 0;
        internal int baseline = 0;
        internal string[] channelNames = null;
        internal DataType dataType;
        internal double lsbValue = 0;
        internal string unit = null;

        internal MeasurementEntryImpl(Unisens unisens, XmlNode entryNode)
            : base(unisens, entryNode)
        {
            parse(entryNode);
        }

        public MeasurementEntryImpl(Unisens unisens, string id, string[] channelNames, DataType dataType, double sampleRate)
            : base(unisens, id, sampleRate)
        {

            this.channelNames = channelNames;
            this.dataType = dataType;
        }

        internal MeasurementEntryImpl(MeasurementEntry measurementEntry)
            : base(measurementEntry)
        {

            adcResolution = measurementEntry.getAdcResolution();
            adcZero = measurementEntry.getAdcZero();
            baseline = measurementEntry.getBaseline();
            channelNames = (string[])measurementEntry.getChannelNames().Clone();
            dataType = measurementEntry.getDataType();
            lsbValue = measurementEntry.getLsbValue();
            unit = measurementEntry.getUnit();
        }


        private void parse(XmlNode measurementNode)
        {
            var attrs = measurementNode.Attributes;
            var attrNode = attrs.GetNamedItem(Constants.MEASUREMENTENTRY_ADCRESOLUTION);
            adcResolution = (attrNode != null) ? int.Parse(attrNode.Value) : 0;
            attrNode = attrs.GetNamedItem(Constants.MEASUREMENTENTRY_ADCZERO);
            adcZero = (attrNode != null) ? int.Parse(attrNode.Value) : 0;
            attrNode = attrs.GetNamedItem(Constants.MEASUREMENTENTRY_BASELINE);
            baseline = (attrNode != null) ? int.Parse(attrNode.Value) : 0;
            attrNode = attrs.GetNamedItem(Constants.MEASUREMENTENTRY_LSBVALUE);
            lsbValue = (attrNode != null) ? double.Parse(attrNode.Value, System.Globalization.CultureInfo.InvariantCulture) : 0;

            attrNode = attrs.GetNamedItem(Constants.MEASUREMENTENTRY_DATATYPE);
            if (attrNode != null)
            {
                // What's about this change? DataType is saved in lower case letters in the xml file, but it's upper case in the ENUM.
//                dataType = (DataType)Enum.Parse(typeof(DataType), attrNode.Value.ToLower());
                dataType = (DataType)Enum.Parse(typeof(DataType), attrNode.Value.ToUpper());
                //Enum.TryParse(attrNode.Value, out dataType);
            }
            attrNode = attrs.GetNamedItem(Constants.MEASUREMENTENTRY_UNIT);
            unit = (attrNode != null) ? attrNode.Value : null;

            var childNodes = measurementNode.ChildNodes;
            XmlNode childNode = null;
            var channelNames = new List<string>();
            for (int i = 0; i < childNodes.Count; i++)
            {
                childNode = childNodes.Item(i);
                if ((childNode.NodeType == XmlNodeType.Element) && childNode.Name.Equals(Constants.CHANNEL, StringComparison.CurrentCultureIgnoreCase))
                    channelNames.Add(childNode.Attributes.GetNamedItem(Constants.CHANNEL_NAME).Value);
            }

            object[] o = channelNames.ToArray();
            this.channelNames = new string[o.Length];
            for (int i = 0; i < o.Length; i++)
                this.channelNames[i] = (string)o[i];


            return;
        }

        public int getAdcResolution()
        {
            return adcResolution;
        }

        public int getAdcZero()
        {
            return adcZero;
        }

        public int getBaseline()
        {
            return baseline;
        }

        public int getChannelCount()
        {
            return channelNames.Length;
        }

        public string[] getChannelNames()
        {
            return channelNames;
        }

        public DataType getDataType()
        {
            return dataType;
        }

        public double getLsbValue()
        {
            return lsbValue;
        }

        public string getUnit()
        {
            return unit;
        }

        public abstract void resetPos();


        public void setAdcResolution(int adcResolution)
        {
            this.adcResolution = adcResolution;
        }

        public void setAdcZero(int adcZero)
        {
            this.adcZero = adcZero;
        }

        public void setBaseline(int baseline)
        {
            this.baseline = baseline;
        }

        public void setChannelNames(string[] channelNames)
        {
            this.channelNames = channelNames;
        }

        public void setDataType(DataType dataType)
        {
            this.dataType = dataType;
        }

        public void setLsbValue(double lsbValue)
        {
            this.lsbValue = lsbValue;
        }

        public void setUnit(string unit)
        {
            this.unit = unit;
        }

        public void setAdcProperties(int adcZero, int adcResolution, int baseline, double lsbValue)
        {
            this.adcZero = adcZero;
            this.adcResolution = adcResolution;
            this.baseline = baseline;
            this.lsbValue = lsbValue;
        }
    }
}
