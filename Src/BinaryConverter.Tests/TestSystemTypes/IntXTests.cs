using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestSystemTypes
{
    [TestClass]
    public class IntXTests
    {
        //----------------------------------------------------------------
        [TestMethod]
        public void Test_Int32()
        {
            int val = 1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int32Negative()
        {
            int val = -1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int32Max()
        {
            int val = Int32.MaxValue;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int32Min()
        {
            int val = Int32.MinValue;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int32NullableWithValue()
        {
            int? val = Int32.MinValue;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int?>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int32NullableWithNull()
        {
            int? val = null;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int?>(buf);
            Assert.AreEqual(val, cloned);
        }

        //----------------------------------------------------------------

        [TestMethod]
        public void Test_UInt32()
        {
            uint val = 1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<uint>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int64()
        {
            Int64 val = 1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Int64>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int64Max()
        {
            Int64 val = Int64.MaxValue;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Int64>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int64Min()
        {
            Int64 val = Int64.MinValue;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Int64>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_BooleanTrue()
        {
            Boolean val = true;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Boolean>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_BooleanFalse()
        {
            Boolean val = false;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Boolean>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Byte()
        {
            Byte val = 12;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Byte>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_SByte()
        {
            SByte val = -12;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<SByte>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Int16()
        {
            Int16 val = 1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<Int16>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_UInt16()
        {
            UInt16 val = 1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<UInt16>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_UInt64()
        {
            UInt64 val = 1234;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<UInt64>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_UInt64Max()
        {
            UInt64 val = UInt64.MaxValue;
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<UInt64>(buf);
            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_Char()
        {
            char val = 'x';
            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<char>(buf);
            Assert.AreEqual(val, cloned);
        }
    }
}

