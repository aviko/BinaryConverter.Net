﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Serializers
{
    class DictionarySerializer : BaseSerializer
    {
        public override bool CommonNullHandle { get { return false; } }

        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            int count = br.Read7BitInt();
            if (count == -1)
            {
                return null;
            }
            var instance = (IDictionary)Activator.CreateInstance(type);
            Type genericArgKey = type.GetGenericArguments()[0];
            Type genericArgVal = type.GetGenericArguments()[1];

            for (int i = 0; i < count; i++)
            {
                instance.Add(
                    Serializer.DeserializeObject(br, genericArgKey, settings, null, null), //key
                    Serializer.DeserializeObject(br, genericArgVal, settings, null, null)); //val
            }
            return instance;
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var dictionary = value as IDictionary;
            if (dictionary == null)
            {
                bw.Write7BitLong(-1);
                return;
            }
            var count = dictionary.Count;
            bw.Write7BitLong(count);
            Type genericArgKey = type.GetGenericArguments()[0];
            Type genericArgVal = type.GetGenericArguments()[1];
            foreach (var key in dictionary.Keys)
            {
                Serializer.SerializeObject(genericArgKey, key, bw, settings, null, null);
                Serializer.SerializeObject(genericArgVal, dictionary[key], bw, settings, null, null);
            }
        }
    }
}
