using System;
using System.Reflection;
using org.unisens.ri.config;

namespace org.unisens
{


    /**
     * UnisensFactoryBuilder creates a UnisensFactory of the used unisens implementation. The
     * standard unisens implementation can be set by the system property 
     * "org.unisens.StandardUnisensFactoryClass". If no system property is set the 
     * reference implementation in the namespace "org.unisens.ri" is used as standard 
     * implementation.
     * 
     * @author Joerg Ottenbacher
     * @author Radi Nedkov
     * @author Malte Kirst
     *
     */
    public class UnisensFactoryBuilder
    {

        /**
         * Creates a UnisensFactory from the standard implementation. 
         * 
         * @return UnisensFactory
         */
        public static UnisensFactory createFactory()
        {
            string factoryClass = UserSettings.Default[Constants.PROPERTIE_STANDARDUNISENSFACTORYCLASS].ToString();
            if (factoryClass == null)
                factoryClass = "org.unisens.ri.UnisensFactoryImpl";
            //try
            //{
                Assembly ass = typeof(UnisensFactory).Assembly;
                var readerClass = ass.GetType(factoryClass);
                var constructor = readerClass.GetConstructor(new Type[0]);//new[] { typeof(ValuesEntry) });
                return constructor.Invoke(new object[0]) as UnisensFactory;
            //}
            //catch (FileLoadException e)
            //{
            //    Console.WriteLine("Class (" + factoryClass + ") could not be accessed!");
            //    e.printStackTrace();
            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine("Class (" + factoryClass + ") could not be found!");
            //    e.printStackTrace();
            //}
            //catch (TargetInvocationException e)
            //{
            //    Console.WriteLine("Class (" + factoryClass + ") could not be instantiated!");
            //    e.printStackTrace();
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}

            //return null;
        }

        /**
         * Creates a UnisensFactory specified by a given UnisensFactory class name. 
         * 
         * @param factoryClass name of the factory class
         * @return UnisensFactory
         */
        public static UnisensFactory createFactory(string factoryClass)
        {
            //try
            //{
                Assembly ass = typeof(UnisensFactory).Assembly;
                var readerClass = ass.GetType(factoryClass);
                var constructor = readerClass.GetConstructor(new[] { typeof(ValuesEntry) });
                return constructor.Invoke(new object[0]) as UnisensFactory;
            //}
            //catch (FileLoadException e)
            //{
            //    Console.WriteLine("Class (" + factoryClass + ") could not be accessed!");
            //    e.printStackTrace();
            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine("Class (" + factoryClass + ") could not be found!");
            //    e.printStackTrace();
            //}
            //catch (TargetInvocationException e)
            //{
            //    Console.WriteLine("Class (" + factoryClass + ") could not be instantiated!");
            //    e.printStackTrace();
            //}
            //catch (Exception e)
            //{
            //    e.printStackTrace();
            //}
            //return null;
        }
    }
}