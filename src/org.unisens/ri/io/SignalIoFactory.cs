using org.unisens.ri.config;

namespace org.unisens.ri.io
{
    public class SignalIoFactory
    {
        public static SignalReader createSignalReader(SignalEntry signalEntry)
        {
            string readerClassName = UserSettings.Default[Constants.SIGNAL_READER.Replace("format", signalEntry.getFileFormat().getFileFormatName().ToLower())].ToString();
            if (readerClassName != null)
            {
                //try
                //{
                    var ass = typeof(SignalIoFactory).Assembly;
                    var readerClass = ass.GetType(readerClassName);
                    var constructor = readerClass.GetConstructor(new[] { typeof(SignalEntry) });
                    return constructor.Invoke(new[] { signalEntry }) as SignalReader;

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


        public static SignalWriter createSignalWriter(SignalEntry signalEntry)
        {
            string readerClassName = UserSettings.Default[Constants.SIGNAL_WRITER.Replace("format", signalEntry.getFileFormat().getFileFormatName().ToLower())].ToString();
            if (readerClassName != null)
            {
                //try
                //{
                    var ass = typeof(SignalIoFactory).Assembly;
                    var readerClass = ass.GetType(readerClassName);
                    var constructor = readerClass.GetConstructor(new[] { typeof(SignalEntry) });
                    return constructor.Invoke(new[] { signalEntry }) as SignalWriter;
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