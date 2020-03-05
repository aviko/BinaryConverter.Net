using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestSystemTypes
{
    [TestClass]
    public class GuidTests
    {
        [TestMethod]
        public void Test_Default()
        {

            var val = default(Guid);
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Guid>(buf);

            Assert.AreEqual(val, cloned);
        }
        
        [TestMethod]
        public void Test_Empty() //same as default...
        {

            var val = Guid.Empty;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Guid>(buf);

            Assert.AreEqual(val, cloned);
        }


        [TestMethod]
        public void Test_RandomNewGuid()
        {

            var val = Guid.NewGuid();
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Guid>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Constants()
        {

            var val = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4");
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Guid>(buf);

            Assert.AreEqual(val, cloned);
        }

    }
}
