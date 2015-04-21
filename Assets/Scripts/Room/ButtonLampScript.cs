using UnityEngine;
using System.Collections;

public class ButtonLampScript : ButtonScript
{
	public Light lamp;
	public float intensity = 1.0f;
	private float nextFire = 0.0f;

	void Start()
	{
		this.multiple = true;
		this.reset = true;
		this.singlereset = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (Time.time < nextFire)
			return;

		nextFire = Time.time + 1.0f;

		if (toggle())
		{
			lamp.intensity =
				(isFinal() && multiple && prev != null)
					? (lamp.intensity > 0.001f
						? 0.0f
						: intensity)
					: intensity;
		}
		else if (!multiple || (prev == null && next == null))
		{
			lamp.intensity = 0.0f;
		}
	}
	
	public override void enable()
	{
		transform.Rotate(0, 180f, 0f);
	}

	public override void disable()
	{
		transform.Rotate(0, 180f, 0f);
	}
}
