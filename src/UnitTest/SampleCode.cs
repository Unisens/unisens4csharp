using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;
using System.IO;

namespace UnisensUnitTest
{
    public class SampleCode
    {
        public static void main(String[] args)
        {
            new SampleCode();
        }

        public SampleCode()
        {
            Console.WriteLine("start.");
            try
            {
                //			signalCsvDouble();
                signalBin();
                signalCsv();
                signalXml();

                valuesBin();
                valuesCsv();
                valuesXml();

                eventBin();
                eventCsv();
                eventXml();
            }
            catch (UnisensParseException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
            }
            catch (DuplicateIdException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("done.");
        }

        private void signalBin()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            SignalEntry se = u.createSignalEntry("signal.bin", new String[] { "A", "B" }, DataType.INT16, 250);
            short[][] A = new short[][] { new short[] { 1, 2, 3 }, new short[] { 4, 5, 6 } };
            se.append(A);
            u.save();
            u.closeAll();
        }


        private void signalXml()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            SignalEntry se = u.createSignalEntry("signal.xml", new String[] { "A", "B" }, DataType.INT16, 250);
            short[][] A = new short[][] { new short[] { 1, 2, 3 }, new short[] { 4, 5, 6 } };
            se.setFileFormat(se.createXmlFileFormat());
            se.append(A);
            u.save();
            u.closeAll();
        }

        private void signalCsv()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            SignalEntry se = u.createSignalEntry("signal.csv", new String[] { "A", "B" }, DataType.INT16, 250);
            short[][] A = new short[][] { new short[] { 1, 2, 3 }, new short[] { 4, 5, 6 } };
            CsvFileFormat cff = se.createCsvFileFormat();
            cff.setSeparator(";");
            cff.setDecimalSeparator(",");
            se.setFileFormat(cff);
            se.append(A);
            u.save();
            u.closeAll();
        }

        private void signalCsvDouble()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            SignalEntry se = u.createSignalEntry("signalD.csv", new String[] { "A", "B" }, DataType.DOUBLE, 250);
            double[][] A = new double[][] { new double[] { 1.3, 2.5, 3.2 }, new double[] { 4.3, 5.0, 6.123456 } };
            CsvFileFormat cff = se.createCsvFileFormat();
            cff.setSeparator(";");
            cff.setDecimalSeparator(",");
            se.setFileFormat(cff);
            se.append(A);
            u.save();
            u.closeAll();
        }

        private void valuesCsv()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            ValuesEntry ve = u.createValuesEntry("values.csv", new String[] { "A", "B" }, DataType.INT16, 250);
            CsvFileFormat cff = ve.createCsvFileFormat();
            cff.setSeparator(";");
            cff.setDecimalSeparator(".");
            ve.setFileFormat(cff);
            ve.append(new Value(1320, new short[] { 1, 4 }));
            ve.append(new Value(22968, new short[] { 2, 5 }));
            ve.append(new Value(30232, new short[] { 3, 6 }));
            u.save();
            u.closeAll();
        }

        private void valuesBin()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            ValuesEntry ve = u.createValuesEntry("values.bin", new String[] { "A", "B" }, DataType.INT16, 250);
            ve.setFileFormat(ve.createBinFileFormat());
            ve.append(new Value(1320, new short[] { 1, 4 }));
            ve.append(new Value(22968, new short[] { 2, 5 }));
            ve.append(new Value(30232, new short[] { 3, 6 }));
            u.save();
            u.closeAll();
        }

        private void valuesXml()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            ValuesEntry ve = u.createValuesEntry("values.xml", new String[] { "A", "B" }, DataType.INT16, 250);
            ve.setFileFormat(ve.createXmlFileFormat());
            ve.append(new Value(1320, new short[] { 1, 4 }));
            ve.append(new Value(22968, new short[] { 2, 5 }));
            ve.append(new Value(30232, new short[] { 3, 6 }));
            u.save();
            u.closeAll();
        }

        private void eventCsv()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            EventEntry ee = u.createEventEntry("event.csv", 1000);
            CsvFileFormat cff = ee.createCsvFileFormat();
            cff.setSeparator(";");
            cff.setDecimalSeparator(".");
            ee.setFileFormat(cff);
            ee.append(new Event(124, "N", "NORMAL"));
            ee.append(new Event(346, "N", "NORMAL"));
            ee.append(new Event(523, "V", "PVC"));
            u.save();
            u.closeAll();
        }

        private void eventXml()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            EventEntry ee = u.createEventEntry("event.xml", 1000);
            ee.setFileFormat(ee.createXmlFileFormat());
            ee.append(new Event(124, "N", "NORMAL"));
            ee.append(new Event(346, "N", "NORMAL"));
            ee.append(new Event(523, "V", "PVC"));
            u.save();
            u.closeAll();
        }

        private void eventBin()
        {
            UnisensFactory uf = UnisensFactoryBuilder.createFactory();
            Unisens u = uf.createUnisens("C:\\TestData");
            EventEntry ee = u.createEventEntry("event.bin", 1000);
            ee.setFileFormat(ee.createBinFileFormat());
            ee.setCommentLength(6);
            ee.setTypeLength(1);
            ee.append(new Event(124, "N", "NORMAL"));
            ee.append(new Event(346, "N", "NORMAL"));
            ee.append(new Event(523, "V", "PVC   "));
            u.save();
            u.closeAll();
        }
    }
}