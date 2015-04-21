using UnityEngine;
using System.Collections;

public abstract class ButtonScript : MonoBehaviour
{
	// Next and previous elements for the chain
	public GameObject next = null;
	public GameObject prev = null;
	
	// Represents the current status of the element
	public bool myEnabled = false;
	
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
		if (myEnabled && singlereset && (next == null || !next.GetComponent<ButtonScript>().myEnabled))
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
	
	public abstract void disable();
	void _disable(bool recursive, ButtonScript from = null)
	{
		myEnabled = false;
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
	
	public abstract void enable();
	bool _enable()
	{
		myEnabled = true;
		enable();

		// If this element is the last of the chain
		if (isFinal())
		{
			// In case of multiple possible activations, we reset the chain
			if (multiple && (!singlereset || prev != null))
				_disable(true);
			
			return true;
		}
		
		return false;
	}
	
	bool isValid()
	{
		if (prev != null && !prev.GetComponent<ButtonScript>().myEnabled)
			return false;
		
		return true;
	}
	
	public bool isFinal()
	{
		return (next == null || final);
	}
}
