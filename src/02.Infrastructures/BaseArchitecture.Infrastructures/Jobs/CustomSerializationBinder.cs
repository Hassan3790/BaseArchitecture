using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace BaseArchitecture.Infrastructures.Jobs
{
    public class CustomSerializationBinder : ISerializationBinder
    {
        public Type BindToType(string assemblyName, string typeName)
        {
            var type = Assembly.Load(assemblyName)?.GetType(typeName);
            return type ?? Type.GetType($"{typeName}, {assemblyName}");
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.Assembly.FullName;
            typeName = serializedType.FullName;
        }
    }
}
