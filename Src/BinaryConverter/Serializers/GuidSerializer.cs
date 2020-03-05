using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Serializers
{

    public class GuidSerializer : BaseSerializer
    {
        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            var guidByte = (byte[])Serializer.DeserializeObject(br, typeof(byte[]), settings, null, null);
            return new Guid(guidByte);
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var guidByte = ((Guid)value).ToByteArray();
            Serializer.SerializeObject(typeof(byte[]), guidByte, bw, settings, null, null);
        }
    }
}
