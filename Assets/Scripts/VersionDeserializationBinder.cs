using System.Runtime.Serialization;
using System;
using System.Reflection;

public sealed class VersionDeserializationBinder : SerializationBinder
{
	public override Type BindToType(string assemblyName, string typeName)
	{
		if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
		{
			Type typeToDeserialize = null;
			
			assemblyName = Assembly.GetExecutingAssembly().FullName;
			typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
			return typeToDeserialize;
		}
		return null;
	}
}