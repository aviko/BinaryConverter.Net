using System;
using System.IO;

namespace BinaryConverter
{
    public static class BinaryConvert
    {
        public static T DeserializeObject<T>(byte[] buf, SerializerSettings settings = null)
        {
            return Serializer.DeserializeObject<T>(buf, settings);
        }

        public static object DeserializeObject(Type type, byte[] buf, SerializerSettings settings = null)
        {
            return Serializer.DeserializeObject(buf, type, settings);

        }

        public static byte[] SerializeObject<T>(T value, SerializerSettings settings = null)
        {
            return Serializer.SerializeObject(value, settings);
        }

        public static byte[] SerializeObject<T>(Type type, T value, SerializerSettings settings = null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryTypesWriter bw = new BinaryTypesWriter(ms))
                {
                    Serializer.SerializeObject(typeof(T), value, bw, settings, null, null);
                    return ms.ToArray();
                }
            }
        }
    }
}
