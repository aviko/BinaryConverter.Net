using BinaryConverter.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryConverter.Tests.TestCollections
{

    [TestClass]
    public class HashSetTests
    {
        [TestMethod]
        public void Test_ListNull()
        {
            HashSet<string> val = null;

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<HashSet<string>>(buf);

            Assert.AreEqual(val, cloned);
        }

        [TestMethod]
        public void Test_ListEmpty()
        {
            var val = new HashSet<string>();

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<HashSet<string>>(buf);

            CollectionAssert.AreEqual(val.OrderBy(x => x).ToList(), cloned.OrderBy(x => x).ToList());
        }

        [TestMethod]
        public void Test_ListOfInt()
        {
            var val = new HashSet<int>() {
                1 ,
                2 ,
                3
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<HashSet<int>>(buf);

            CollectionAssert.AreEqual(val.OrderBy(x => x).ToList(), cloned.OrderBy(x => x).ToList());

        }

        [TestMethod]
        public void Test_ListOfString()
        {
            var val = new HashSet<string>() {
                "1" ,
                "2" ,
                "3" ,
                null ,
                string.Empty
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<HashSet<string>>(buf);

            CollectionAssert.AreEqual(val.OrderBy(x => x).ToList(), cloned.OrderBy(x => x).ToList());

        }

        [TestMethod]
        public void Test_ListOfStruct()
        {
            var val = new HashSet<DateTime>() {
              new DateTime(2000, 1, 1) ,
              new DateTime(2002, 2, 2) ,
              new DateTime(2003, 3, 3) ,
              default(DateTime)
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<HashSet<DateTime>>(buf);

            CollectionAssert.AreEqual(val.OrderBy(x => x).ToList(), cloned.OrderBy(x => x).ToList());

        }

        [TestMethod]
        public void Test_ListOfObjects()
        {
            var val = new HashSet<PocoSimple>() {
                new PocoSimple() {Int = 1, Str = "1" } ,
                new PocoSimple() {Int = 2, Str = "2" } ,
                new PocoSimple() {Int = 3, Str = "3" } ,
                 null
            };

            var buf = BinaryConvert.SerializeObject(val);
            var cloned = BinaryConvert.DeserializeObject<HashSet<PocoSimple>>(buf);

            CollectionAssert.AreEqual(val.OrderBy(x => x?.Int ?? 0).ToList(), cloned.OrderBy(x => x?.Int ?? 0).ToList());


        }
    }
}
