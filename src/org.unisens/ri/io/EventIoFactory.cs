using org.unisens.ri.config;


namespace org.unisens.ri.io
{

    public class EventIoFactory
    {


        public static EventReader createEventReader(EventEntry eventEntry)
        {
            string readerClassName =
                UserSettings.Default[
                    Constants.EVENT_READER.Replace("format", eventEntry.getFileFormat().getFileFormatName().ToLower())].
                    ToString();
            if (readerClassName != null)
            {
                //try
                //{
                    var ass = typeof(EventIoFactory).Assembly;
                    var readerClass = ass.GetType(readerClassName);
                    var constructor = readerClass.GetConstructor(new[] { typeof(EventEntry) });
                    return constructor.Invoke(new[] { eventEntry }) as EventReader;
            //    }
            //    catch (FileLoadException e)
            //    {
            //        Console.WriteLine("Class (" + readerClassName + ") could not be accessed!");
            //        e.printStackTrace();
            //    }
            //    catch (FileNotFoundException e)
            //    {
            //        Console.WriteLine("Class (" + readerClassName + ") could not be found!");
            //        e.printStackTrace();
            //    }
            //    catch (TargetInvocationException e)
            //    {
            //        Console.WriteLine("Class (" + readerClassName + ") could not be instantiated!");
            //        e.printStackTrace();
            //    }
            //    catch (Exception e)
            //    {
            //        e.printStackTrace();
            //    }

            }
            return null;
        }


        public static EventWriter createEventWriter(EventEntry eventEntry)
        {
            string readerClassName = UserSettings.Default[Constants.EVENT_WRITER.Replace("format", eventEntry.getFileFormat().getFileFormatName().ToLower())].ToString();
            if (readerClassName != null)
            {
                //try
                //{
                    var ass = typeof(EventIoFactory).Assembly;
                    var readerClass = ass.GetType(readerClassName);
                    var constructor = readerClass.GetConstructor(new[] { typeof(EventEntry) });
                    return constructor.Invoke(new[] { eventEntry }) as EventWriter;
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