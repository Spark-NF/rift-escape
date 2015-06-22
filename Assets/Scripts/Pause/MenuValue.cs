using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class MenuValue : MonoBehaviour
{
	protected int val = 0;
	public int def = 0;
	public int min = 0;
	public int max = 10;
	public int step = 1;

	public GameObject label = null;
	protected string format;

	public void Start()
	{
		Text t = label.GetComponent<Text>();
		format = t.text;

		val = def;

		updateLabel();
	}
	
	public abstract void change(int oldValue, int newValue);

	public void minus()
	{
		int newVal = val - step;
		if (newVal < min)
			newVal = min;

		change(val, newVal);
		val = newVal;

		updateLabel();
	}

	public void plus()
	{
		int newVal = val + step;
		if (newVal > max)
			newVal = max;
		
		change(val, newVal);
		val = newVal;
		
		updateLabel();
	}

	public void reset()
	{
		change(val, def);
		val = def;
		
		updateLabel();
	}

	public void updateLabel()
	{
		Text t = label.GetComponent<Text>();
		t.text = format.Replace("#", System.Convert.ToString(val));
	}
}
