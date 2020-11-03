using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryConverter.Serializers
{
    public class DateTimeSerializerArg : ISerializerArg
    {
        public long TickResolution { get; set; } = 0;
    }

    public class DateTimeSerializer : BaseSerializer
    {
        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            var typedSerializerArg = GetSerializerArg<DateTimeSerializerArg>(type, settings, serializerArg);

            if (typedSerializerArg != null && typedSerializerArg.TickResolution > 0)
            {
                return br.ReadCompactDateTime(typedSerializerArg.TickResolution);
            }

            return new DateTime(br.Read7BitLong(), DateTimeKind.Utc); //todo: compact
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var typedSerializerArg = GetSerializerArg<DateTimeSerializerArg>(type, settings, serializerArg);

            if (typedSerializerArg != null && typedSerializerArg.TickResolution > 0)
            {
                bw.WriteCompactDateTime((DateTime)value, typedSerializerArg.TickResolution);
                return;
            }

            bw.Write7BitLong(((DateTime)(object)value).Ticks); //todo: compact
        }
    }
    public class TimeSpanSerializer : BaseSerializer
    {
        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            var typedSerializerArg = GetSerializerArg<DateTimeSerializerArg>(type, settings, serializerArg);

            if (typedSerializerArg != null && typedSerializerArg.TickResolution > 0)
            {
                return new TimeSpan(br.Read7BitLong() * typedSerializerArg.TickResolution);
            }

            return new TimeSpan(br.Read7BitLong()); 
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var typedSerializerArg = GetSerializerArg<DateTimeSerializerArg>(type, settings, serializerArg);

            if (typedSerializerArg != null && typedSerializerArg.TickResolution > 0)
            {
                bw.Write7BitLong(((TimeSpan)(object)value).Ticks / typedSerializerArg.TickResolution);
                return;
            }

            bw.Write7BitLong(((TimeSpan)(object)value).Ticks); 
        }
    }
}
