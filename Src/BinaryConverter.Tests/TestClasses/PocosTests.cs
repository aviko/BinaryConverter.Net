using BinaryConverter.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestClasses
{
    [TestClass]
    public class PocosTests
    {
        [TestInitialize]
        public void testInit()
        {
            SerializerRegistry.UnregisterClassMap(typeof(PocoSimple));
        }


        [TestMethod]
        public void Test_PocosNull()
        {
            PocoSimple val = null;

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoSimple>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_PocosSimple()
        {
            var val = new PocoSimple()
            {
                Int = 1,
                Str = "1",
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoSimple>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_PocosSimpleNonGeneric()
        {
            var val = new PocoSimple()
            {
                Int = 1,
                Str = "1",
            };

            var buf = BinaryConvert.SerializeObject(typeof(PocoSimple), val);
            var cloned = BinaryConvert.DeserializeObject(typeof(PocoSimple),buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_PocoWithAllPrimitives()
        {
            var val = new PocoWithAllPrimitives()
            {
                Byte = 1,
                SByte = -1,
                Int16 = -2,
                UInt16 = 2,
                Int32 = 7,
                UInt32 = 22221,
                Int64 = -324561,
                UInt64 = 2341,
                Char = '1',
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoWithAllPrimitives>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_PocoComplexNotInitialized()
        {
            var val = new PocoComplex();

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoComplex>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_PocoComplex()
        {
            var val = new PocoComplex()
            {
                Id = 7,
                Time1 = new DateTime((DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond) * TimeSpan.TicksPerSecond),
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
                TupleN2 = new Tuple<int, string>(1, "1"),
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

            //for (int i = 0; i < 100_000; i++)
            {
                var buf = BinaryConvert.SerializeObject(val);
                var cloned = BinaryConvert.DeserializeObject<PocoComplex>(buf);
                Assert.AreEqual(val, cloned);
            }
        }

    }
}
