using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable()]
public class SaveData : ISerializable
{
	public Transform playerTransform;
	
	public SaveData()
	{
	}

	public SaveData(SerializationInfo info, StreamingContext ctxt)
	{
		playerTransform = (Transform)info.GetValue("playerTransform", typeof(Transform));
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("playerTransform", playerTransform);
	}
}
