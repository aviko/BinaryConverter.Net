using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestCollections
{
    [TestClass]
    public class TuplesTests
    {
        [TestMethod]
        public void Test_Tuple_N1()
        {
            var val = new Tuple<int>(1);

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N2()
        {
            var val = new Tuple<int, string>(1, "1");

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N3()
        {
            var val = new Tuple<int, string, decimal>(1, "1", 1.23m);

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string, decimal>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N4()
        {
            var val = new Tuple<int, string, decimal, double>(1, "1", 1.23m, 3.3);

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string, decimal, double>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N5()
        {
            var val = new Tuple<int, string, decimal, double, string>(1, "1", 1.23m, 3.3, null);

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string, decimal, double, string>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N6()
        {
            var val = new Tuple<int, string, decimal, double, string, byte>(1, "1", 1.23m, 3.3, null, 7);

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string, decimal, double, string, byte>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N7()
        {
            var val = new Tuple<int, string, decimal, double, string, byte, DateTime>(1, "1", 1.23m, 3.3, null, 7, new DateTime(2010, 10, 10, 10, 10, 10));

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string, decimal, double, string, byte, DateTime>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Tuple_N8Rest()
        {
            var val = new Tuple<int, string, decimal, double, string, byte, DateTime, Tuple<int, string>>(1, "1", 1.23m, 3.3, null, 7, new DateTime(2010, 10, 10, 10, 10, 10), new Tuple<int, string>(2, "2"));

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Tuple<int, string, decimal, double, string, byte, DateTime, Tuple<int, string>>>(buf);

            Assert.AreEqual(val, cloned);
        }
    }
}
