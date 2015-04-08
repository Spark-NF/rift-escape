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
		this.singlereset = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		toggle();
		
		transform.Rotate(0, 180f, 0f);
	}
	
	void enable()
	{
		lamp.intensity = intensity;
	}

	void disable()
	{
		lamp.intensity = 0f;
	}
}
