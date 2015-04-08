using UnityEngine;
using System.Collections;

public class ButtonLampScript : ButtonScript
{
	public Light lamp;
	public float intensity = 1.0f;

	void Start()
	{
		this.multiple = true;
		this.reset = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("OnTriggerEnter");
		if (toggle())
		{
			Debug.Log("Toggle");
			lamp.intensity =
				lamp.intensity <= 0.0001f
				? intensity
				: 0
			;
		}
	}
}
