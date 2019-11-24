using BinaryConverter.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestSystemTypes
{
    [TestClass]
    public class DateTimeTests
    {
        [TestMethod]
        public void Test_DateTimeTickResolution()
        {
            var settings = new SerializerSettings();
            settings.SerializerArgMap[typeof(DateTime)] = new DateTimeSerializerArg() { TickResolution = 1 };

            var val = DateTime.Now;
            var buf = BinaryConvert.SerializeObject(val, settings);
            var cloned = BinaryConvert.DeserializeObject<DateTime>(buf, settings);

            Assert.AreEqual(val, cloned);
            Assert.AreEqual(buf.Length, 9); //better not to use 7-bit
        }

        [TestMethod]
        public void Test_DateTimeNullableWithValue()
        {
            var settings = new SerializerSettings();
            settings.SerializerArgMap[typeof(DateTime)] = new DateTimeSerializerArg() { TickResolution = 1 };

            DateTime? val = DateTime.Now;
            var buf = BinaryConvert.SerializeObject(val, settings);
            var cloned = BinaryConvert.DeserializeObject<DateTime?>(buf, settings);

            Assert.AreEqual(val, cloned);
            Assert.AreEqual(buf.Length, 10); //better not to use 7-bit
        }

        [TestMethod]
        public void Test_DateTimeNullableWithNull()
        {
            var settings = new SerializerSettings();
            settings.SerializerArgMap[typeof(DateTime)] = new DateTimeSerializerArg() { TickResolution = 1 };

            DateTime? val = null;
            var buf = BinaryConvert.SerializeObject(val, settings);
            var cloned = BinaryConvert.DeserializeObject<DateTime?>(buf, settings);

            Assert.AreEqual(val, cloned);
            Assert.AreEqual(buf.Length, 1); //better not to use 7-bit
        }

        [TestMethod]
        public void Test_DateTimeSecondResolution()
        {
            var settings = new SerializerSettings();
            settings.SerializerArgMap[typeof(DateTime)] = new DateTimeSerializerArg() { TickResolution = TimeSpan.TicksPerSecond };
            var val = new DateTime(2010, 10, 10, 10, 10, 10);

            var buf = BinaryConvert.SerializeObject(val, settings);
            var cloned = BinaryConvert.DeserializeObject<DateTime>(buf, settings);

            Assert.AreEqual(val, cloned);
            Assert.AreEqual(buf.Length, 6);
        }

        [TestMethod]
        public void Test_DateTimeDayResolution()
        {
            var settings = new SerializerSettings();
            settings.SerializerArgMap[typeof(DateTime)] = new DateTimeSerializerArg() { TickResolution = TimeSpan.TicksPerDay};
            var val = new DateTime(2010, 10, 10);

            var buf = BinaryConvert.SerializeObject(val, settings);
            var cloned = BinaryConvert.DeserializeObject<DateTime>(buf, settings);

            Assert.AreEqual(val, cloned);
            Assert.AreEqual(buf.Length, 3);
        }
    }
}
