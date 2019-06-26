using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestSystemTypes
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void Test_String()
        {
            var val = "Hello World";
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<string>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_StringNull()
        {
            string val = null;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<string>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_StringNonGeneric()
        {
            var val = "Hello World";
            var buf = BinaryConvert.SerializeObject(typeof(string), val);
            var cloned = BinaryConvert.DeserializeObject(typeof(string), buf);
            Assert.AreEqual(val, cloned);
        }
    }
}
