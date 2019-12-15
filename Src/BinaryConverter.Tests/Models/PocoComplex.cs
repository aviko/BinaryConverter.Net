using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Tests.Models
{
    public enum TestEnum
    {
        Val0,
        Val1,
        Val10 = 10
    }

    class PocoComplex
    {
        public int Id { get; set; }
        public DateTime Time1 { get; set; }
        public DateTime? Time2 { get; set; }
        public DateTime? Time3 { get; set; }
        public string Comment { get; set; }
        public PocoWithAllPrimitives SubRecord { get; set; }
        public decimal Dec1 { get; set; }
        public float Real32 { get; set; }
        public double Real64 { get; set; }
        public TestEnum TestEnum { get; set; }
        public Tuple<int, string> TupleN2 { get; set; }
        public List<int> IntList { get; set; }
        public List<PocoWithAllPrimitives> SubRecordList { get; set; }
        public Dictionary<int, int> IntDict { get; set; }
        public Dictionary<string, PocoSimple> SubRecordDict { get; set; }
        public byte[] ByteArray1 { get; set; } //null
        public byte[] ByteArray2 { get; set; } //empty
        public byte[] ByteArray3 { get; set; }
        public string[] StringArray { get; set; }


        public override bool Equals(object other)
        {
            var bufThis = BinaryConvert.SerializeObject(this);
            var bufOhter = BinaryConvert.SerializeObject(other as PocoComplex);

            return ArrayEqualityComparer<byte>.Equals(bufThis, bufOhter);
        }

        public override int GetHashCode()
        {
            var bufThis = BinaryConvert.SerializeObject(this);
            return ArrayEqualityComparer<byte>.GetHashCode(bufThis);
        }
    }
}
