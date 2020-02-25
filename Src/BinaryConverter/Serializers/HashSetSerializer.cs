using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Serializers
{
    class HashSetSerializer : BaseSerializer
    {
        public override bool CommonNullHandle { get { return false; } }

        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            int count = br.Read7BitInt();
            if (count == -1)
            {
                return null;
            }
            var instance = Activator.CreateInstance(type);
            Type genericArgtype = type.GetGenericArguments()[0];
            var method = type.GetMethod("Add");

            for (int i = 0; i < count; i++)
            {
                method.Invoke(
                    instance,
                    new object[] { Serializer.DeserializeObject(br, genericArgtype, settings, null, null) });
            }
            return instance;
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var hashset = value as System.Collections.IEnumerable;
            if (hashset == null)
            {
                bw.Write7BitLong(-1);
                return;
            }

            var method = type.GetProperty("Count");
            var count = (int)method.GetValue(hashset);

            bw.Write7BitLong(count);
            Type genericArgtype = type.GetGenericArguments()[0];
            foreach (var val in hashset)
            {
                Serializer.SerializeObject(genericArgtype, val, bw, settings, null, null);
            }
        }
    }
}
