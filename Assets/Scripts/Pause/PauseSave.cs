using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PauseSave : MenuItem
{
	public override void activate()
	{
		Debug.Log("Writing Information 1");
		SaveData data = new SaveData();
		data.playerTransform = GameObject.Find("OVRPlayerController").transform;

		Stream stream = File.Open("saved_game.game", FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder(); 
		Debug.Log("Writing Information");
		bformatter.Serialize(stream, data);
		stream.Close();
	}
}
