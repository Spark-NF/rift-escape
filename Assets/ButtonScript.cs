using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
	// Next and previous elements for the chain
	public GameObject next = null;
	public GameObject prev = null;
	
	// Represents the current status of the element
	public bool enabled = false;
	
	// Whether it's the last button in the chain or not
	public bool final = false;
	
	// Whether the chain can be activated multiple times
	protected bool multiple = true;
	
	// Whether the chain should be reset on failure
	protected bool reset = true;
	
	// Whether we can toggle single elements
	protected bool singlereset = true;
	
	public bool toggle()
	{
		// When we toggle again the last element
		if (enabled && singlereset)
		{
			_disable(false);
			return false;
		}
		
		// If we try to toggle an invalid element
		if (!isValid())
		{
			_disable(reset);
			return false;
		}
		
		return _enable();
	}
	
	void disable()
	{
	}
	void _disable(bool recursive, ButtonScript from = null)
	{
		enabled = false;
		disable();
		
		// Recursive disabling disables all the chain
		if (recursive)
		{
			if (prev != null && prev.GetComponent<ButtonScript>() != from)
				prev.GetComponent<ButtonScript>()._disable(true, this);
			if (next != null && next.GetComponent<ButtonScript>() != from)
				next.GetComponent<ButtonScript>()._disable(true, this);
		}
	}
	
	void enable()
	{
	}
	bool _enable()
	{
		enabled = true;
		enable();

		// If this element is the last of the chain
		if (next == null || final)
		{
			// In case of multiple possible activations, we reset the chain
			if (multiple)
				_disable(true);
			
			return true;
		}
		
		return false;
	}
	
	bool isValid()
	{
		if (prev != null && !prev.GetComponent<ButtonScript>().enabled)
			return false;
		
		return true;
	}
}
