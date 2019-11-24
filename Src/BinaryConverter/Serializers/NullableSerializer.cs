using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Serializers
{
    class NullableSerializer : BaseSerializer
    {
        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            if (br.ReadBoolean() == false) //null
            {
                return null;
            }
            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            var serializer = SerializerRegistry.GetSerializer(nullableUnderlyingType);
            return Serializer.DeserializeObject(br, nullableUnderlyingType, settings, serializer, serializerArg);

        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            bw.Write(value != null);
            if (value == null)
            {
                return;
            }
            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            var serializer = SerializerRegistry.GetSerializer(nullableUnderlyingType);
            Serializer.SerializeObject(nullableUnderlyingType, value, bw, settings, serializer, serializerArg);
        }
    }
}
