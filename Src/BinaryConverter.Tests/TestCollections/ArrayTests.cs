using BinaryConverter.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.TestCollections
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void Test_ArrayNull()
        {
            string[] val = null;

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<string[]>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_ArrayEmpty()
        {
            var val = new string[] { };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<string[]>(buf);

            CollectionAssert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_ArrayOfInt()
        {
            var val = new int[] {
                1 ,
                2 ,
                3
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<int[]>(buf);

            CollectionAssert.AreEqual(val, cloned);

        }

        [TestMethod]
        public void Test_ArrayOfString()
        {
            var val = new string[] {
                "1" ,
                "2" ,
                "3" ,
                null ,
                string.Empty
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<string[]>(buf);

            CollectionAssert.AreEqual(val, cloned);

        }

        [TestMethod]
        public void Test_ArrayOfStruct()
        {
            var val = new DateTime[] {
              new DateTime(2000, 1, 1) ,
              new DateTime(2002, 2, 2) ,
              new DateTime(2003, 3, 3) ,
              default(DateTime)
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<DateTime[]>(buf);

            CollectionAssert.AreEqual(val, cloned);

        }

        [TestMethod]
        public void Test_ArrayOfObjects()
        {
            var val = new PocoSimple[] {
                new PocoSimple() {Int = 1, Str = "1" } ,
                new PocoSimple() {Int = 2, Str = "2" } ,
                new PocoSimple() {Int = 3, Str = "3" } ,
                 null
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<PocoSimple[]>(buf);

            CollectionAssert.AreEqual(val, cloned);


        }
    }
}
