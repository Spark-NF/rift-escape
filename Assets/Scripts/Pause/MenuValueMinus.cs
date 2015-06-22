using UnityEngine;
using System.Collections;

public class MenuValueMinus : MenuItem
{
	public GameObject menuValue = null;
	private MenuValue menu = null;

	public void Start()
	{
		menu = menuValue.GetComponent<MenuValue>();
	}

	public override void activate()
	{
		menu.minus();
	}
}
