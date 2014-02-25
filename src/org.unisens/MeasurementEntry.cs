
/**
 * MeasurementEntry is the superclass of SignalEntry and ValuesEntry and defines the common methods.
 * Direct instances of MeasurementEntry are normally not used.
 *  
 * @author Joerg Ottenbacher
 * @author Radi Nedkov
 * @author Malte Kirst
 *
 */
using System;

namespace org.unisens
{
    public interface MeasurementEntry : TimedEntry
    {

        /**
         * Gets the names of all channels as Array of String
         * 
         * @return the names of all channels
         */
        String[] getChannelNames();

        /**
         * Gets the resolution in bits of the ADC used to acquire the data contained in this Entry
         * 
         * @return the resolution in bits
         */
        int getAdcResolution();

        /**
         * Sets the resolution in bits of the ADC used to acquire the data contained in this Entry
         * @param adcResolution the resolution in bits
         */
        void setAdcResolution(int adcResolution);

        /**
         * Gets the integer value given by the ADC, when the input falls exactly at the center of the ADC range 
         * For bipolar ADCs with two's complement output adczero is usually zero.
         * 
         * @return the value given by the ADC, when the input falls exactly at the center of the ADC range
         */
        int getAdcZero();

        /**
         * Sets the integer value given by the ADC, when the input falls exactly at the center of the ADC range 
         * For bipolar ADCs with two's complement output adczero is usually zero.
         * 
         * @param adcZero the value given by the ADC, when the input falls exactly at the center of the ADC range
         */
        void setAdcZero(int adcZero);

        /**
         * Gets the value of ADC output that would map to the value of 0 of the physical variable acquired 
         * by the ADC. This value can be beyond the adc output range. The value of the physical variable
         * is calculated by value = (ADCout + baseline) * lsbValue.
         * 
         * @return the value of ADC output that would map to the value of 0 of the physical variable
         */
        int getBaseline();

        /**
         * Sets the value of ADC output that would map to the value of 0 of the physical variable acquired 
         * by the ADC. This value can be beyond the adc output range. The value of the physical variable
         * is calculated by value = (ADCout + baseline) * lsbValue.
         * 
         * @param baseline the value of ADC output that would map to the value of 0 of the physical variable
         */
        void setBaseline(int baseline);

        /**
         * Gets the equivalent value of the physical variable represented by the least significant bit of the ADC
         * used to acquire the data contained in this Entry
         * 
         * @return the value of the physical variable represented by the least significant bit of the ADC
         */
        double getLsbValue();

        /**
         * Sets the equivalent value of the physical variable represented by the least significant bit of the ADC
         * used to acquire the data contained in this Entry
         * 
         * @param lsbValue the value of the physical variable represented by the least significant bit of the ADC
         */
        void setLsbValue(double lsbValue);

        /**
         * Gets the string that specifies the physical unit of the acquired variable(s)
         * 
         * @return the unit
         */
        String getUnit();

        /**
         * Sets the string that specifies the physical unit of the acquired variable(s)
         * 
         * @param unit the unit
         */
        void setUnit(String unit);

        /**
         * Gets the number of channels in this Entry
         * 
         * @return the number of Channels
         */
        int getChannelCount();

        /**
         * Sets the names of the channels in this Entry. The number of channels cannot be changed 
         * after a channel is created
         * 
         * @param channelNames the names of the channels as Array of Strings
         */
        void setChannelNames(String[] channelNames);

        /**
         * Reset the file pointer of this Entry. The next read will read from the beginning of the data file
         */
        void resetPos();

        /**
         * Gets the DataType of the data contained in this Entry
         * 
         * @return the DataType
         */
        DataType getDataType();

        /**
         * Sets the DataType of the data contained in this Entry
         * 
         * @param dataType the DataType
         */
        void setDataType(DataType dataType);

        /**
         * Sets the properties of the ADC used to acquire the data contained in this Entry.
         * 
         * @param adcZero the value given by the ADC, when the input falls exactly at the center of the ADC range
         * @param adcResolution the resolution in bits
         * @param baseline the value of ADC output that would map to the value of 0 of the physical variable
         * @param lsbValue the value of the physical variable represented by the least significant bit of the ADC
         */
        void setAdcProperties(int adcZero, int adcResolution, int baseline, double lsbValue);
    }
}