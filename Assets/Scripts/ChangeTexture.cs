using UnityEngine;
using System.Collections;

public class ChangeTexture : MonoBehaviour {
	public bool on = false;
	public GameObject source;
	public Material on_texture;
	public Material off_texture;
	
	void OnTriggerEnter(Collider other)
	{
		if (on)
		{
			source.GetComponent<Renderer>().material = off_texture;
			on = false;
		}
		else
		{
			source.GetComponent<Renderer>().material = on_texture;
			on = true;
		}
	}
}