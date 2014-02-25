/*
Unisens Tests - tests for a universal sensor data format
Copyright (C) 2008 FZI Research Center for Information Technology, Germany
                   Institute for Information Processing Technology (ITIV),
				   KIT, Germany

This file is part of the Unisens Tests. For more information, see
<http://www.unisens.org>

The Unisens Tests is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Unisens Tests is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with the Unisens Tests. If not, see <http://www.gnu.org/licenses/>. 
*/
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using org.unisens;
using System.IO;
using org.unisens.ri.config;

namespace UnisensUnitTest
{
    [TestClass]
    public class CustomEntryImplTest : TestProperties
    {
        public static CustomEntry customEntry;
        public static Unisens unisens;
        public static UnisensFactory factory;

        public CustomEntryImplTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            String TestLocation = CopyExampleToTest(EXAMPLE2);
            factory = UnisensFactoryBuilder.createFactory();
            unisens = factory.createUnisens(TestLocation);
            customEntry = (CustomEntry)unisens.getEntry("entry.custom");
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            unisens.closeAll();
        }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void testGetAttribute()
        {
            Assert.AreEqual("customAttrValue1", customEntry.getAttribute("customAttr1"));
            Assert.IsNull(customEntry.getAttribute("unexpectedAttr"));
            Assert.IsNull(customEntry.getAttribute(Constants.ENTRY_CONTENTCLASS));
            Assert.IsNull(customEntry.getAttribute(Constants.ENTRY_ID));
            Assert.IsNull(customEntry.getAttribute(Constants.ENTRY_SOURCE));
            Assert.IsNull(customEntry.getAttribute(Constants.ENTRY_SOURCE_ID));
            Assert.IsNull(customEntry.getAttribute(Constants.ENTRY_COMMENT));
        }

        [TestMethod]
        public void testGetAttributes()
        {
            Dictionary<String, String> expected = new Dictionary<String, String>();
            expected.Add("customAttr1", "customAttrValue1");
            expected.Add("customAttr2", "customAttrValue2");
            Dictionary<String, String> actual = customEntry.getAttributes();
            assertDictionaryEquals(expected, actual);
        }

        [TestMethod]
        public void testGetFileFormat()
        {
            FileFormat fileFormat = customEntry.getFileFormat();
            Assert.IsTrue(fileFormat is CustomFileFormat);
        }

        [TestMethod]
        public void testClone()
        {
            CustomEntry clonedEntry = (CustomEntry)customEntry.clone<CustomEntry>();
            Assert.AreEqual(customEntry.getId(), clonedEntry.getId());
            Assert.AreEqual(customEntry.getComment(), clonedEntry.getComment());
            Assert.AreEqual(customEntry.getContentClass(), clonedEntry.getContentClass());
            Assert.AreEqual(customEntry.getSource(), clonedEntry.getSource());
            Assert.AreEqual(customEntry.getSourceId(), clonedEntry.getSourceId());
            Assert.AreEqual(customEntry.getFileFormat().getFileFormatName(), clonedEntry.getFileFormat().getFileFormatName());
            assertDictionaryEquals(customEntry.getAttributes(), clonedEntry.getAttributes());
        }
    }
}