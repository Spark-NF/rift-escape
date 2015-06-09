using UnityEngine;
using System.Collections;

public abstract class MenuToggle : MenuItem
{
	public GameObject checkmark = null;
	public bool value = true;

	public abstract void toggle(bool active);

	public override void activate()
	{
		value = !value;
		checkmark.SetActive(value);
		toggle(value);
	}
}
