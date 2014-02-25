using org.unisens.ri.config;

namespace org.unisens.ri.io
{
    public class ValuesIoFactory
    {
        public static ValuesReader createValuesReader(ValuesEntry valuesEntry)
        {
            string readerClassName = UserSettings.Default[Constants.VALUES_READER.Replace("format", valuesEntry.getFileFormat().getFileFormatName().ToLower())].ToString();
            if (readerClassName != null)
            {
                //try
                //{
                    var ass = typeof(ValuesIoFactory).Assembly;
                    var readerClass = ass.GetType(readerClassName);
                    var constructor = readerClass.GetConstructor(new[] { typeof(ValuesEntry) });
                    return constructor.Invoke(new[] { valuesEntry }) as ValuesReader;
                    //Class<ValuesReader> readerClass = (Class<ValuesReader>).forName(readerClassName);
                    //Constructor<ValuesReader> readerConstructor = readerClass.getConstructor(typeof(ValuesEntry));
                    //return readerConstructor.newInstance(valuesEntry);
                //}
                //catch (FileLoadException e)
                //{
                //    Console.WriteLine("Class (" + readerClassName + ") could not be accessed!");
                //    e.printStackTrace();
                //}
                //catch (FileNotFoundException e)
                //{
                //    Console.WriteLine("Class (" + readerClassName + ") could not be found!");
                //    e.printStackTrace();
                //}
                //catch (TargetInvocationException e)
                //{
                //    Console.WriteLine("Class (" + readerClassName + ") could not be instantiated!");
                //    e.printStackTrace();
                //}
                //catch (Exception e)
                //{
                //    e.printStackTrace();
                //}
            }
            return null;
        }


        public static ValuesWriter createValuesWriter(ValuesEntry valuesEntry)
        {
            string readerClassName = UserSettings.Default[Constants.VALUES_WRITER.Replace("format", valuesEntry.getFileFormat().getFileFormatName().ToLower())].ToString();
            if (readerClassName != null)
            {
                //try
                //{
                    var ass = typeof(ValuesIoFactory).Assembly;
                    var readerClass = ass.GetType(readerClassName);
                    var constructor = readerClass.GetConstructor(new[] { typeof(ValuesEntry) });
                    return constructor.Invoke(new[] { valuesEntry }) as ValuesWriter;

                    //Class<ValuesWriter> readerClass = (Class<ValuesWriter>)Class.forName(readerClassName);
                    //Constructor<ValuesWriter> readerConstructor = readerClass.getConstructor(ValuesEntry.class);
                    //return readerConstructor.newInstance(valuesEntry);

                //}
                //catch (FileLoadException e)
                //{
                //    Console.WriteLine("Class (" + readerClassName + ") could not be accessed!");
                //    e.printStackTrace();
                //}
                //catch (FileNotFoundException e)
                //{
                //    Console.WriteLine("Class (" + readerClassName + ") could not be found!");
                //    e.printStackTrace();
                //}
                //catch (TargetInvocationException e)
                //{
                //    Console.WriteLine("Class (" + readerClassName + ") could not be instantiated!");
                //    e.printStackTrace();
                //}
                //catch (Exception e)
                //{
                //    e.printStackTrace();
                //}
            }
            return null;
        }
    }
}