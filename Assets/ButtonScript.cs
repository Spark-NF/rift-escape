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
	
	public bool toggle()
	{
		// When we toggle again the last element
		if (enabled && (next == null || !next.GetComponent<ButtonScript>().enabled))
		{
			disable(false);
			return false;
		}
		
		// If we try to toggle an invalid element
		if (!isValid())
		{
			disable(reset);
			return false;
		}
		
		return enable();
	}
	
	public void disable(bool recursive = true)
	{
		enabled = false;
		
		// Recursive disabling disables all the chain
		if (prev != null && recursive)
			prev.GetComponent<ButtonScript>().disable(true);
		if (next != null && recursive)
			next.GetComponent<ButtonScript>().disable(true);
	}
	
	public bool enable()
	{
		enabled = true;
		
		// If this element is the last of the chain
		if (next == null || final)
		{
			// In case of multiple possible activations, we reset the chain
			if (multiple)
				disable(true);
			
			return true;
		}
		
		return true;
	}
	
	public bool isValid()
	{
		if (prev != null && !prev.GetComponent<ButtonScript>().enabled)
			return false;
		
		return true;
	}
}
