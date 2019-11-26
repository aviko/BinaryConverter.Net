using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryConverter.Serializers
{
    public class TupleSerializer : BaseSerializer
    {

        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            var props = type.GetProperties();
            var args = new object[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                args[i] = Serializer.DeserializeObject(br, props[i].PropertyType, settings, null, null);
            }

            var instance = Activator.CreateInstance(type, args);
            return instance;
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var props = value.GetType().GetProperties();//.Select().ToList();
            foreach (var prop in props)
            {
                Serializer.SerializeObject(prop.PropertyType, prop.GetValue(value), bw, settings, null, null);
            }
        }
    }
}
