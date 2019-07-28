using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BinaryConverter.Serializers
{
    class ClassSerializer : BaseSerializer
    {
        private ConcurrentDictionary<Type, FastReflectionClassMap> _fastReflectionMaps = new ConcurrentDictionary<Type, FastReflectionClassMap>();

        public override object Deserialize(BinaryTypesReader br, Type type, SerializerSettings settings, ISerializerArg serializerArg)
        {
            var classMap = SerializerRegistry.GetClassMap(type); //todo: also base types?

            var fastReflectionClassMap = GetFastReflectionClassMap(type);
            var propNames = fastReflectionClassMap.PropertyInfos.Keys.ToList();
            var instance = fastReflectionClassMap.CreateInstance(Type.EmptyTypes); //Activator.CreateInstance(type);

            foreach (var propName in propNames)
            {
                MemberMap memberMap = classMap?.GetMemberMap(propName);
                if (memberMap?.Ignored == true) continue;

                var pInfo = fastReflectionClassMap.PropertyInfos[propName];
                pInfo.Setter(
                    ref instance,
                    Serializer.DeserializeObject(br, pInfo.PropertyType, settings, memberMap?.Serializer, memberMap?.SerializerArg));
            }


            return instance;
        }

        public override void Serialize(BinaryTypesWriter bw, Type type, SerializerSettings settings, ISerializerArg serializerArg, object value)
        {
            var classMap = SerializerRegistry.GetClassMap(type); //todo: also base types?

            var fastReflectionClassMap = GetFastReflectionClassMap(type);
            var propNames = fastReflectionClassMap.PropertyInfos.Keys.ToList();

            foreach (var propName in propNames)
            {
                MemberMap memberMap = classMap?.GetMemberMap(propName);
                if (memberMap?.Ignored == true) continue;

                var pInfo = fastReflectionClassMap.PropertyInfos[propName];

                var val = pInfo.Getter(value);

                Serializer.SerializeObject(pInfo.PropertyType, val, bw, settings, memberMap?.Serializer, memberMap?.SerializerArg);
            }

        }

        private FastReflectionClassMap GetFastReflectionClassMap(Type type)
        {
            if (_fastReflectionMaps.TryGetValue(type, out var fastReflectionClassMap) == false)
            {
                fastReflectionClassMap = new FastReflectionClassMap(type);
                _fastReflectionMaps[type] = fastReflectionClassMap;
            }
            return fastReflectionClassMap;
        }
    }

    internal class FastReflectionPropertyInfo
    {
        internal MemberGetter<object, object> Getter { get; set; }
        internal MemberSetter<object, object> Setter { get; set; }
        internal Type PropertyType { get; set; }
    }

    internal class FastReflectionClassMap
    {
        public Dictionary<string, FastReflectionPropertyInfo> PropertyInfos { get; set; } = new Dictionary<string, FastReflectionPropertyInfo>();
        public CtorInvoker<object> CreateInstance;

        internal FastReflectionClassMap(Type type)
        {

            CreateInstance = type.DelegateForCtor();

            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .OrderBy(x => x.MetadataToken);

            foreach (var prop in props)
            {
                var pInfo = new FastReflectionPropertyInfo()
                {
                    Getter = prop.DelegateForGet(),
                    Setter = prop.DelegateForSet(),
                    PropertyType = prop.PropertyType
                };

                PropertyInfos[prop.Name] = pInfo;
            }
        }

    }
}
