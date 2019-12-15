using System;
using System.Collections;
using System.Text;

namespace BinaryConverter.Serializers
{

    class ArraySerializer : BaseSerializer
    {
        public override bool CommonNullHandle { get { return false; } }


        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            int count = br.Read7BitInt();
            if (count == -1)
            {
                return null;
            }
            var instance = Array.CreateInstance(type.GetElementType(), count);

            for (int i = 0; i < count; i++)
            {
                instance.SetValue(Serializer.DeserializeObject(br, type.GetElementType(), settings, null, null), i);
            }
            return instance;
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            Array array = value as Array;
            if (array == null)
            {
                bw.Write7BitLong(-1);
                return;
            }

            var count = array.Length;
            bw.Write7BitLong(count);
            Type elementType = array.GetType().GetElementType();
            for (int i = 0; i < count; i++)
            {
                Serializer.SerializeObject(elementType, array.GetValue(i), bw, settings, null, null);
            }
        }

    }
}
