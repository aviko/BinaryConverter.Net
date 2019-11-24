using BinaryConverter.Serializers;
using BinaryConverter.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestClasses
{
    [TestClass]
    public class PocosWithClassMapsTests
    {
        [TestMethod]
        public void Test_ClassMapIgnore1()
        {
            SerializerRegistry.RegisterClassMap(typeof(PocoSimple), new ClassMap<PocoSimple>(cm =>
            {
                cm.MapProperty(p => p.Int).Ignored = true;
            }));

            var val = new PocoSimple()
            {
                Int = 1,
                Str = "2",
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoSimple>(buf);

            Assert.AreEqual(val, cloned); //Note: comparer use class map, so ite return equal even that they are not!
            Assert.AreEqual(cloned.Int, 0);
            Assert.AreEqual(cloned.Str, "2");
        }

        [TestMethod]
        public void Test_ClassMapIgnore2()
        {
            SerializerRegistry.RegisterClassMap(typeof(PocoSimple), new ClassMap<PocoSimple>(cm =>
            {
                cm.MapProperty(p => p.Str).Ignored = true;
            }));

            var val = new PocoSimple()
            {
                Int = 1,
                Str = "2",
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoSimple>(buf);

            Assert.AreEqual(val, cloned); //Note: comparer use class map, so ite return equal even that they are not!
            Assert.AreEqual(cloned.Int, 1);
            Assert.AreEqual(cloned.Str, null);
        }

        [TestMethod]
        public void Test_ClassMapSerializerArgTime()
        {
            SerializerRegistry.RegisterClassMap(typeof(PocoComplex), new ClassMap<PocoComplex>(cm =>
            {
                cm.MapProperty(p => p.Time1).SerializerArg = new DateTimeSerializerArg() { TickResolution = TimeSpan.TicksPerDay };
                cm.MapProperty(p => p.Time2).SerializerArg = new DateTimeSerializerArg() { TickResolution = TimeSpan.TicksPerMinute };
            }));

            var val = new PocoComplex()
            {
                Id = 7,
                Time1 = new DateTime(2010, 10, 10, 10, 10, 10),
                Time2 = new DateTime(2010, 10, 10, 10, 10, 10),
                Comment = null,//"Bla!",
                SubRecord = new PocoWithAllPrimitives()
                {
                    Byte = 1,
                    SByte = -2,
                    Int16 = -3,
                    UInt16 = 4,
                    Int32 = 5,
                    UInt32 = 6,
                    Int64 = 7,
                    UInt64 = 8,
                    Char = 'a',
                },
                Dec1 = 1234567890.954m,
                Real32 = 17890.9f,
                Real64 = 167890.1,
                TestEnum = TestEnum.Val1,
                IntList = new List<int> { 1, 4, 9, 16 },
                SubRecordList = new List<PocoWithAllPrimitives>
                {
                    new PocoWithAllPrimitives()
                    {
                        Byte = 11,
                        SByte = -12,
                        Int16 = -13,
                        UInt16 = 14,
                        Int32 = 15,
                        UInt32 = 16,
                        Int64 = 17,
                        UInt64 = 18,
                        Char = 'b',
                    }
                },
                IntDict = new Dictionary<int, int>
                {
                    { 0 , 0 },
                    { 1 , 1 },
                    { 2 , 4 },
                    { 3 , 9 },
                    { 4 , 16 },
                },
                SubRecordDict = new Dictionary<string, PocoSimple>
                {
                    { "key1" , new PocoSimple{Int = 1 , Str = null}},
                    { "key2" , new PocoSimple{Int = -1 , Str = "str"} },
                }
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoComplex>(buf);

            Assert.AreEqual(val, cloned); //Note: comparer use class map, so ite return equal even that they are not!

            Assert.AreEqual(cloned.Time1.Second, 0);
            Assert.AreEqual(cloned.Time1.Minute, 0);
            Assert.AreEqual(cloned.Time1.Hour, 0);

            Assert.AreEqual(cloned.Time2.Value.Second, 0);
            Assert.AreEqual(cloned.Time2.Value.Minute, 10);
            Assert.AreEqual(cloned.Time2.Value.Hour, 10);

            Assert.AreEqual(cloned.Time3, null);


        }
    }
}
