using BinaryConverter.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BinaryConverter
{

    public class SerializerSettings
    {
        public Dictionary<Type, ISerializer> SerializerMap { get; set; } = new Dictionary<Type, ISerializer>();
        public Dictionary<Type, ISerializerArg> SerializerArgMap { get; set; } = new Dictionary<Type, ISerializerArg>();
    }


    public static class Serializer
    {
        //===============================================================================================
        // Serialize
        //===============================================================================================

        public static byte[] SerializeObject<T>(T value, SerializerSettings settings)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryTypesWriter bw = new BinaryTypesWriter(ms))
                {
                    SerializeObject(typeof(T), value, bw, settings, null, null);
                    return ms.ToArray();
                }
            }
        }

        internal static void SerializeObject<T>(Type type, T value, BinaryTypesWriter bw, SerializerSettings settings, ISerializer serializer, ISerializerArg serializerArg)
        {
            serializer = GetSerializer(type, serializer);
            if (serializer == null)
            {
                throw new Exception($"SerializeObject: serializer not found for type {type.FullName}");
            }

            if (type.IsClass && serializer.CommonNullHandle)
            {
                bw.Write(value != null);
                if (value == null)
                {
                    return;
                }
            }

            serializer.Serialize(bw, type, settings, serializerArg, value);

        }

        //===============================================================================================
        // Deserialize
        //===============================================================================================


        public static T DeserializeObject<T>(byte[] buf, SerializerSettings settings)
        {
            return (T)DeserializeObject(buf, typeof(T), settings);
        }

        public static object DeserializeObject(byte[] buf, Type type, SerializerSettings settings)
        {
            using (MemoryStream ms = new MemoryStream(buf))
            {

                using (BinaryTypesReader br = new BinaryTypesReader(ms))
                {
                    return DeserializeObject(br, type, settings, null, null);
                }
            }
        }

        internal static object DeserializeObject(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializer serializer, ISerializerArg serializerArg)
        {
            serializer = GetSerializer(type, serializer);
            if (serializer == null)
            {
                throw new Exception($"SerializeObject: serializer not found for type {type.FullName}");
            }

            if (type.IsClass && serializer.CommonNullHandle)
            {
                if (br.ReadBoolean() == false) //null
                {
                    return null;
                }
            }

            return serializer.Deserialize(br, type, settings, serializerArg);



        }

        //===============================================================================================
        // 
        //===============================================================================================


        private static ISerializer GetSerializer(Type type, ISerializer serializer)
        {
            if (serializer != null)
            {
                return serializer;
            }

            serializer = SerializerRegistry.GetSerializer(type);
            if (serializer != null)
            {
                return serializer;
            }

            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            if (nullableUnderlyingType != null)
            {
                return SerializerRegistry.GetNullableSerializer();
            }

            if (type.IsGenericType)
            {
                serializer = SerializerRegistry.GetSerializer(type.GetGenericTypeDefinition());
                if (serializer != null)
                {
                    return serializer;
                }
            }

            if (type.IsEnum)
            {
                return SerializerRegistry.GetSerializer(typeof(Enum));
            }

            if (type.IsClass && serializer == null)
            {

                return SerializerRegistry.GetSerializer(typeof(object));
            }

            return null;
        }
    }
}
